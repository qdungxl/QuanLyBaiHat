using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DataDAL
    {
        string sqlconn = @"Data Source=QUOCDUNGSURFACE\SQLEXPRESS01;Initial Catalog=CSDL_QLBH;Integrated Security=True";
        protected SqlConnection conn = null;
        public void Openconn()
        {
            if (conn == null)
                conn = new SqlConnection(sqlconn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }
        public void Closeconn()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
}
