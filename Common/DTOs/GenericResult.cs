using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class GenericResult
    {
        public object Result { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
