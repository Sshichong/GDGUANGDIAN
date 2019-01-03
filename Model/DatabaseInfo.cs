using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DatabaseInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int pkid { get; set; }

        /// <summary>
        /// 数据库用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 数据库别名
        /// </summary>
        public string anotherName { get; set; }

        /// <summary>
        /// 数据库地址
        /// </summary>
        public string databaseAddress { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string databaseName { get; set; }

        /// <summary>
        /// 数据库密码
        /// </summary>
        public string databasePwd { get; set; }

        /// <summary>
        /// 录音ftp地址
        /// </summary>
        public string recordAddress { get; set; }

        /// <summary>
        /// 录音ftp用户名
        /// </summary>
        public string recordUserName { get; set; }

        /// <summary>
        /// 录音ftp密码
        /// </summary>
        public string recordPwd { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public byte isDel { get; set; }

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
