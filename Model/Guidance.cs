using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Guidance
    {
        public int guidID { get; set; }
        public string userName { get; set; }
        public string anotherName { get; set; }
        public string datebaseAddress { get; set; }
        public string databaseName { get; set; }
        public string databasePwd { get; set; }
        public string recordAddress { get; set; }
        public string recordUserName { get; set; }
        public string recordPwd { get; set; }
        public byte isDel { get; set; }
        public DateTime addTime { get; set; }
        public DateTime updateTime { get; set; }
    }
}
