using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int userID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string userPwd { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public byte? isDel { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updateTime { get; set; }
    }
}
