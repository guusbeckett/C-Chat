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
            richTextBoxChat.Text += textBoxSend.Text + "\n";
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
            Program.connect.sendMessage(textBoxSend.Text,"ALL",clientName);
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
                if (user == null) break;
                listBoxRecievers.Items.Add(user);
            }
            
        }

        private void listBoxRecievers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxRecievers_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedReciever = this.listBoxRecievers.SelectedValue.ToString();
        }

        public void selectedRecieverUpdate()
        {
            
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
                richTextBoxChat.Text += message + "\n";
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

        private void ChatWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
