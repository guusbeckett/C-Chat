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

            Console.WriteLine(msg.Chat);

            foreach (Client client in clientList)
            {
                if (client.user == msg.Reciever || msg.Reciever == "ALL")
                {
                    client.sendHandler(_packet);
                }
            }
        }   
        
        public void handshakeResponse(Client _client, Handshake _handshake)
        {
            int count = 1;
            ResponseHandshake antwoord = new ResponseHandshake();
            antwoord.Result = ResponseHandshake.ResultType.RESULTTYPE_ACCESSDENIED;
            if (clientList.Contains(_client))
            {
                antwoord.Result = ResponseHandshake.ResultType.RESULTTYPE_USER_EXISTS;
                foreach (Client client in clientList)
	            {
		            if (_client.user.Equals(client.user + count))
	                {
		                count++;
	                }
                    else antwoord.givenUsername = _client.user + count;
	            }
            }
            else
            {
                addToClientList(_client);
                antwoord.Result = ResponseHandshake.ResultType.RESULTTYPE_OK;
            }

            Packet responsePack = new Packet();
            responsePack.Data = antwoord;
            responsePack.Flag = Packet.PacketFlag.PACKETFLAG_RESPONSE_HANDSHAKE;
            _client.sendHandler(responsePack);
        }

        public Packet getOnlineList()
        {
            Packet packet = new Packet();
            packet.Flag = Packet.PacketFlag.PACKETFLAG_RESPONSE_USERLIST;
            packet.Data = clientList;
            return packet;
        }

        public void addToClientList(Client client)
        {
            clientList.Add(client);
        }

        public void changeStatus(Packet packet, Client _client)
        {
            _client.setStatus(UserStatus.Status.STATUS_ONLINE);
            _client.setStatus(UserStatus.Status.SATUS_BUSY);
            _client.setStatus(UserStatus.Status.STATUS_AWAY);
            _client.setStatus(UserStatus.Status.STATUS_OFFLINE);
        }
    }
}