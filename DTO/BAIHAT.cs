using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BAIHAT
    {
        public string MaBH { get; set; }
        public string TenBH { get; set; }
        public string NhacSy { get; set; }
        public CASY CaSy { get; set; }
        public override string ToString()
        {
            return TenBH;
        }
    }
}
