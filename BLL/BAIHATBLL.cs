using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class BAIHATBLL
    {
        BAIHATDAL bhdal = new BAIHATDAL();
        public List<BAIHAT> LayToanBoBaiHat()
        {
            return bhdal.LayToanBoBaiHat();
        }
        public void LuuBaiHat(BAIHAT bh)
        {
            bhdal.LuuBaiHat(bh);
        }
        public void CapNhatBaiHat(BAIHAT bh)
        {
            bhdal.CapNhatBaiHat(bh);
        }
        public void XoaBaiHat(string mabh)
        {
            bhdal.XoaBaiHat(mabh);
        }
    }
}
