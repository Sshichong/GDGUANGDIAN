using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    /// <summary>
    /// 统计返回实体类
    /// </summary>
    public class StatisticsDto
    {
        /// <summary>
        /// 频率
        /// </summary>
        public string databaseAnotherName { get; set; }

        /// <summary>
        /// 一天总计
        /// </summary>
        public string dayTotal { get; set; }

        /// <summary>
        /// 一周总计
        /// </summary>
        public string weekTotal { get; set; }

        /// <summary>
        /// 一月总计
        /// </summary>
        public string monthTotal { get; set; }
    }
}
