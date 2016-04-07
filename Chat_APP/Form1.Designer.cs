namespace Chat_APP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textLocalPort = new System.Windows.Forms.TextBox();
            this.textLocalIP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textFriendPort = new System.Windows.Forms.TextBox();
            this.textFriendIP = new System.Windows.Forms.TextBox();
            this.textMessage = new System.Windows.Forms.TextBox();
            this.listMessage = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listUnique = new System.Windows.Forms.ListBox();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listDataEX = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataSize = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.clearConsole = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textLocalPort);
            this.groupBox1.Controls.Add(this.textLocalIP);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Center 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP";
            // 
            // textLocalPort
            // 
            this.textLocalPort.Location = new System.Drawing.Point(78, 58);
            this.textLocalPort.Name = "textLocalPort";
            this.textLocalPort.Size = new System.Drawing.Size(100, 20);
            this.textLocalPort.TabIndex = 1;
            // 
            // textLocalIP
            // 
            this.textLocalIP.Location = new System.Drawing.Point(78, 20);
            this.textLocalIP.Name = "textLocalIP";
            this.textLocalIP.Size = new System.Drawing.Size(100, 20);
            this.textLocalIP.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textFriendPort);
            this.groupBox2.Controls.Add(this.textFriendIP);
            this.groupBox2.Location = new System.Drawing.Point(323, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Center 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "IP";
            // 
            // textFriendPort
            // 
            this.textFriendPort.Location = new System.Drawing.Point(74, 57);
            this.textFriendPort.Name = "textFriendPort";
            this.textFriendPort.Size = new System.Drawing.Size(100, 20);
            this.textFriendPort.TabIndex = 1;
            // 
            // textFriendIP
            // 
            this.textFriendIP.Location = new System.Drawing.Point(74, 19);
            this.textFriendIP.Name = "textFriendIP";
            this.textFriendIP.Size = new System.Drawing.Size(100, 20);
            this.textFriendIP.TabIndex = 0;
            // 
            // textMessage
            // 
            this.textMessage.Location = new System.Drawing.Point(117, 119);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(325, 20);
            this.textMessage.TabIndex = 2;
            // 
            // listMessage
            // 
            this.listMessage.FormattingEnabled = true;
            this.listMessage.Location = new System.Drawing.Point(12, 174);
            this.listMessage.Name = "listMessage";
            this.listMessage.Size = new System.Drawing.Size(200, 160);
            this.listMessage.Sorted = true;
            this.listMessage.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(218, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 100);
            this.button1.TabIndex = 4;
            this.button1.Text = "Start Connection";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(448, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Send Data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 116);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Load Local Data";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(218, 174);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(99, 62);
            this.button4.TabIndex = 7;
            this.button4.Text = "Initial Sync";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listUnique
            // 
            this.listUnique.FormattingEnabled = true;
            this.listUnique.Location = new System.Drawing.Point(218, 357);
            this.listUnique.Name = "listUnique";
            this.listUnique.Size = new System.Drawing.Size(305, 56);
            this.listUnique.TabIndex = 8;
            // 
            // textSearch
            // 
            this.textSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textSearch.Location = new System.Drawing.Point(12, 337);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(200, 20);
            this.textSearch.TabIndex = 9;
            this.textSearch.TextChanged += new System.EventHandler(this.textSearch_TextChanged);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(12, 363);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(54, 23);
            this.searchButton.TabIndex = 10;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(52, 396);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(99, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Suggestions on";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // listDataEX
            // 
            this.listDataEX.FormattingEnabled = true;
            this.listDataEX.Location = new System.Drawing.Point(323, 174);
            this.listDataEX.Name = "listDataEX";
            this.listDataEX.Size = new System.Drawing.Size(200, 160);
            this.listDataEX.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(324, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Incoming Data:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Local Data:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(218, 340);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Console";
            // 
            // dataSize
            // 
            this.dataSize.Location = new System.Drawing.Point(409, 150);
            this.dataSize.Name = "dataSize";
            this.dataSize.Size = new System.Drawing.Size(100, 20);
            this.dataSize.TabIndex = 16;
            this.dataSize.Text = "Data Size (KB)";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(71, 363);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(54, 23);
            this.clearButton.TabIndex = 17;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // clearConsole
            // 
            this.clearConsole.Location = new System.Drawing.Point(130, 363);
            this.clearConsole.Name = "clearConsole";
            this.clearConsole.Size = new System.Drawing.Size(82, 23);
            this.clearConsole.TabIndex = 18;
            this.clearConsole.Text = "Clear Console";
            this.clearConsole.UseVisualStyleBackColor = true;
            this.clearConsole.Click += new System.EventHandler(this.clearConsole_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 429);
            this.Controls.Add(this.clearConsole);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.dataSize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listDataEX);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.textSearch);
            this.Controls.Add(this.listUnique);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listMessage);
            this.Controls.Add(this.textMessage);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "PageCacheSyncher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textLocalPort;
        private System.Windows.Forms.TextBox textLocalIP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textFriendPort;
        private System.Windows.Forms.TextBox textFriendIP;
        private System.Windows.Forms.TextBox textMessage;
        private System.Windows.Forms.ListBox listMessage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listUnique;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ListBox listDataEX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox dataSize;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button clearConsole;
    }
}

