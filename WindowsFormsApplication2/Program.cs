using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Login loginForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            loginForm = new Login();
            Application.Run(loginForm);
        }
    }

    class Connection()
    {
        private TcpClient tcpClient;
        public string ip { get; set; }

        public void Login(string login, string password, string ip)
        {
            CChat_Library.Objects.Packet Pack = new CChat_Library.Objects.Packet();
            Pack.Flag = CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_REQUEST_HANDSHAKE;
            Pack.Data = new CChat_Library.Objects.Packets.Handshake
            {
                username = login,
                password = password

            };
             tcpClient = new TcpClient(ip, 31337);
             this.ip = ip;
             sendPacket(Pack);
             Thread Comm = new Thread(new ParameterizedThreadStart(HandleCommunication));
             Comm.Start(tcpClient);
             this.login = login;
             this.pass = password;

        }
    }
}
