﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CChat_Library.Objects.Packets
{
    [Serializable]
    public class RequestUsers
    {
        string requestingUserName { get; set; }
    }
}
