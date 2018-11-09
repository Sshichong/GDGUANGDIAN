using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using MySql.Data.MySqlClient;

namespace BLL.serviceImp
{
   public  class UserServiceImp : UserService
    {
        public User getUser(string username)
        {
            string sql = "select UserName,UserPwd from user where UserName =@userName";
            MySqlParameter parameter = new MySqlParameter("@userName", username);
            MySqlHelper db = new MySqlHelper(); 
            DataSet user = db.GetDataSet(MySqlHelper.Conn,CommandType.Text,sql, parameter);
            User u = new User();
            if (user.Tables[0].Rows.Count == 0)
            {
                return u;
            }
            else
            {
                DataTable dt = user.Tables[0];
                DataRow dr = dt.Rows[0];
               // u.userID = Convert.ToInt32(dr["UserID"]);
                u.userName = dr["UserName"].ToString();
                u.userPwd = dr["UserPwd"].ToString();
               // u.isDel = Convert.ToByte(dr["Isdel"]);
                //u.addTime = Convert.ToDateTime(dr["Addtime"]);
                //u.updateTime = Convert.ToDateTime(dr["Updatetime"]);
                return u;
            }
            
        }
    }
}
