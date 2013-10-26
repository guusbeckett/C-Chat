using ClientApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{

    static class Program
    {
        public static ChatWindow chatWindow;
        public static LoginWindow loginWindow;
        public static Connection connect;
        public static List<Client> clients;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            loginWindow = new LoginWindow();
            chatWindow = new ChatWindow();
            Application.Run(loginWindow);
        }
    }


    }
    class Connection
    {
        private TcpClient tcpClient;

        public void Login(string login, string password, string ip)
        {
            CChat_Library.Objects.Packet Pack = new CChat_Library.Objects.Packet();
            Pack.Flag = CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_REQUEST_HANDSHAKE;
            Pack.Data = new CChat_Library.Objects.Packets.Handshake
            {
                username = login,
                password = password

            };
             tcpClient = new TcpClient(ip, 1994);
             this.ip = ip;
             sendPacket(Pack);
             logged = true;
             Thread Comm = new Thread(new ParameterizedThreadStart(HandleCommunication));
             Comm.Start(tcpClient);
             lost = 0;
             this.login = login;
             this.pass = password;
             Program.chatWindow.clientName = login;
        }

        private void form2()
        {
            Application.Run(Program.chatWindow);
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
                                packet = (CChat_Library.Objects.Packet)new BinaryFormatter().Deserialize(tcpClient.GetStream());
                                handlePacket(packet);
                                lost = 0;
                            }

                            catch
                            {
                                if (logged)
                                {
                                    ++lost;
                                    Console.WriteLine("Packet lost");
                                    if (lost > 5)
                                    {
                                        logged = false;
                                        Program.connect.Login(login, pass, ip);
                                        break;
                                    }
                                }
                                else
                                {
                                    Program.loginWindow.denied(3);
                                }
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
                    Console.WriteLine("socket was dropped");
                }

            }

            //tcpClient.Close();
        }

        private void handlePacket(CChat_Library.Objects.Packet packet)
        {
            Console.WriteLine("Packet ontvangen");
            switch (packet.Flag)
            {
                case CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_RESPONSE_USERLIST:
                    List<String> users = (List<String>)packet.Data;
                    if (Program.chatWindow.InvokeRequired)
                    {
                        Program.chatWindow.Invoke(new Action(() => Program.chatWindow.updateUsers(users)));
                    }
                    
                    break;
                case CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_CHAT:
                    CChat_Library.Objects.Packets.ChatMessage chatMess = (CChat_Library.Objects.Packets.ChatMessage)packet.Data;
                    foreach (Client client in Program.clients)
                    {
                        if (client.getName().Equals(chatMess.Sender.ToString()))
                        {
                            client.recieveChat(chatMess.Chat, chatMess.Sender);
                            int o = 0;
                            for (int i = 0; i < Program.clients.Count; i++)
                            {
                                if (Program.clients[i].getName().Equals(chatMess.Sender))
                                {
                                    o = i;
                                    break;
                                }
                            }
                        

                            if (Program.chatWindow.selectedReciever.Equals(null))
                            {
                                if (Program.chatWindow.InvokeRequired)
                                {
                                    Program.chatWindow.Invoke(new Action(() => Program.chatWindow.refreshChat()));
                                }
                            }
                        }
                    }
                    if (chatMess.Reciever.Equals("ALL"))
                    {
                        Program.chatWindow.recieveChat("ALL", chatMess.Chat);
                    }
                    break;
               case CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_RESPONSE_HANDSHAKE:
                    CChat_Library.Objects.Packets.ResponseHandshake handshake = (CChat_Library.Objects.Packets.ResponseHandshake)packet.Data;
               switch (handshake.Result)
                    {
                        case CChat_Library.Objects.Packets.ResponseHandshake.ResultType.RESULTTYPE_ACCESSDENIED:
                            Program.loginWindow.denied(2);
                            break;
                        case CChat_Library.Objects.Packets.ResponseHandshake.ResultType.RESULTTYPE_INVALIDCREDENTIALS:
                            Program.loginWindow.denied(1);
                            break;
                        case CChat_Library.Objects.Packets.ResponseHandshake.ResultType.RESULTTYPE_USER_EXISTS:
                            Program.loginWindow.denied(3);
                            break;
                        case CChat_Library.Objects.Packets.ResponseHandshake.ResultType.RESULTTYPE_OK:
                            //Program.form2.Hide();
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
            CChat_Library.Objects.Packet Pack = new CChat_Library.Objects.Packet();
            Pack.Flag = CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_REQUEST_USERLIST;
            //Pack.Data 
            sendPacket(Pack);
        }

        public void sendPacket(CChat_Library.Objects.Packet packet)
        {
            if (packet != null)
            {
                BinaryFormatter format = new BinaryFormatter();
                format.Serialize(tcpClient.GetStream(), packet);
            }
        }

        public void sendMessage(string message, string reciever, string sender)
        {
            CChat_Library.Objects.Packet Pack = new CChat_Library.Objects.Packet();
            Pack.Flag = CChat_Library.Objects.Packet.PacketFlag.PACKETFLAG_CHAT;
            Pack.Data = new CChat_Library.Objects.Packets.ChatMessage()
            {
                Reciever = reciever,
                Sender = sender,
                Chat = message
            };
            sendPacket(Pack);
        }


        public string ip { get; set; }

        public int lost { get; set; }

        public bool logged { get; set; }

        public string login { get; set; }

        public string pass { get; set; }

        
    }

    class Client
    {
        protected string naam;
        protected string chatLog;
        private CChat_Library.Objects.UserStatus.Status status;

        public Client(string naam)
        {
            this.naam = naam;
            chatLog = "";
        }

        public void setStatus(CChat_Library.Objects.UserStatus.Status statuus)
        {
            status = statuus;
        }

        public CChat_Library.Objects.UserStatus.Status getStatus()
        {
            return status;
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

