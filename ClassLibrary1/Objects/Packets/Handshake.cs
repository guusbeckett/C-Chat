using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CChat_Library.Objects.Packets
{
    [Serializable]
    public class Handshake
    {
        /// <summary>
        /// The username used for login
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// The password used for login
        /// </summary>
        public string password { get; set; }
    }
}
