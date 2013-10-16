using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CChat_Library.Objects.Packets
{
    [Serializable]
    public class ResponseHandshake
    {
       /// <summary>
       /// The result type
       /// </summary>
        public enum ResultType
        {
            RESULTTYPE_OK,
            RESULTTYPE_INVALIDCREDENTIALS,
            RESULTTYPE_ACCESSDENIED
        }

        /// <summary>
        /// The result that is returned
        /// </summary>
        public ResultType Result { get; set; }
    }
}
