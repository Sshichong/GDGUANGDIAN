using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    /// <summary>
    /// 返回录音记录实体类
    /// </summary>
    public class SoundRecordDto
    {
        /// <summary>
        /// 记录id
        /// </summary>
        public int pkid { get; set; }

        /// <summary>
        /// 频率,databaseInfo的Another字段
        /// </summary>
        public string databaseAnotherName { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string fileName { get; set; }

        /// <summary>
        /// 录音开始时间
        /// </summary>
        public string starttime { get; set; }

        /// <summary>
        /// 录音结束时间
        /// </summary>
        public string endtime { get; set; }

        /// <summary>
        /// 试听地址
        /// </summary>
        public string filePath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string fileSize { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public string fileLen { get; set; }

        
    }
}
