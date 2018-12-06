using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 录音记录表
    /// </summary>
    public class SoundRecord
    {
        /// <summary>
        /// 记录标识
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
        /// 开始时间
        /// </summary>
        public string starttime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endtime { get; set; }

        /// <summary>
        /// 录音时长
        /// </summary>
        public string filelen { get; set; }

        /// <summary>
        /// 录音存放位置
        /// </summary>
        public string filepath { get; set; }

        /// <summary>
        /// 录音文件名
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// 录音格式
        /// </summary>
        public string fileformat { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }
    }
}
