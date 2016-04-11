using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Words.Attribute;

namespace Words.Controllers
{
    [AccountAttribute(IsChecked = true)]
    public class TableController : Controller
    {
        // GET: Table
        public ActionResult Index()
        {
            //DataTable ds= Reader(@"select a.name 表名,b.name 字段名,c.name 字段类型,c.length 字段长度 from sysobjects a,syscolumns b,systypes c where a.id=b.id
            //and a.name = 'cetsix' and a.xtype = 'U'
            //and b.xtype = c.xtype");
            



            return View();
        }
       
        public ActionResult Test() {
            string con = "Server=192.168.1.70;Database=dpnetwork_data;uid=sa;pwd=bmc!1234+";
            string sqlcommend = @"SELECT TOP 1000 [Fld_Id]
      ,[Fld_MaxValue]
      ,[Fld_MinValue]
      ,[Fld_Time]
      ,[Fld_TotalValue]
      ,[Fld_OriTotalValue]
      ,[Fld_AFUid]
      ,[Fld_FlowMeterUid]
  FROM [dpnetwork_data].[dbo].[FlowDay_t]";
            DataTable dt= Reader(con, sqlcommend);
            

            return View();
        }
        public JsonResult SQL(string sql,string con) {
           
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (sql.ToLower().Contains("select"))
            {
                js.Data =JsonConvert.SerializeObject(Reader(con,sql));
            }
            else {
                js.Data = Writer(con, sql);
            }
         
            return js;
        }
        Func<string,string, DataTable> Reader= (cs,sql) => {
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;
            SqlDataAdapter dbAdapter = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            dbAdapter.Fill(ds);
            con.Close();
            return ds;
        };
        Func<string,string, int> Writer =(cs,sql) =>
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;
            int num = cmd.ExecuteNonQuery();
            con.Close();
            return num;
        };
    }
}