using System.IO;
using System.Net;
using System;
using System.Threading;
using Chat = System.Net;
using System.Collections;
using ConsoleApplication3;



namespace ConsoleApplication3
{
    class CommunicatieProt
    {
        private String nickName;
        private StreamWriter writer;
        private StreamReader reader;
        private System.Net.Sockets.TcpClient client;

        public CommunicatieProt(System.Net.Sockets.TcpClient tcpClient)
        {
	        //tcp client maken
	        client = tcpClient;
	        //maakt thread
	        Thread chatThread = new Thread(new ThreadStart(startChat));
	        //start threadje
	        chatThread.Start();
        }

        private string GetNick()
        {
            //Vraag naar de id van de gebruiker
            writer.WriteLine("Wat is je gebruikersnaam? ");
            //ensure the buffer is empty
            writer.Flush();
            //return the value the user provided
            return reader.ReadLine();
        }

        private void startChat()
        {
            reader = new System.IO.StreamReader(client.GetStream());
            writer = new System.IO.StreamWriter(client.GetStream());
            writer.WriteLine("Welcome to PCChat!");
            nickName = GetNick();
            while (ConsoleApplication3.ChatServer.id.Contains(nickName))
            {
                writer.WriteLine("ERROR - Gebruikersnaam bestaat al");
                nickName = GetNick();
            }
            ConsoleApplication3.ChatServer.id.Add(nickName, client);
            ConsoleApplication3.ChatServer.idConnect.Add(client, nickName);
            ConsoleApplication3.ChatServer.SendSystemMessage("** " + nickName + " ** heeft de chat gejoint");
            writer.WriteLine("Chateuden.....\r\n-------------------------------");
            writer.Flush();
            Thread chatThread = new Thread(new ThreadStart(runChat));
            chatThread.Start();
        }

        private void runChat()
        {
            try
            {
                //maak line leeg.
                string line = "";
                while (true)
                {
                    //read the curent line
                    line = reader.ReadLine();
                    //send our message
                    ConsoleApplication3.ChatServer.SendMsgToAll(nickName, line);
                }
            }
            catch (Exception e44)
            {
                Console.WriteLine(e44);
            }
        }
    }
}
