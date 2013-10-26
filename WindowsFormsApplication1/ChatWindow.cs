using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ChatWindow : Form
    {
        public string selectedReciever;
        public string clientName;
        public ChatWindow()
        {
            InitializeComponent();
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
            if (selectedReciever != null)
            {
                foreach (Client clie in Program.clients)
                {
                       
                    if(clie.getName().Equals(selectedReciever)) richTextBoxChat.Text = clie.getChat();
                }
            }
        }

        private void sendChat()
        {
            if (listBoxRecievers.SelectedIndex <= 0) Program.connect.sendMessage(textBoxSend.Text, "ALL", clientName);
            else Program.connect.sendMessage(textBoxSend.Text,selectedReciever,clientName);
        }

        public void setClientName(string clientNaam)
        {
            clientName = clientNaam;
        }


        public void updateUsers(List<string> users)
        {
            listBoxRecievers.Items.Clear();
            foreach(string user in users)
            {
                if (!user.Equals(clientName)) listBoxRecievers.Items.Add(user);
            }
            
        }

        private void listBoxRecievers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxRecievers_SelectedValueChanged(object sender, EventArgs e)
        {
            richTextBoxChat.Text = "";
            selectedReciever = this.listBoxRecievers.SelectedItem.ToString();
            this.labelActiveClient.Text = selectedReciever;
            foreach (Client klient in Program.clients)
            {
                foreach (Client cl in Program.clients)
                {
                    if (cl.getName().Equals(listBoxRecievers.SelectedItem))
                    {
                        try
                        {
                            Program.chatWindow.Invoke(new Action(() => setChatText(klient.getChat())));
                        }
                        catch { }
                    }
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
        }

        private void setChatText(string text)
        {
            richTextBoxChat.Text = text;
        }

        private void ChatWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
