using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CallRecord
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int pkid { get; set; }

        /// <summary>
        /// 主叫号码
        /// </summary>
        public string calling { get; set; }

        /// <summary>
        /// 被叫号码
        /// </summary>
        public string called { get; set; }

        /// <summary>
        /// 呼叫状态
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string starttime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endtime { get; set; }

        /// <summary>
        /// 通话时长
        /// </summary>
        public int talkingtime { get; set; }
    }
}
