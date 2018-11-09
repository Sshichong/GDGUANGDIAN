using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.serviceImp
{
    public class GuidanceServiceImp : GuidanceService
    {
        public int  AddGuidanceInformation(string databaseAddress, string databaseName,
            string userName, string anotherName, string databasePwd, string recordAddress, string recordUserName, string recordPwd)
        {
            int flag = 0;

            string sql = "insert into guidance (UserName,AnotherName,DatabaseAddress," +
                "DatabaseName,DatabasePwd,RecordAddress,RecordUserName,RecordPwd," +
                "Isdel,Addtime,Updatetime) values (@username,@anotherName,@databaseAddress," +
                "@databaseName,@databasePwd,@recordAddress,@recordUserName,@recordPwd,0,now(),now())";
            MySqlConnection mycon = new MySqlConnection(MySqlHelper.Conn);
            mycon.Open();
            MySqlCommand cmd = new MySqlCommand(sql, mycon);
            MySqlParameter p1 = new MySqlParameter("@username", MySqlDbType.VarChar);
            p1.Value = userName;
            MySqlParameter p2 = new MySqlParameter("@anotherName", MySqlDbType.VarChar);
            p2.Value = anotherName;
            MySqlParameter p3 = new MySqlParameter("@databaseAddress", MySqlDbType.VarChar);
            p3.Value = databaseAddress;
            MySqlParameter p4 = new MySqlParameter("@databaseName", MySqlDbType.VarChar);
            p4.Value = databaseName;
            MySqlParameter p5 = new MySqlParameter("@databasePwd", MySqlDbType.VarChar);
            p5.Value = databasePwd;
            MySqlParameter p6 = new MySqlParameter("@recordAddress", MySqlDbType.VarChar);
            p6.Value = recordAddress;
            MySqlParameter p7 = new MySqlParameter("@recordUserName", MySqlDbType.VarChar);
            p7.Value = recordUserName;
            MySqlParameter p8 = new MySqlParameter("@recordPwd", MySqlDbType.VarChar);
            p8.Value = recordPwd;

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);

            if (cmd.ExecuteNonQuery()>0)
            {
                flag = 1;
            }

            return flag;

            // MySqlParameter pamarmeter = new MySqlParameter("@username",userName);
        }
    }
}
