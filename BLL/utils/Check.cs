using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.utils
{
    public class Check
    {
        public int checkDatabase(string databaseAddress,string databaseName, string databaseusername,String databasePwd)
        {
            string Conn = "Database='" + databaseName + "';Data Source = '" + databaseAddress + "'; User Id = '"+ databaseusername + "'; Password = '" + databasePwd + "'; charset = 'utf8'; pooling = true";
            MySqlConnection mycon = new MySqlConnection(Conn);
            try
            {
                mycon.Open();
                if (ConnectionState.Open == mycon.State)
                {
                    mycon.Close();
                    return 1;
                }
                
            }
            catch (Exception e)
            {
                Console.Write(e);
                return 0;
            }
            return 0;

        }
    }
}
