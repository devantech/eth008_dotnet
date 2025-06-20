using System;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ETH008Test
{
    internal class TCPPort
    {

        private TcpClient client = new TcpClient();
        private NetworkStream? stream;

        private string ip;
        private int port;


        public TCPPort(String i, int p) {

            ip = i;
            port = p;

        }


        /// <summary>
        /// Try and connect to the module.
        /// </summary>
        /// <returns>True for success, false for failure.</returns>
        public async Task<bool> Connect()
        {
            try
            {
                var ct = client.ConnectAsync(ip, port);
                await ct;
                stream = client.GetStream();
                stream.ReadTimeout = 1000;
                stream.WriteTimeout = 1000;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Close();
                return false;
            }
        }


        /// <summary>
        /// Try and write a number of bytes to the module.
        /// </summary>
        /// <param name="data">A buffer containing bytes to write.</param>
        /// <param name="length">The bumber of bytes to write.</param>
        /// <returns>-1 on failure to write.</returns>
        public async Task<int> Write(byte[] data, int length)
        {

            if (stream == null) return -1;  // Return fail if no network stream
            if (!client.Connected) return -1; // Return fail if the client is not connected

            try
            {
                var wt = stream.WriteAsync(data, 0, length);
                await wt;
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Close();
            }
            return -1;
        }


        /// <summary>
        /// Try and read a number of bytes from the module.
        /// </summary>
        /// <param name="buffer">The buffer to place the bytes into.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>-1 on failure to read.</returns>
        public async Task<int> Read(byte[] buffer, int length)
        {

            if (stream == null) return -1;  // Return fail if no network stream
            if (!client.Connected) return -1; // Return fail if the client is not connected

            try
            {
                var rt = stream.ReadAsync(buffer, 0, length);
                await rt;
                return rt.Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Close();
                return -1;
            }

        }


        /// <summary>
        /// Dispose of the network streams.
        /// </summary>
        public void Close()
        {
            if (stream != null) stream.Dispose();
            client.Dispose();
        }


    }
}
