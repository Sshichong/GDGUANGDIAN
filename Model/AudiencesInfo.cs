using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 听众实体类
    /// </summary>
    public class AudiencesInfo
    {
        /// <summary>
        /// 应用系统标识
        /// </summary>
        public int ai_Id { get; set; }

        /// <summary>
        /// 听众名称
        /// </summary>
        public string ai_Name { get; set; }

        /// <summary>
        /// 拼音简写
        /// </summary>
        public string ai_PyName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string ai_Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string ai_Age { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string ai_Birth { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string ai_Tel { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string ai_CarCode { get; set; }

        /// <summary>
        /// 证件
        /// </summary>
        public string ai_Certi { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string ai_Addr { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string ai_Occu { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string ai_Email { get; set; }

        /// <summary>
        /// 最后来电时间
        /// </summary>
        public string ai_FromTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string ai_Remark { get; set; }

        /// <summary>
        /// 听众类型
        /// </summary>
        public int ai_Type { get; set; }
    }
}
