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
    public partial class LoginWindow : Form
    {
        Connection connect;
        public LoginWindow()
        {
            InitializeComponent();
            Program.connect = new Connection();
            connect = Program.connect;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Program.chatWindow.openForm();
                    connect.Login(textBox1.Text, textBox2.Text, textBox3.Text);
                    this.Close();
                    
                }

                catch
                {
                    MessageBox.Show("Server is unavailable, it is probably offline, please try again later.", "Server unavailable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        internal void denied(int errorCode)
        {
            switch (errorCode)
            {
                case 1:
                    MessageBox.Show("Invalid username and/or password!", "Invalid credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 2:
                    MessageBox.Show("User already in use!", "Acces denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 3:
                    MessageBox.Show("Your username already exists, your new name is automagicly assigned", "Username Conflict", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
         }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connect.Login(textBox1.Text, textBox2.Text, textBox3.Text);
                this.Close();
            }

            catch
            {
                MessageBox.Show("Server is unavailable, it is probably offline, please try again later.", "Server unavailable", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Program.chatWindow.openForm();
                    connect.Login(textBox1.Text, textBox2.Text, textBox3.Text);
                    this.Close();
                }

                catch
                {
                    MessageBox.Show("Server is unavailable, it is probably offline, please try again later.", "Server unavailable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
    }
}
