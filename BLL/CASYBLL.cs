using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class CASYBLL
    {
        CASYDAL casyAc = new CASYDAL();
        public List<CASY> LayToanBoCaSy()
        {
            return casyAc.LayToanBoCaSy();
        }
    }
}
