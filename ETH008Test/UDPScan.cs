using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ETH008Test
{

    /// <summary>
    /// Holds information about a discovered module.
    /// </summary>
    class ModuleData
    {
        public string hostname = "";
        public string ip = "";
    }


    internal class UDPScan
    {

        // Delegate to handle callbacks when a module is discovered.
        public delegate void ModuleFoundCallback(ModuleData md);
        private ModuleFoundCallback? listener = null;


        struct UDPState
        {
            public IPEndPoint EP;
            public UdpClient UDPClient;
        }

        private UDPState GlobalUDP;


        /// <summary>
        /// Starts a UDP scan to find modules on the network.
        /// </summary>
        public void StartScan(ModuleFoundCallback mf)
        {

            try
            {
                listener = mf;
                GlobalUDP.UDPClient = new UdpClient();
                GlobalUDP.EP = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 30303);
                IPEndPoint BindEP = new IPEndPoint(IPAddress.Any, 30303);
                byte[] DiscoverMsg = Encoding.ASCII.GetBytes("Discovery: Who is out there?");

                // Set the local UDP port to listen on
                GlobalUDP.UDPClient.Client.Bind(BindEP);

                // Enable the transmission of broadcast packets without having them be received by ourself
                GlobalUDP.UDPClient.EnableBroadcast = true;
                GlobalUDP.UDPClient.MulticastLoopback = false;

                // Configure ourself to receive discovery responses
                GlobalUDP.UDPClient.BeginReceive(ReceiveCallback, GlobalUDP);

                // Transmit the discovery request message
                GlobalUDP.UDPClient.Send(DiscoverMsg, DiscoverMsg.Length, new System.Net.IPEndPoint(System.Net.IPAddress.Parse("255.255.255.255"), 30303));
            }
            catch
            {
                throw;
            }

        }


        /// <summary>
        /// A datagram packet was received. Pull the module information out of it.
        /// </summary>
        /// <param name="ar"></param>
        public void ReceiveCallback(IAsyncResult ar)
        {
            UDPState MyUDP;
            if (ar.AsyncState is not UDPState st)
            {
                return;
            }
            else
            {
                MyUDP = st;
            }


            byte[] data;
            int line_start = 0;
            int line_end;

            ModuleData module = new();

            // Obtain the UDP message body and convert it to a string, with remote IP address attached as well
            data = (MyUDP.UDPClient.EndReceive(ar, ref MyUDP.EP!));

            if (Encoding.UTF8.GetString(data) != "Discovery: Who is out there?")
            {
                do
                {
                    line_end = Array.IndexOf<byte>(data, 0x0a, line_start);
                    switch (data[line_start])
                    {
                        case 4:
                            module.hostname = Encoding.UTF8.GetString(data, line_start + 1, line_end - line_start);
                            module.hostname = module.hostname.TrimEnd();
                            break;
                        case 5:
                            module.ip = data[line_start + 1].ToString() + '.' + data[line_start + 2].ToString() + '.' + data[line_start + 3].ToString() + '.' + data[line_start + 4].ToString();
                            break;
                        case 69: //E at the start of string ETH484
                            string ReceiveString = Encoding.ASCII.GetString(data);
                            ReceiveString = MyUDP.EP!.Address.ToString() + "\n" + ReceiveString.Replace("\r\n", "\n");
                            MyUDP.UDPClient.BeginReceive(ReceiveCallback, MyUDP);
                            return;
                    }
                    line_start = line_end + 1;
                }
                while (line_start < data.Length);

                if (module.hostname != null)
                {
                    listener?.Invoke(module);
                }
            }
            // Configure the UdpClient class to accept more messages, if they arrive
            MyUDP.UDPClient.BeginReceive(ReceiveCallback, MyUDP);
        }


        /// <summary>
        /// Stop the UDP scan.
        /// </summary>
        public void StopUDP()
        {
            GlobalUDP.UDPClient.Close();
        }

    }

    
}
