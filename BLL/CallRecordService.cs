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
        List<CallRecordDto> getCallRecordAll(string databaseAddress, string databaseName,string username,string anotherName,string databasePwd,int start,int end);

        List<CallRecordDto> getCallRecordByKey(string databaseAddress,string databaseName,string userName,string anotherName,string databasePwd,string key,int start,int end);

        StatisticsDto getDayWeekMonTime(string databaseAddress, string databaseName, string userName, string databasePwd, string anotherName);

        List<CallRecordDto> getExportCallRecordAll(string databaseAddress, string databaseName, string userName, string anotherName, string databasePwd);

        List<CallRecordDto> getExportCallRecordByKey(string databaseAddress, string databaseName, string userName, string anotherName, string databasePwd, string keyword);
    }
}
