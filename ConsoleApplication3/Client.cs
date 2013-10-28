using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using CChat_Library.Objects;
using CChat_Library.Objects.Packets;

namespace ConsoleApplication3
{

    class Client
    {

        //
        private Thread clientThread;
        private TcpClient clientTcp;
        private ChatServer server;
        public String user;
        public String password;
        public CChat_Library.Objects.UserStatus.Status status;

        //

        public Client(TcpClient client, ChatServer server)
        {
            this.clientTcp = client;
            this.server = server;
            clientThread = new Thread(new ThreadStart(streamHandler));
            clientThread.Start();
        }

        public void streamHandler()
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            CChat_Library.Objects.Packet pack = null;
            for (; ; )
            {
                try
                {
                    pack = binaryFormatter.Deserialize(clientTcp.GetStream()) as CChat_Library.Objects.Packet;
                    CommunicatieHandler.readPacket(server, this, pack);
                    Thread.Sleep(10);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Closed as exception: " + e);
                    binaryFormatter = null;
                    server.removeFromList(this);
                    server.refreshListForAll();
                    close();
                }

            }
        }

        private void close()
        {
            try
            {
                //clientTcp.GetStream().Close();
                //Console.WriteLine("Deze gaat nog");
                clientTcp.Close();
                clientThread.Abort();
            }
            catch(Exception e)
            {
               throw e;
            }
        }

        public void setStatus(CChat_Library.Objects.UserStatus.Status status)
        {
            this.status = status;
        }
    
        public void setUsernamePassword(string u, string p)
        {
            this.password = p;
            this.user = u;
        }

        internal void sendHandler(Packet responsePack)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            try
            {
                //Console.WriteLine("Send: " + responsePack.Flag);
                //Console.WriteLine("Send: " + responsePack.Data);
                formatter.Serialize(clientTcp.GetStream(), responsePack);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + e);
            }
        }
    }
}
