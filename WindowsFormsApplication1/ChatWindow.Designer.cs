namespace WindowsFormsApplication1
{
    partial class ChatWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatWindow));
            this.listBoxRecievers = new System.Windows.Forms.ListBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.labelActiveClient = new System.Windows.Forms.Label();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.labelUserSendMessage = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxRecievers
            // 
            this.listBoxRecievers.FormattingEnabled = true;
            this.listBoxRecievers.Location = new System.Drawing.Point(12, 93);
            this.listBoxRecievers.Name = "listBoxRecievers";
            this.listBoxRecievers.Size = new System.Drawing.Size(120, 394);
            this.listBoxRecievers.TabIndex = 0;
            this.listBoxRecievers.SelectedIndexChanged += new System.EventHandler(this.listBoxRecievers_SelectedIndexChanged);
            this.listBoxRecievers.SelectedValueChanged += new System.EventHandler(this.listBoxRecievers_SelectedValueChanged);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(463, 501);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 20);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(138, 502);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(319, 20);
            this.textBoxSend.TabIndex = 2;
            this.textBoxSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSend_KeyDown_1);
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.Location = new System.Drawing.Point(138, 93);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxChat.Size = new System.Drawing.Size(400, 402);
            this.richTextBoxChat.TabIndex = 3;
            this.richTextBoxChat.Text = "";
            // 
            // labelActiveClient
            // 
            this.labelActiveClient.AutoSize = true;
            this.labelActiveClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelActiveClient.Location = new System.Drawing.Point(12, 42);
            this.labelActiveClient.Name = "labelActiveClient";
            this.labelActiveClient.Size = new System.Drawing.Size(324, 37);
            this.labelActiveClient.TabIndex = 4;
            this.labelActiveClient.Text = "Select a friend to talk!";
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DisplayMember = "0";
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "Online",
            "Busy",
            "Away",
            "Offline"});
            this.comboBoxStatus.Location = new System.Drawing.Point(12, 501);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(120, 21);
            this.comboBoxStatus.TabIndex = 6;
            this.comboBoxStatus.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelUserSendMessage
            // 
            this.labelUserSendMessage.AutoSize = true;
            this.labelUserSendMessage.Location = new System.Drawing.Point(12, 9);
            this.labelUserSendMessage.Name = "labelUserSendMessage";
            this.labelUserSendMessage.Size = new System.Drawing.Size(0, 13);
            this.labelUserSendMessage.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(460, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 67);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ChatWindow
            // 
            this.AcceptButton = this.buttonSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 530);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelUserSendMessage);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.labelActiveClient);
            this.Controls.Add(this.richTextBoxChat);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.listBoxRecievers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatWindow";
            this.Text = "C-Chat by Mackett";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatWindow_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.ChatWindow_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxRecievers;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.Label labelActiveClient;
        public System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label labelUserSendMessage;
        private System.Windows.Forms.Button button1;
    }
}

