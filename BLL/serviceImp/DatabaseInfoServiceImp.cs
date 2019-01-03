using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
namespace BLL.serviceImp
{
    public class DatabaseInfoServiceImp : DatabaseInfoService
    {
        /// <summary>
        /// 设置项，插入数据库
        /// </summary>
        /// <param name="databaseAddress"></param>
        /// <param name="databaseName"></param>
        /// <param name="userName"></param>
        /// <param name="anotherName"></param>
        /// <param name="databasePwd"></param>
        /// <param name="recordAddress"></param>
        /// <param name="recordUserName"></param>
        /// <param name="recordPwd"></param>
        /// <returns></returns>
        public int AddDatabaseInfo(string databaseAddress, string databaseName,
            string userName, string anotherName, string databasePwd)
        {
            int flag = 0;

            string sql = "insert into tDatabaseInfo (UserName,AnotherName,DatabaseAddress," +
                "DatabaseName,DatabasePwd," +
                "Isdel,Addtime,Updatetime) values (@username,@anotherName,@databaseAddress," +
                "@databaseName,@databasePwd,0,now(),now())";
            using (MySqlConnection mycon = new MySqlConnection(MySqlHelper.Conn))
            {
                // string Conn = "server = " + databaseAddress + ";uid = " + userName + ";pwd = " + databasePwd + "; database = " + databaseName;
                //SqlConnection mycon = new SqlConnection(Conn);
                mycon.Open();
                MySqlCommand cmd = new MySqlCommand(sql, mycon);
                
                //SqlCommand cmd = new SqlCommand(sql, mycon);
                //SqlParameter p1 = new SqlParameter("@username", MySqlDbType.VarChar);
                //p1.Value = userName;
                //SqlParameter p2 = new SqlParameter("@anotherName", MySqlDbType.VarChar);
                //p2.Value = anotherName;
                //SqlParameter p3 = new SqlParameter("@databaseAddress", MySqlDbType.VarChar);
                //p3.Value = databaseAddress;
                //SqlParameter p4 = new SqlParameter("@databaseName", MySqlDbType.VarChar);
                //p4.Value = databaseName;
                //SqlParameter p5 = new SqlParameter("@databasePwd", MySqlDbType.VarChar);
                //p5.Value = databasePwd;
                MySqlParameter p1 = new MySqlParameter("@username", MySqlDbType.VarChar)
                {
                    Value = userName
                };
                MySqlParameter p2 = new MySqlParameter("@anotherName", MySqlDbType.VarChar);
                p2.Value = anotherName;
                MySqlParameter p3 = new MySqlParameter("@databaseAddress", MySqlDbType.VarChar);
                p3.Value = databaseAddress;
                MySqlParameter p4 = new MySqlParameter("@databaseName", MySqlDbType.VarChar);
                p4.Value = databaseName;
                MySqlParameter p5 = new MySqlParameter("@databasePwd", MySqlDbType.VarChar);
                p5.Value = databasePwd;


                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);


                if (cmd.ExecuteNonQuery() > 0)
                {
                    flag = 1;
                }

                return flag;
            }

            // MySqlParameter pamarmeter = new MySqlParameter("@username",userName);
        }

        /// <summary>
        /// 根据数据库名和地址查询数据库
        /// </summary>
        /// <param name="databaseAddress"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public int getDatabaseNumByDatabaseName(string databaseAddress,string databaseName)
        {
            string sql = "select * from tDatabaseInfo where DatabaseName = @databaseName and DatabaseAddress = @databaseAddress";
            MySqlParameter parameter = new MySqlParameter("@databaseName",databaseName);
            MySqlParameter parameter1 = new MySqlParameter("@databaseAddress",databaseAddress);
            MySqlParameter[] p = { parameter,parameter1};
            MySqlHelper db = new MySqlHelper();
            //SqlConnection SQlConn = new SqlConnection("server = .;uid = sa;pwd = ; database = 你要连接的数据库名称");
            DataSet data = db.GetDataSet(MySqlHelper.Conn, CommandType.Text, sql, p);
            return data.Tables[0].Rows.Count;
        }


        /// <summary>
        /// 根据pkid删除数据库信息
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public int deleteDatabase(int pkid)
        {
            int flag = 0;
            string sql = "delete from tDatabaseInfo where PKID =@pkid";
            using (MySqlConnection mycon = new MySqlConnection(MySqlHelper.Conn)) {
                mycon.Open();
                MySqlCommand cmd = new MySqlCommand(sql, mycon);
                MySqlParameter p = new MySqlParameter("@pkid", MySqlDbType.Int32);
                p.Value = pkid;

                cmd.Parameters.Add(p);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    flag = 1;
                }

            }
                return flag;
        }

        /// <summary>
        /// 设置项下的显示查询
        /// </summary>
        /// <returns></returns>
        //public List<DatabaseInfo> GetDatabaseInfo()
        //{
        //    string sql = "selecet AnotherName,DatabaseAddress,DatabaseName,UserName from tDatabaseInfo";
        //    MySqlHelper db = new MySqlHelper();
            
        //    DataSet guidances = db.GetDataSet(MySqlHelper.Conn, CommandType.Text, sql, null);
        //    DataTable dt = guidances.Tables[0];
        //    List<DatabaseInfo> list = new List<DatabaseInfo>();

        //    //遍历DataTable 封装成List<DatabaseInfo>
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        DatabaseInfo gd = new DatabaseInfo();
        //        gd.anotherName = dr["AnotherName"].ToString();
        //        gd.databaseAddress = dr["DatabaseAddress"].ToString();
        //        gd.databaseName = dr["DatabaseName"].ToString();
        //        gd.userName = dr["UserName"].ToString();
        //        list.Add(gd);
        //    }

        //    return list;
        //}

        /// <summary>
        /// 查询所有数据库
        /// </summary>
        /// <returns></returns>
        public List<DatabaseInfo> getDatabaseAll()
        {
            string sql = "select PKID,DatabaseAddress,DatabaseName,UserName,DatabasePwd,AnotherName from tDatabaseInfo";
            MySqlHelper db = new MySqlHelper();
            DataSet databases = db.GetDataSet(MySqlHelper.Conn,CommandType.Text,sql,null);

            DataTable dt = databases.Tables[0];
            List<DatabaseInfo> list = new List<DatabaseInfo>();

            //遍历DataTable 封装成List<DatabaseInfo>
            foreach(DataRow dr in dt.Rows)
            {
                DatabaseInfo d = new DatabaseInfo();
                d.pkid = Convert.ToInt32(dr["PKID"].ToString());
                d.databaseAddress = dr["DatabaseAddress"].ToString();
                d.databaseName = dr["DatabaseName"].ToString();
                d.userName = dr["UserName"].ToString();
                d.databasePwd = dr["DatabasePwd"].ToString();
                d.anotherName = dr["AnotherName"].ToString();
                list.Add(d);
            }
            return list;
        }
    }
}
