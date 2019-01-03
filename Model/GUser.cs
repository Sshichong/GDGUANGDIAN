using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 导播用户实体类
    /// </summary>
    public class GUser
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string userPwd { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int userType { get; set; }

        /// <summary>
        /// 用户联系电话
        /// </summary>
        public string telPhone { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 最近一次联系时间
        /// </summary>
        public string lastTeleTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
    }
}
