using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Aaron.Common
{
    public class MySQLHelper
    {
        public static DataTable GetDataTable()
        {
            string user = "root";
            string pwd = "123456";
            string ConString = $"Server=127.0.0.1;Database=mydb; User ID={user};Password={pwd};port=3306;CharSet=utf8;pooling=true;";
            MySqlConnection con = new MySqlConnection(ConString);//连接数据库
            con.Open();//打开连接
            string cmdstr = "select * from user";
            MySqlDataAdapter da = new MySqlDataAdapter(cmdstr, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;
        }


    }
}