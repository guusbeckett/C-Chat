using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CChat_Library.Objects.Packets
{
    [Serializable]
    public class ChangeStatus
    {
        public UserStatus.Status status { get; set; }
        public string clientName { get; set; }
    }
}
