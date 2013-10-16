using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Program
    {
        public static List<Client> clients;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public void sendMessage(string reciever, string message)
        {
            CChat_Library.Objects.Packet pack = new CChat_Library.Objects.Packet();
            pack.Flag = CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_CHAT;
            pack.Data = new CChat_Library.Objects.Packets.ChatMessage()
            {
                Sender = "",
                Reciever = reciever,
                Chat = message
            };


        }


    }
    class Connection
    {
        private TcpClient tcpClient;


        public void Login(string login, string password, string ip)
        {
            CChat_Library.Objects.Packet Pack = new CChat_Library.Objects.Packet();
            Pack.Flag = CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_REQUEST_HANDSHAKE;
            Pack.Data = new CChat_Library.Objects.Packets.Handshake()
            {
                username = login,
                password = password

            };
            tcpClient = new TcpClient(ip, 31337);
            this.ip = ip;
            stream = new SslStream(tcpClient.GetStream(), false, new System.Net.Security.RemoteCertificateValidationCallback(checkCert), null);
            try
            {
                stream.AuthenticateAsClient(ip);
            }
            catch
            {
            }
            sendPacket(Pack);
            Thread Comm = new Thread(new ParameterizedThreadStart(HandleCommunication));
            Comm.Start(tcpClient);

            //temp code for testing without server
            //Program.form2.denied(2);
            Program.form2.Close();
            //clientStream.Write(), 0, );
        }

        private void sendPacket(CChat_Library.Objects.Packet Pack)
        {
            throw new NotImplementedException();
        }




        public void HandleCommunication(object tcp)
        {
            TcpClient tcpClient = (TcpClient)tcp;


            while (true)
            {
                CChat_Library.Objects.Packet packet;
                try
                {
                    if (tcpClient.GetStream() != null)
                    {
                        if (tcpClient.GetStream().CanRead)
                        {
                            try
                            {
                                packet = (CChat_Library.Objects.Packet)new BinaryFormatter().Deserialize(stream);
                                handlePacket(packet);
                            }

                            catch
                            {
                                Console.WriteLine("Packet lost");
                            }
                        }
                        else
                        {
                            Console.WriteLine("broke connection");
                            break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("broke connection");
                        break;
                    }
                }
                catch
                {
                    tcpClient = new TcpClient(ip, 31337);
                    Console.WriteLine("socket was dropped");
                }

            }

            //tcpClient.Close();
        }


        private void handlePacket(CChat_Library.Objects.Packet packet)
        {
            switch (packet.Flag)
            {
                case CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_RESPONSE_USERLIST:
                    List<String> users = (List<String>)packet.Data;
                    if (Program.form1.InvokeRequired)
                    {
                        Program.form1.Invoke(new Action(() => Program.form1.updateUsers(users)));
                    }

                    break;
                case CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_CHAT:
                    CChat_Library.Objects.Packets.ChatMessage chatMess = (CChat_Library.Objects.Packets.ChatMessage)packet.Data;
                    Console.WriteLine(chatMess.Sender.ToString());
                    foreach (Client client in Program.clients)
                    {
                        if (client.getName().Equals(chatMess.Sender.ToString()))
                        {
                            client.recieveChat(chatMess.Chat, chatMess.Sender.ToString());
                            int o = 0;
                            for (int i = 0; i < Program.clients.Count; i++)
                            {
                                if (Program.clients[i].getName().Equals(chatMess.Sender))
                                {
                                    o = i;
                                    break;
                                }
                            }
                            if (Program.form1.selectedReciever == o)
                            {
                                if (Program.form1.InvokeRequired)
                                {
                                    Program.form1.Invoke(new Action(() => Program.form1.refreshChat()));
                                }
                            }
                        }
                    }
                    break;
                case CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_RESPONSE_HANDSHAKE:
                    CChat_Library.Objects.Packets.ResponseHandshake handshake = (CChat_Library.Objects.Packets.ResponseHandshake)packet.Data;

                    switch (handshake.Result)
                    {
                        case CChat_Library.Objects.Packets.ResponseHandshake.ResultType.RESULTTYPE_ACCESSDENIED:
                            Program.form2.denied(2);
                            break;
                        case CChat_Library.Objects.Packets.ResponseHandshake.ResultType.RESULTTYPE_INVALIDCREDENTIALS:
                            Program.form2.denied(1);
                            break;
                        case CChat_Library.Objects.Packets.ResponseHandshake.ResultType.RESULTTYPE_OK:
                            Program.form2.Hide();
                            Thread Comm = new Thread(form2);
                            Comm.Start();
                            Program.clients = new List<Client>();
                            requestUsers();
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("packet not recognized");
                    break;
            }
        }
        

        public void requestUsers()
        {
            //BinaryFormatter format = new BinaryFormatter();
            //format.Serialize(tcpClient.GetStream(), Package);
        }


        public void sendMessage(string message, string reciever)
        {
            ChatMessage chatMessage = new ChatMessage()
            {
                Receiver = reciever,
                Sender = "Jim",
                Message = message
            };
            BinaryFormatter format = new BinaryFormatter();
            format.Serialize(tcpClient.GetStream(), chatMessage);
        }

        public string ip { get; set; }
    }

    class Client
    {
        protected string naam;
        protected string chatLog;

        public Client(string naam)
        {
            this.naam = naam;
            chatLog = "";
        }

        public void recieveChat(string message, string sender)
        {
            this.chatLog += "[" + DateTime.Now.ToShortTimeString() + "] " + sender + ": " + message + "\n";
        }

        public string getChat()
        {
            return chatLog;
        }

        public string getName()
        {
            return naam;
        }
    }

    [Serializable]
    public class ChatMessage
    {
        /// <summary>
        /// The sender of the packet
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// The receiver of the packet
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// The chat message
        /// </summary>
        public string Message { get; set; }
    }
    [Serializable]
    public class Packet
    {
        /// <summary>
        /// The packet flag
        /// </summary>
        public enum PacketFlag
        {
            PACKETFLAG_REQUEST_HANDSHAKE,
            PACKETFLAG_RESPONSE_HANDSHAKE,
            PACKETFLAG_CHAT,
            PACKETFLAG_BIKECONTROL,
            PACKETFLAG_VALUES,
            PACKETFLAG_REQUEST_VALUES,
            PACKETFLAG_RESPONSE_VALUES,
            PACKETFLAG_REQUEST_USERLIST,
            PACKETFLAG_RESPONSE_USERLIST
        }

        /// <summary>
        /// The packet data
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// The flag of the packet
        /// </summary>
        public PacketFlag Flag { get; set; }
    }
}
