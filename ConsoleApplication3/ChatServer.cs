using System.IO;
using System.Net;
using System;
using System.Threading;
using Chat = System.Net;
using System.Collections;
using System.Net.Sockets;
using System.Diagnostics;
using CChat_Library.Objects;
using CChat_Library.Objects.Packets;
using System.Collections.Generic;


namespace ConsoleApplication3
{

    class ChatServer
    {
        //
        private TcpListener tcpListener;
        private List<Client> clientList = new List<Client>();
        int count = 1;


       //


        public ChatServer()
        {
            tcpListener = new TcpListener(System.Net.IPAddress.Any, 1994);
            clientzoeker();
        }

        public void clientzoeker()
        {
            tcpListener.Start();
            TcpClient clientZoeker;
            for (; ; )
            {
                try
                {
                    Console.WriteLine("Zoeken naar clients...");
                    clientZoeker = tcpListener.AcceptTcpClient();
                    newClient(clientZoeker);
                    Console.WriteLine("Client connected");
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Error in client zoeken");
                }
                Thread.Sleep(10);
            }
        }

        public void newClient(TcpClient client)
        {
            new Client(client, this);
        }


    
        public void sendMessage(Packet _packet,Client _client)
        {
            ChatMessage msg = (ChatMessage)_packet.Data;

            saveLogMsg(msg.Chat, _client.user);

            Console.WriteLine(msg.Chat);

            foreach (Client client in clientList)
            {
                if (client.user == msg.Reciever || msg.Reciever == "ALL")
                {
                    client.sendHandler(_packet);
                    return;
                }
            }
        }

        public void refreshListForAll()
        {
            Packet packet = getOnlineList();
            foreach (Client client in clientList)
            {
                client.sendHandler(packet);
            }
        }
        
        public void handshakeResponse(Client _client, Handshake _handshake)
        {
            count = 1;
            ResponseHandshake antwoord = new ResponseHandshake();
            //antwoord.Result = ResponseHandshake.ResultType.RESULTTYPE_ACCESSDENIED;
            if(_client.user.Equals("ALL"))
            {
                antwoord.Result = ResponseHandshake.ResultType.RESULTTYPE_INVALIDCREDENTIALS;
            }
            if (clientList.Count == 0)
            {
                Console.WriteLine("Geen users online, creating List");
                clientList.Add(_client);
                antwoord.Result = ResponseHandshake.ResultType.RESULTTYPE_OK;
            }
            else
            {
                foreach (Client client in clientList)
                {
                    if (client.user.Equals(_client.user))
                    {
                        //Console.WriteLine("Response is EXISTS");
                        antwoord.Result = ResponseHandshake.ResultType.RESULTTYPE_USER_EXISTS;
                        changeName(_client, client);
                        clientList.Add(_client);
                        antwoord.givenUsername = _client.user;
                        break;
                    }
                    else
                    {
                        //Console.WriteLine("Response is OK");
                        clientList.Add(_client);
                        antwoord.Result = ResponseHandshake.ResultType.RESULTTYPE_OK;
                        break;
                    }
                }
            }
            Packet responsePack = new Packet();
            responsePack.Data = antwoord;
            responsePack.Flag = Packet.PacketFlag.PACKETFLAG_RESPONSE_HANDSHAKE;
            _client.sendHandler(responsePack);
        }

        public void changeName(Client _client, Client client)
        {
            if (client.user.Equals(_client.user))
            {
                _client.user = _client.user + count;
                count++;
                changeName(_client, client);
            }
            else return;
        }

        public Packet getOnlineList()
        {
            Packet packet = new Packet();
            packet.Flag = Packet.PacketFlag.PACKETFLAG_RESPONSE_USERLIST;
            List<string> sendlist = new List<string>();
            Console.WriteLine("Users: ");
            foreach (Client client in clientList )
            {
                Console.WriteLine(client.user);
                sendlist.Add(client.user);
            }

            packet.Data = sendlist;
            return packet;
        }

        public void removeFromList(Client _client)
        {
            clientList.Remove(_client);
        }

        public void changeStatus(Packet _packet, Client _client)
        {
            CChat_Library.Objects.UserStatus.Status status = ((CChat_Library.Objects.Packets.ChangeStatus)_packet.Data).status;

            Packet packet = new Packet();
            packet.Flag = Packet.PacketFlag.PACKETFLAG_RESPONSE_STATUS;
            CChat_Library.Objects.Packets.ChangeStatus henkie = new CChat_Library.Objects.Packets.ChangeStatus();
            henkie.clientName = _client.user;
            henkie.status=status;
            packet.Data = henkie;
            foreach (Client client in clientList)
            {
                client.sendHandler(packet);
            }
        }

        public void saveLogMsg(string msg, string user)
        {
            DateTime vandaagdate = DateTime.Today;
            string vandaag = vandaagdate.ToShortDateString().Replace("-", "");
            StreamWriter writer = new StreamWriter(vandaag + ".log", true);
            DateTime tijd = DateTime.Now;
            writer.WriteLine("[" + tijd.ToShortTimeString() + "] " + user +": \" " +  msg + " \"");
            writer.Flush();
            writer.Close();
        }
    }
}