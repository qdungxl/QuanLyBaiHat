using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class CASYDAL:DataDAL
    {
        public List<CASY> LayToanBoCaSy()
        {
            List<CASY> dsCaSy = new List<CASY>();
            Openconn();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from CASY";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CASY cs = new CASY();
                cs.MaCS = reader.GetString(0);
                cs.TenCS = reader.GetString(1);
                dsCaSy.Add(cs);
            }
            return dsCaSy;
        }
    }
}
