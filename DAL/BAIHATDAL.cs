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
    public class BAIHATDAL:DataDAL
    {
        public List<BAIHAT> LayToanBoBaiHat()
        {
            Openconn();
            List<BAIHAT> dsBH = new List<BAIHAT>();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select *from BAIHAT";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                BAIHAT bh = new BAIHAT();
                bh.MaBH = reader.GetString(0);
                bh.TenBH = reader.GetString(1);
                bh.NhacSy = reader.GetString(2);
                CASY cs = new CASY();
                cs.MaCS = reader.GetString(3);
                bh.CaSy = cs;
                dsBH.Add(bh);
            }
            return dsBH;
        }
        public void LuuBaiHat(BAIHAT bh)
        {
            Openconn();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into BAIHAT(MaBH,TenBH,NhacSy,MaCS) values (@mabh,@tenbh,@nhacsy,@macs)";
            command.Connection = conn;
            command.Parameters.Add("@mabh", SqlDbType.NVarChar).Value = bh.MaBH;
            command.Parameters.Add("@tenbh", SqlDbType.NVarChar).Value = bh.TenBH;
            command.Parameters.Add("@nhacsy", SqlDbType.NVarChar).Value = bh.NhacSy;
            command.Parameters.Add("@macs", SqlDbType.NVarChar).Value = bh.CaSy.MaCS;
            command.ExecuteNonQuery();
        }
        public void CapNhatBaiHat(BAIHAT bh)
        {
            Openconn();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "update BAIHAT set TenBH=@tenbh, NhacSy=@nhacsy, MaCS=@macs where MaBH=@mabh";
            command.Connection = conn;
            command.Parameters.Add("@tenbh", SqlDbType.NVarChar).Value = bh.TenBH;
            command.Parameters.Add("@nhacsy", SqlDbType.NVarChar).Value = bh.NhacSy;
            command.Parameters.Add("@macs", SqlDbType.NVarChar).Value = bh.CaSy.MaCS;
            command.Parameters.Add("@mabh", SqlDbType.NVarChar).Value = bh.MaBH;
            command.ExecuteNonQuery();
        }
        public void XoaBaiHat(string mabh)
        {
            Openconn();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from BAIHAT where MaBH = @ma";
            command.Connection = conn;
            command.Parameters.Add("@ma", SqlDbType.NVarChar).Value = mabh;
            command.ExecuteNonQuery();
        }
    }
}
