using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CChat_Library;
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class ChatWindow : Form
    {
        public string selectedReciever;
        public string clientName;
        private Dictionary<string, CChat_Library.Objects.UserStatus.Status> statusDict = new Dictionary<string, CChat_Library.Objects.UserStatus.Status>() { {"Online", CChat_Library.Objects.UserStatus.Status.STATUS_ONLINE },{"Busy",CChat_Library.Objects.UserStatus.Status.SATUS_BUSY},{"Away",CChat_Library.Objects.UserStatus.Status.STATUS_AWAY},{"Offline", CChat_Library.Objects.UserStatus.Status.STATUS_OFFLINE} };

        public ChatWindow()
        {
            InitializeComponent();
            //comboBox1.SelectedIndex = 0; 
            playStartUpSpound();
        }

        

        private void buttonSend_Click(object sender, EventArgs e)
        {
            enterOrButtonPress();
        }

        private void enterOrButtonPress()
        {
            sendChat();
            textBoxSend.Clear();
        }

        private void textBoxSend_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enterOrButtonPress();
            }
        }

        internal void refreshChat()
        {
            
                loadChat();
            
        }

        private void sendChat()
        {
            if (listBoxRecievers.SelectedIndex < 0) Program.connect.sendMessage(textBoxSend.Text, "ALL", clientName);
            else
            {
                Program.connect.sendMessage(textBoxSend.Text, selectedReciever, clientName);
                foreach(Client jimDeKanarie in Program.clients)
                {
                    if(jimDeKanarie.getName().Equals(selectedReciever))
                    {
                        jimDeKanarie.recieveChat(textBoxSend.Text, "Me");
                    }
                }
                refreshChat();
            }

        }

        public void setClientName(string clientNaam)
        {
            clientName = clientNaam;
            Program.connect.sendMessage(clientName + " is now online.", "ALL", clientName);
        }


        public void updateUsers(List<string> users)
        {
            listBoxRecievers.Items.Clear();
            foreach(string user in users)
            {
                if (!user.Equals(clientName)) listBoxRecievers.Items.Add(user);

            }
            if(listBoxRecievers.SelectedValue == null)
            {
                richTextBoxChat.Text = "";
            }

        }
            
        

        private void listBoxRecievers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxRecievers_SelectedValueChanged(object sender, EventArgs e)
        {
            richTextBoxChat.Text = "";
            if (!listBoxRecievers.SelectedIndex.Equals(-1))
            {
                loadChat();
            if (selectedReciever.Equals(userFormNotification))
            {
                labelUserSendMessage.Text = "";
            }
            }
            foreach(Client client in Program.clients)
            {
                if(client.getName().Equals(selectedReciever))
                {
                    updateStatus(client.getStatus());
                }
            }
        }

        private void loadChat()
        {
            selectedReciever = this.listBoxRecievers.SelectedItem.ToString();
            this.labelActiveClient.Text = selectedReciever;
            foreach (Client klient in Program.clients)
            {
                if (klient.getName().Equals(listBoxRecievers.SelectedItem))
                {
                    try
                    {
                        Program.chatWindow.Invoke(new Action(() => setChatText(klient.getChat())));
                    }
                    catch { }
                    setChatText(klient.getChat());
                    break;
                }
            }
        }

        public void recieveChat(string sender,string message)
        {
            if (Program.chatWindow.InvokeRequired)
            {
                try {
                    Program.chatWindow.Invoke(new Action(() => procesChat(sender,message)));
                }
                catch { }
             
            }
            richTextBoxChat.SelectionStart = richTextBoxChat.Text.Length;
            richTextBoxChat.ScrollToCaret();
        }
        private void procesChat(string sender, string message)
        {
            if (sender.Equals("ALL"))
            {
                richTextBoxChat.Text += "Broadcast ["+DateTime.Now.ToShortTimeString()+"]: "+ message + "\n";
                richTextBoxChat.Update();
            }
            else
                foreach (Client cl in Program.clients)
                {
                    if (cl.getName().Equals(sender))
                    {
                        cl.recieveChat(message, sender);
                    }
                }
            richTextBoxChat.SelectionStart = richTextBoxChat.Text.Length;
            richTextBoxChat.ScrollToCaret();
            
        }

        private void setChatText(string text)
        {
            richTextBoxChat.Text = text;
            richTextBoxChat.SelectionStart = richTextBoxChat.Text.Length;
            richTextBoxChat.ScrollToCaret();
        }

        private void ChatWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CChat_Library.Objects.UserStatus.Status sendStatus = CChat_Library.Objects.UserStatus.Status.STATUS_ONLINE;
            foreach (KeyValuePair<string,CChat_Library.Objects.UserStatus.Status> stat in statusDict)
            {
                if (comboBoxStatus.SelectedIndex >= 0)
                {
                    if (stat.Key.Equals(comboBoxStatus.SelectedItem.ToString()))
                    {
                        sendStatus = stat.Value;
                        break;
                    }
                }

            }
            Program.connect.changeStatus(sendStatus);
        }

        public void updateStatus(CChat_Library.Objects.UserStatus.Status vlag)
        {
            switch (vlag)
            {
                case CChat_Library.Objects.UserStatus.Status.STATUS_ONLINE:
                    //button1.BackColor = Color.Green;
                    pictureBox1.ImageLocation = "online.png";
                    break;
                case CChat_Library.Objects.UserStatus.Status.STATUS_OFFLINE:
                    //button1.BackColor = Color.Gray;
                    pictureBox1.ImageLocation = "offline.png";
                    break;
                case CChat_Library.Objects.UserStatus.Status.STATUS_AWAY:
                    //button1.BackColor = Color.Orange;
                    pictureBox1.ImageLocation = "away.png";
                    break;
                case CChat_Library.Objects.UserStatus.Status.SATUS_BUSY:
                    //button1.BackColor = Color.Red;
                    pictureBox1.ImageLocation = "busy.png";
                    break;
            }
        }

        internal void playStartUpSpound()
        {
            (new SoundPlayer(@"startup.wav")).Play();
        }

        internal void playNotificationSound()
        {
            (new SoundPlayer(@"notification.wav")).Play();
        }

        private void ChatWindow_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        public void openForm()
        {
            comboBoxStatus.SelectedIndex = 0;
        }

        public void recieveMessageNotification(string user)
        {
            userFormNotification = user;
            Program.chatWindow.playNotificationSound();
            labelUserSendMessage.Text = user + " just send you a message!";
        }

        public string userFormNotification { get; set; }

        internal void updateStatus(CChat_Library.Objects.Packets.ChangeStatus statPack)
        {
            foreach (Client client in Program.clients)
            {
                if (client.getName().Equals(statPack.clientName)&&!client.getName().Equals(clientName))
                {
                    Program.chatWindow.updateStatus(statPack.status);
                    break;
                }
                if (client.getName().Equals(statPack.clientName))
                {
                    client.setStatus(statPack.status);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.File.WriteAllText(saveFileDialog.FileName, richTextBoxChat.Text);
        }
    }


}


