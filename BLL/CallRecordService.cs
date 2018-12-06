using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Dto;

namespace BLL
{
    interface CallRecordService
    {
        List<CallRecordDto> getCallRecordAll(string databaseAddress, string databaseName,string username,string anotherName,string databasePwd);

        List<CallRecordDto> getCallRecordByKey(string databaseAddress,string databaseName,string userName,string anotherName,string databasePwd,string key);

        StatisticsDto getDayWeekMonTime(string databaseAddress, string databaseName, string userName, string databasePwd, string anotherName);
    }
}
