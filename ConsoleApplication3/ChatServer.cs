using System.IO;
using System.Net;
using System;
using System.Threading;
using Chat = System.Net;
using System.Collections;
using System.Net.Sockets;

namespace ConsoleApplication3
{
    class ChatServer
    {
        System.Net.Sockets.TcpListener chatServer;
        public static Hashtable id;
        public static Hashtable idConnect;
        public static StreamWriter log;
        private static DateTime datum;

        public ChatServer()
        {
            id = new Hashtable(100);
            idConnect = new Hashtable(100);
            chatServer = new TcpListener(4296);
            createLog();
            Console.WriteLine("Wachten op Clients");
            Thread connectThread = new Thread(new ThreadStart(connectionThread));
            connectThread.Start();
            while (true)
            {
                String cnsmsg = Console.ReadLine();
                if (cnsmsg.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("App word afgesloten");
                    break;
                }

                else if (cnsmsg.Equals("Help", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("------\r\nDit is de Help informatie:\r\n\r\nExit: Stop het programma\r\nSave: Sla de chat log op\r\nLoad: Laad de chat log\r\n------");
                }

                else if (cnsmsg.Equals("Save", StringComparison.OrdinalIgnoreCase))
                {
                    SaveLog();
                    Console.WriteLine("Log opgeslagen op de volgende locatie: ");
                }

                else if (cnsmsg.Equals("Load", StringComparison.OrdinalIgnoreCase))
                {
                    Loadlog();
                    Console.WriteLine("Log geladen");
                }
                else Console.WriteLine("Commando niet bekend");
            }
            
        }


        public void connectionThread()
        {
            try
            {
             while (true)
            {
                //start de chat servertje
                chatServer.Start();
                //kijk voor connecties
                if (chatServer.Pending())
                {
                    //wacht op connecties
                    Chat.Sockets.TcpClient chatConnection = chatServer.AcceptTcpClient();
                    //laat weten dat er verbinding is
                    Console.WriteLine("Client geconnect");
                    //maak nieuw protje aan
                    CommunicatieHandler comm = new CommunicatieHandler(chatConnection);
                }
            }
            }
            catch (Exception e3)
            {
                Console.WriteLine(e3);
            }
        }

        public static void SendMsgToAll(string nick, string msg)
        {
            StreamWriter writer; //Maak Writer aan
            ArrayList ToRemove = new ArrayList(0); 

            //Nieuwe array van TCP Clients aanmaken.
            Chat.Sockets.TcpClient[] tcpClient = new Chat.Sockets.TcpClient[ChatServer.id.Count];

            //Maak lijst met tcp Clients aan op basis van ID's 
            ChatServer.id.Values.CopyTo(tcpClient, 0);
         
            for (int cnt = 0; cnt < tcpClient.Length; cnt++)
            {
                try
                {
                    //kijk of Berichtje leeg is of null. Dan moet hij gewoon doorgaan.
                    if (msg.Trim() == "" || tcpClient[cnt] == null)
                    continue;

                    // Maak de StreamWriter aan.
                    writer = new StreamWriter(tcpClient[cnt].GetStream());

                    //Druk bericht af met id er voor.
                    writer.WriteLine(id + ": " + msg);


                    //Nu er voor zorgen dat alles verstuurd is en clean is nu.
                    writer.Flush();

                    writer = null; //zet de writer weer op pauze. Wacht tot ie weer nodig is.
                }
                catch (Exception e2) //is de exeption als iemand uit de chat gaat. Die wordt hier opgevangen. (Leave)
                {
                    Console.WriteLine(e2);
                    string str = (string)ChatServer.idConnect[tcpClient[cnt]];
                    
                    ChatServer.SendSystemMessage("\"" + str + "\"" + " heeft het gesprek verlaten"); //Druk een bericht af in de chat dat de "Gebruiker" weg is.

                    ChatServer.id.Remove(str); // verwijder gebruiker uit de lijst.

                    ChatServer.idConnect.Remove(tcpClient[cnt]);// verwijder uit de array voor nieuwe connector.


                }
            }
        }

        public static void SendSystemMessage(string msg)
        {
            StreamWriter writer; //writer weer aanmaken en alles het zelfde alleen nu door system een message
            ArrayList ToRemove = new ArrayList(0);
            Chat.Sockets.TcpClient[] tcpClient = new Chat.Sockets.TcpClient[ChatServer.id.Count];
            ChatServer.id.Values.CopyTo(tcpClient, 0);

            for (int i = 0; i < tcpClient.Length; i++)
            {
                try
                {
                    if (msg.Trim() == "" || tcpClient[i] == null)
                        continue;

                    writer = new StreamWriter(tcpClient[i].GetStream());
                    writer.WriteLine(msg);
                    writer.Flush();
                    writer = null;

                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1);
                    ChatServer.id.Remove(ChatServer.idConnect[tcpClient[i]]);
                    ChatServer.idConnect.Remove(tcpClient[i]);
                }
            }
        }

        public static void SaveLog()
        {

        }

        public static void Loadlog()
        {

        }

        public static void createLog()
        {
            datum = new DateTime();
            datum = DateTime.Today;
            String name = datum.ToString();
            name = name.Replace(":", " ");
            name = "./Logs/" + name + ".txt";
            Console.WriteLine(name);
            log = new StreamWriter(name);
        }

    }
}