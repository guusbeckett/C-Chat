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
    public partial class Form1 : Form
    {
        public Form1()
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
            textBoxSend.Clear();
        }

        private void textBoxSend_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enterOrButtonPress();
            }
        }


    }
}
