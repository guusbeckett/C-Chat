using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CChat_Library.Objects
{
    [Serializable]
    public class UserStatus
    {
        public enum Status
        {
            STATUS_OFFLINE,
            STATUS_ONLINE,
            STATUS_AWAY,
            SATUS_BUSY
        }

        public Status stat { get; set; }
    }
}
