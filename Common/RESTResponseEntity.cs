using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
 public    class RESTResponseEntity
    {
        public int StatusCode { get; set; }
        public byte[] Buffer { get; set; }
        public object Data { get; set; }
    }
}
