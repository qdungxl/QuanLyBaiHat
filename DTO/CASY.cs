using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CASY
    {
        public string MaCS { get; set; }
        public string TenCS { get; set; }
        public List<BAIHAT> dsBaiHat { get; set; }
        public CASY()
        {
            dsBaiHat = new List<BAIHAT>();
        }
        public override string ToString()
        {
            return TenCS;
        }
    }
}
