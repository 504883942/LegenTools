using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManage
{
    public class DatabaseManage
    {
        public SqlConnection con = new SqlConnection("Server=192.168.1.73;Database=Words;uid=sa;pwd=bmc!1234+");
        public DataTable GetAllTable() {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText =@"select a.name 表名,b.name 字段名,c.name 字段类型,c.length 字段长度 from sysobjects a,syscolumns b,systypes c where a.id=b.id
            and a.name = 'cetsix' and a.xtype = 'U'
            and b.xtype = c.xtype";
            cmd.Connection = con;
            SqlDataAdapter dbAdapter = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            dbAdapter.Fill(ds);
            con.Close();
            return ds;
        }
    }
}
