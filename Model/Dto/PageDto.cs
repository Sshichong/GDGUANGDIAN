using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    /// <summary>
    /// 分页实体类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageDto<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int currPage { get; set; }

        /// <summary>
        /// 总页
        /// </summary>
        public int totalPage { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> data { get; set; }
    }
}
