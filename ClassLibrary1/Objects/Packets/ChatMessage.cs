using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CChat_Library.Objects.Packets
{
    [Serializable]
    public class ChatMessage
    {
        /// <summary>
        /// The client name that sends the message
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// The client name that the message is ment for
        /// </summary>
        public string Reciever { get; set; }

        /// <summary>
        /// The actual chatmessage
        /// </summary>
        public string Chat { get; set; }
    }
}
