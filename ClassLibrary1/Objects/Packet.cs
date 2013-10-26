using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CChat_Library.Objects
{
   
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
                PACKETFLAG_REQUEST_USERLIST,
                PACKETFLAG_RESPONSE_USERLIST,
                PACKETFLAG_CHANGE_STATUS,
                PACKETFLAG_RESPONSE_STATUS
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

