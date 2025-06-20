namespace ETH008Test
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            relay1_button = new Button();
            relay2_button = new Button();
            moduleSelectComboBox = new ComboBox();
            groupBox1 = new GroupBox();
            passwordTextBox = new TextBox();
            label2 = new Label();
            connectButton = new Button();
            label1 = new Label();
            portNumber = new NumericUpDown();
            timer1 = new System.Windows.Forms.Timer(components);
            groupBox2 = new GroupBox();
            powerLabel = new Label();
            firmwareLabel = new Label();
            idLabel = new Label();
            relay3_button = new Button();
            relay4_button = new Button();
            relay5_button = new Button();
            relay6_button = new Button();
            relay7_button = new Button();
            relay8_button = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)portNumber).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // relay1_button
            // 
            relay1_button.Location = new Point(12, 12);
            relay1_button.Name = "relay1_button";
            relay1_button.Size = new Size(214, 23);
            relay1_button.TabIndex = 0;
            relay1_button.Text = "Relay 1";
            relay1_button.UseVisualStyleBackColor = true;
            relay1_button.Click += RelayButtonClick;
            // 
            // relay2_button
            // 
            relay2_button.Location = new Point(12, 41);
            relay2_button.Name = "relay2_button";
            relay2_button.Size = new Size(214, 23);
            relay2_button.TabIndex = 1;
            relay2_button.Text = "Relay 2";
            relay2_button.UseVisualStyleBackColor = true;
            relay2_button.Click += RelayButtonClick;
            // 
            // moduleSelectComboBox
            // 
            moduleSelectComboBox.FormattingEnabled = true;
            moduleSelectComboBox.Location = new Point(6, 22);
            moduleSelectComboBox.Name = "moduleSelectComboBox";
            moduleSelectComboBox.Size = new Size(202, 23);
            moduleSelectComboBox.TabIndex = 2;
            moduleSelectComboBox.Text = "Select Module";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(passwordTextBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(connectButton);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(portNumber);
            groupBox1.Controls.Add(moduleSelectComboBox);
            groupBox1.Location = new Point(232, 97);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(214, 141);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Connection Settings";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(69, 78);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(139, 23);
            passwordTextBox.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 81);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 6;
            label2.Text = "Password";
            // 
            // connectButton
            // 
            connectButton.Location = new Point(6, 107);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(202, 23);
            connectButton.TabIndex = 5;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += ConnectButtonClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 53);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 4;
            label1.Text = "Port Number";
            // 
            // portNumber
            // 
            portNumber.Location = new Point(88, 51);
            portNumber.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            portNumber.Name = "portNumber";
            portNumber.Size = new Size(120, 23);
            portNumber.TabIndex = 3;
            portNumber.Value = new decimal(new int[] { 17494, 0, 0, 0 });
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(powerLabel);
            groupBox2.Controls.Add(firmwareLabel);
            groupBox2.Controls.Add(idLabel);
            groupBox2.Location = new Point(232, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(214, 79);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Module Information";
            // 
            // powerLabel
            // 
            powerLabel.AutoSize = true;
            powerLabel.Location = new Point(6, 55);
            powerLabel.Name = "powerLabel";
            powerLabel.Size = new Size(46, 15);
            powerLabel.TabIndex = 2;
            powerLabel.Text = "Power: ";
            // 
            // firmwareLabel
            // 
            firmwareLabel.AutoSize = true;
            firmwareLabel.Location = new Point(6, 37);
            firmwareLabel.Name = "firmwareLabel";
            firmwareLabel.Size = new Size(103, 15);
            firmwareLabel.TabIndex = 1;
            firmwareLabel.Text = "Firmware version: ";
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(6, 19);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(68, 15);
            idLabel.TabIndex = 0;
            idLabel.Text = "Module ID: ";
            // 
            // relay3_button
            // 
            relay3_button.Location = new Point(12, 70);
            relay3_button.Name = "relay3_button";
            relay3_button.Size = new Size(214, 23);
            relay3_button.TabIndex = 5;
            relay3_button.Text = "Relay 3";
            relay3_button.UseVisualStyleBackColor = true;
            relay3_button.Click += RelayButtonClick;
            // 
            // relay4_button
            // 
            relay4_button.Location = new Point(12, 99);
            relay4_button.Name = "relay4_button";
            relay4_button.Size = new Size(214, 23);
            relay4_button.TabIndex = 6;
            relay4_button.Text = "Relay 4";
            relay4_button.UseVisualStyleBackColor = true;
            relay4_button.Click += RelayButtonClick;
            // 
            // relay5_button
            // 
            relay5_button.Location = new Point(12, 128);
            relay5_button.Name = "relay5_button";
            relay5_button.Size = new Size(214, 23);
            relay5_button.TabIndex = 7;
            relay5_button.Text = "Relay 5";
            relay5_button.UseVisualStyleBackColor = true;
            relay5_button.Click += RelayButtonClick;
            // 
            // relay6_button
            // 
            relay6_button.Location = new Point(12, 157);
            relay6_button.Name = "relay6_button";
            relay6_button.Size = new Size(214, 23);
            relay6_button.TabIndex = 8;
            relay6_button.Text = "Relay 6";
            relay6_button.UseVisualStyleBackColor = true;
            relay6_button.Click += RelayButtonClick;
            // 
            // relay7_button
            // 
            relay7_button.Location = new Point(12, 186);
            relay7_button.Name = "relay7_button";
            relay7_button.Size = new Size(214, 23);
            relay7_button.TabIndex = 9;
            relay7_button.Text = "Relay 7";
            relay7_button.UseVisualStyleBackColor = true;
            relay7_button.Click += RelayButtonClick;
            // 
            // relay8_button
            // 
            relay8_button.Location = new Point(12, 215);
            relay8_button.Name = "relay8_button";
            relay8_button.Size = new Size(214, 23);
            relay8_button.TabIndex = 10;
            relay8_button.Text = "Relay 8";
            relay8_button.UseVisualStyleBackColor = true;
            relay8_button.Click += RelayButtonClick;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 251);
            Controls.Add(relay8_button);
            Controls.Add(relay7_button);
            Controls.Add(relay6_button);
            Controls.Add(relay5_button);
            Controls.Add(relay4_button);
            Controls.Add(relay3_button);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(relay2_button);
            Controls.Add(relay1_button);
            Name = "MainWindow";
            Text = "Form1";
            FormClosing += MainWindow_FormClosing;
            Load += MainWindow_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)portNumber).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button relay1_button;
        private Button relay2_button;
        private ComboBox moduleSelectComboBox;
        private GroupBox groupBox1;
        private Label label1;
        private NumericUpDown portNumber;
        private Button connectButton;
        private System.Windows.Forms.Timer timer1;
        private TextBox passwordTextBox;
        private Label label2;
        private GroupBox groupBox2;
        private Label powerLabel;
        private Label firmwareLabel;
        private Label idLabel;
        private Button relay3_button;
        private Button relay4_button;
        private Button relay5_button;
        private Button relay6_button;
        private Button relay7_button;
        private Button relay8_button;
    }
}
