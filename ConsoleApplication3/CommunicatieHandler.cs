using System.IO;
using System.Net;
using System;
using System.Threading;
using Chat = System.Net;
using System.Collections;
using ConsoleApplication3;
using CChat_Library.Objects;
using CChat_Library.Objects.Packets;



namespace ConsoleApplication3
{
    class CommunicatieHandler
    {
        public CommunicatieHandler()
        {

        }

        public static void readPacket(ChatServer _server, Client _client, Packet packet)
        {
            switch (packet.Flag)
            {
                case Packet.PacketFlag.PACKETFLAG_CHANGE_STATUS:
                    _server.changeStatus(packet, _client);
                    break;

                case Packet.PacketFlag.PACKETFLAG_CHAT:
                    System.Diagnostics.Debug.WriteLine("TESTEN");
                    _server.sendMessage(packet, _client);
                    break;

                case Packet.PacketFlag.PACKETFLAG_REQUEST_HANDSHAKE:
                    _server.handshakeResponse(_client, (Handshake)packet.Data);
                    break;

                case Packet.PacketFlag.PACKETFLAG_REQUEST_USERLIST:
                    _client.sendHandler(_server.getOnlineList());
                    break;

                default:
                    Console.WriteLine("Error: Flag not supported - {0}", packet.Flag);
                    break;
            }
        }

        public static void setUssername(Client client, Packet packet)
        {
            client.setUssername(((Handshake)packet.Data).username); 
        }
        
    }
}
