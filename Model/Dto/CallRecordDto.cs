using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    /// <summary>
    /// 返回通话记录实体类
    /// </summary>
    public class CallRecordDto
    {
        /// <summary>
        /// 记录id
        /// </summary>
        public int pkid { get; set; }

        /// <summary>
        /// 频率（databaseAnotherName）
        /// </summary>
        public string databaseAnotherName { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string startTime { get; set; }

        /// <summary>
        /// 主持人，对应tuser表的username
        /// </summary>
        public string hostname { get; set; }

        /// <summary>
        /// 呼入电话，tTel表的calling
        /// </summary>
        public string called { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public string callTime { get; set; }
    }
}
