using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL
{
    interface DatabaseInfoService
    {
        int AddDatabaseInfo(string databaseAddress,string databaseName,string userName,
            string anotherName,string databasePwd);

        List<DatabaseInfo> GetDatabaseInfo();

        int getDatabaseNumByDatabaseName(string databaseName);

        List<DatabaseInfo> getDatabaseAll();
    }
}
