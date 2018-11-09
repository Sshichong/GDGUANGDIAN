using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL
{
    interface GuidanceService
    {
        int AddGuidanceInformation(string databaseAddress,string databaseName,string userName,
            string anotherName,string databasePwd,string recordAddress,string recordUserName,string recordPwd);

        List<Guidance> GetGuidance();
    }
}
