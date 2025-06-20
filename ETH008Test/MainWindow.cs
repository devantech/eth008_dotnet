using System.Diagnostics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ETH008Test
{
    public partial class MainWindow : Form
    {

        const byte EXPECTED_ID = 19;

        TCPPort? port;


        public MainWindow()
        {
            InitializeComponent();
            setUIConnectedState(false);
        }


        /// <summary>
        /// Set the state of the UI elements.
        /// </summary>
        /// <param name="st">the state to set.</param>
        private void setUIConnectedState(bool st)
        {
            relay1_button.Enabled = st;
            relay2_button.Enabled = st;
            relay3_button.Enabled = st;
            relay4_button.Enabled = st;
            relay5_button.Enabled = st;
            relay6_button.Enabled = st;
            relay7_button.Enabled = st;
            relay8_button.Enabled = st;
            
            passwordTextBox.Enabled = !st;
            connectButton.Enabled = !st;
            moduleSelectComboBox.Enabled = !st;
            portNumber.Enabled = !st;

        }


        /// <summary>
        /// Called when a relay button is clicked. Tries to toggle the state of the relay that the button represents.
        /// </summary>
        /// <param name="sender">the button that was clicked</param>
        /// <param name="e">the click event</param>
        private async void RelayButtonClick(object sender, EventArgs e)
        {

            if (sender is Button button)
            {
                if (port == null) { return; }

                byte[] command = { 0, 0, 0 };
                command[0] = button.BackColor == Color.White ? (byte)0x20 : (byte)0x21; // Get the state to toggle the relay to
                string[] parts = button.Text.Split(" ");
                command[1] = Byte.Parse(parts[1]); // The relay number

                var tt = port.Write(command, 3);
                await tt;
                if (tt.Result == -1)
                {
                    return;
                }

                var rt = port.Read(command, 1);
                await rt;
                if (rt.Result == -1)
                {
                    return;
                }

            }

        }


        /// <summary>
        /// Called when the connect button is clicked. Tries to open a connection to a module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectButtonClicked(object sender, EventArgs e)
        {
            string ip = "";

            // Get the IP address either by requesting the user to input a custom one
            // or pulling it out of the selected module item.
            if (moduleSelectComboBox.SelectedIndex == 0)
            {
                IPForm ipf = new IPForm();
                if (ipf.ShowDialog(this) == DialogResult.OK)
                {
                    ip = ipf.ipAddress.Text;
                }
                else
                {
                    return;
                }
            }
            else
            {
                string item = moduleSelectComboBox.SelectedItem as string ?? "";
                string[] module = item.Split(',');
                if (module.Length < 2) { return; }
                ip = module[1];
            }

            // Connect to the selected module.
            ip = ip.Trim();
            int port = (int)portNumber.Value;
            var ct = OpenConnection(ip, port);

        }


        private void MainWindow_Load(object sender, EventArgs e)
        {
            moduleSelectComboBox.Items.Add("Custom IP"); // Menu item used to input a custom IP.

            UDPScan scan = new UDPScan();
            scan.StartScan((m) =>
            {
                Debug.WriteLine("Module found -> " + m.hostname + " " + m.ip);
                moduleSelectComboBox.Invoke(() =>
                {
                    moduleSelectComboBox.Items.Add(m.hostname + ", " + m.ip);
                });
            });
        }


        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopConnection();
        }


        /// <summary>
        /// Set the states of the output controls.
        /// </summary>
        /// <param name="states"></param>
        private void SetUIOutputStates(byte[] states)
        {

            relay1_button.BackColor = (states[0] & 0x01) == 0 ? Color.White : Color.Red;
            relay2_button.BackColor = (states[0] & 0x02) == 0 ? Color.White : Color.Red;
            relay3_button.BackColor = (states[0] & 0x04) == 0 ? Color.White : Color.Red;
            relay4_button.BackColor = (states[0] & 0x08) == 0 ? Color.White : Color.Red;
            relay5_button.BackColor = (states[0] & 0x10) == 0 ? Color.White : Color.Red;
            relay6_button.BackColor = (states[0] & 0x20) == 0 ? Color.White : Color.Red;
            relay7_button.BackColor = (states[0] & 0x40) == 0 ? Color.White : Color.Red;
            relay8_button.BackColor = (states[0] & 0x80) == 0 ? Color.White : Color.Red;

        }


        private async void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Enabled = false;

            var rt = GetDigitalOuts();
            await rt;
            if (rt.Result == null)
            {
                DoError("Could not read output states.");
                return;
            }
            SetUIOutputStates(rt.Result);

            var pt = GetPSU();
            await pt;
            if (pt.Result == -1)
            {
                DoError("Could not read PSU voltages.");
                return;
            }
            int psu = pt.Result;
            powerLabel.Text = "PSU: " + psu / 10 + "." + psu % 10;

            timer1.Enabled = true;

        }

        private void StopConnection()
        {
            timer1.Enabled = false;
            if (port != null)
            {
                port.Close();
                port = null;
            }
            setUIConnectedState(false);
        }


        /// <summary>
        /// Notify the user of an error, and shut down the connection.
        /// </summary>
        /// <param name="message">The error message to show to the user.</param>
        void DoError(string message)
        {
            StopConnection();
            const string caption = "Error";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
        }


        /// <summary>
        /// Try to connect to a module.
        /// </summary>
        /// <param name="i">the ip address.</param>
        /// <param name="p">the port.</param>
        /// <returns></returns>
        private async Task OpenConnection(string i, int p)
        {
            // Connect to the module
            port = new TCPPort(i, p);
            var ct = port.Connect();
            await ct;
            if (ct.Result == false)
            {
                DoError("Failed to connect to modue.");
                return;
            }

            // Check to see if the password is enabled
            var pt = CheckPassword();
            await pt;
            if (pt.Result == false)
            {
                DoError("Incorrect password.");
                return;
            }

            var mi = GetModuleInfo();
            await mi;
            if (mi.Result == null)
            {
                DoError("Unable to get module info.");
                return;
            }
            byte id = mi.Result![0];
            byte fw = mi.Result![1];

            if (id != EXPECTED_ID)
            {
                DoError("Wrong module ID.");
                return;
            }

            idLabel.Text = "Module ID: " + id;
            firmwareLabel.Text = "FirmwareVersion: " + fw;

            timer1.Enabled = true;
            setUIConnectedState(true);

        }


        /// <summary>
        /// Get the PSU voltage from the module.
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetPSU()
        {
            byte[] data = new byte[1];
            data[0] = 0x78;     // Command to get power supply voltage
            if (port == null) return -1;

            var tt = port.Write(data, 1);
            await tt;
            if (tt.Result == -1)
            {
                return -1;
            }

            var rt = port.Read(data, 1);
            await rt;
            if (rt.Result == -1)
            {
                return -1;
            }

            return data[0];


        }

        /// <summary>
        /// Get the unlock time of the module.
        /// </summary>
        /// <returns>the unlock time</returns>
        private async Task<int> GetUnlockTime()
        {
            byte[] data = new byte[1];
            data[0] = 0x7A;     // Command to get the unlock time
            if (port == null) return -1;

            var tt = port.Write(data, 1);
            await tt;
            if (tt.Result == -1)
            {
                return -1;
            }

            var rt = port.Read(data, 1);
            await rt;
            if (rt.Result == -1)
            {
                return -1;
            }

            return data[0];
        }


        /// <summary>
        /// Send a password to the module.
        /// </summary>
        /// <param name="pw">the password to send.</param>
        /// <returns></returns>
        private async Task<bool> SendPassword(string pw)
        {
            if (port == null) return false;

            // The password submit command is 0x79 which is an ascii 'y' character.
            // Place a 'y' in front of the password and send it to submit the password
            // to the module.
            byte[] data = Encoding.ASCII.GetBytes("y" + pw);

            var tt = port.Write(data, data.Length);
            await tt;
            if (tt.Result == -1)
            {
                return false;
            }

            var rt = port.Read(data, 1);
            await rt;
            if (rt.Result == -1)
            {
                return false;
            }

            if (data[0] != 1) return false;

            return true;
        }


        /// <summary>
        /// Check to see if the password is enabled, if it is request it from the user and check for success.
        /// </summary>
        /// <returns>true is the module is ready, and false if it is locked.</returns>
        private async Task<bool> CheckPassword()
        {

            var ut = GetUnlockTime();
            await ut;
            if (ut.Result == 0) // Password is enabled
            {
                var sp = SendPassword(passwordTextBox.Text);  // Send the password to the module
                await sp;
                if (sp.Result == false) return false;

                ut = GetUnlockTime();       // Check to see if the password was accepted
                await ut;
                if (ut.Result == 0) { return false; }

            }

            return true;

        }


        /// <summary>
        /// Get the module info back from the module.
        /// </summary>
        /// <returns>The module info.</returns>
        private async Task<byte[]?> GetModuleInfo()
        {

            if (port == null) return null;

            byte[] data = new byte[3];
            data[0] = 0x10;

            var tt = port.Write(data, 1);
            await tt;
            if (tt.Result == -1)
            {
                return null;
            }

            var rt = port.Read(data, 3);
            await rt;
            if (rt.Result == -1)
            {
                return null;
            }

            return data;

        }


        /// <summary>
        /// Gets the states of the modules digital outputs.
        /// </summary>
        /// <returns>the states of the digital outputs, or null on error.</returns>
        private async Task<byte[]?> GetDigitalOuts()
        {

            if (port == null) return null;

            byte[] data = new byte[1];
            data[0] = 0x24;

            var tt = port.Write(data, 1);
            await tt;
            if (tt.Result == -1)
            {
                return null;
            }

            var rt = port.Read(data, 1);
            await rt;
            if (rt.Result == -1)
            {
                return null;
            }

            return data;

        }


    }
}
