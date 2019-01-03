using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.utils
{
    public class Check
    {
        public int checkDatabase(string databaseAddress,string databaseName, string databaseusername,string databasePwd ,out string constr)
        {
            // string Conn = "Database='" + databaseName + "';Data Source = '" + databaseAddress + "'; User Id = '"+ databaseusername + "'; Password = '" + databasePwd + "'; charset = 'utf8'; pooling = true";
            constr = "server = "+databaseAddress+";uid = "+databaseusername+";pwd = "+databasePwd+"; database = "+databaseName;
            //MySqlConnection mycon = new MySqlConnection(Conn);
            SqlConnection mycon = new SqlConnection(constr);
            try
            {
                mycon.Open();
                if (ConnectionState.Open == mycon.State)
                {
                    bool b = CheckExistsTable("sd_tRec", constr);
                    bool b1 = CheckExistsTable("sd_tTel",constr);
                    mycon.Close();
                    if (b == true&&b1==true)
                    {
                        return 1;
                    }
                    else { 
                    return -1;
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.Write(e);
                return 0;
            }
            return 0;

        }

        /// <summary>
        /// 连接数据库成功后检查是否存在目标数据表
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static bool CheckExistsTable(string tablename,string ConnectionString)
        {
            String tableNameStr = "select count(1) from sysobjects where name = '" + tablename + "'";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(tableNameStr, con);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


    }
}
