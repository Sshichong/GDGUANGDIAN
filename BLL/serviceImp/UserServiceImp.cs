using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;

namespace BLL.serviceImp
{
   public  class UserServiceImp : UserService
    {
        public List<User> getUser(string username)
        {
            string sql = "select UserName,UserPwd from tuser where UserName ='"+username+"'";
            string Conn = ConfigurationManager.AppSettings["sqlserverConnection"].ToString();
            System.Diagnostics.Debug.Write(Conn);

            SqlDataAdapter ada = new SqlDataAdapter(sql, Conn);
            DataSet users = new DataSet();
            ada.Fill(users);

            //MySqlParameter parameter = new MySqlParameter("@userName", username);
            //MySqlHelper db = new MySqlHelper(); 
            //DataSet user = db.GetDataSet(MySqlHelper.Conn,CommandType.Text,sql, parameter);
            User u = new User();
            List<User> list = new List<User>();
            if (users.Tables[0].Rows.Count == 0)
            {
                return list;
            }
            else
            {
                DataTable dt = users.Tables[0];
                DataRow dr = dt.Rows[0];
               // u.userID = Convert.ToInt32(dr["UserID"]);
                u.userName = dr["UserName"].ToString();
                u.userPwd = dr["UserPwd"].ToString();
                // u.isDel = Convert.ToByte(dr["Isdel"]);
                //u.addTime = Convert.ToDateTime(dr["Addtime"]);
                //u.updateTime = Convert.ToDateTime(dr["Updatetime"]);
                list.Add(u);
                return list;
            }
            
        }
    }
}
