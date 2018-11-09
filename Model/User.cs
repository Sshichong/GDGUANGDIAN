using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string userPwd { get; set; }
        public byte? isDel { get; set; }
        public DateTime addTime { get; set; }
        public DateTime updateTime { get; set; }
    }
}
