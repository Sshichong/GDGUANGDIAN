using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.utils;
using Model;
using Model.Dto;

namespace BLL.serviceImp
{
    public class CallRecordServiceImp : CallRecordService
    {
        public List<CallRecordDto> getCallRecordAll(string databaseAddress, string databaseName, string username, string anotherName,string databasePwd)
        {
            string sql = "select t.PKID,t.StartTime,u.UserName,t.Calling,t.TalkingTime from tUser u INNER JOIN sd_ttel t WHERE u.Mobile = t.Called ";
            MySqlHelper mySqlHelper = new MySqlHelper();
            string conn = "Database='"+databaseName+"';Data Source='"+ databaseAddress + "';User Id='"+username+"';Password='"+databasePwd+"';charset='utf8';pooling=true";
            DataSet callRecords = mySqlHelper.GetDataSet(conn, CommandType.Text, sql, null);
            DataTable dt = callRecords.Tables[0];
            List<CallRecordDto> list = new List<CallRecordDto>();

            foreach(DataRow dr in dt.Rows)
            {
                CallRecordDto callRecordDto = new CallRecordDto();
                callRecordDto.pkid = Convert.ToInt32(dr["PKID"].ToString());
                callRecordDto.databaseAnotherName = anotherName;
                callRecordDto.startTime = dr["StartTime"].ToString();
                callRecordDto.hostname = dr["UserName"].ToString();
                callRecordDto.called = dr["Calling"].ToString();
                callRecordDto.callTime = dr["TalkingTime"].ToString();
                list.Add(callRecordDto);
            }
            
            return list;
        }

        public List<CallRecordDto> getCallRecordByKey(string databaseAddress,string databaseName,string userName,string anotherName,string databasePwd,string key)
        {

            List<CallRecordDto> callRecordDtos = new List<CallRecordDto>();

            string conn = "Database='" + databaseName + "';Data Source='" + databaseAddress + "';User Id='" + userName + "';Password='" + databasePwd + "';charset='utf8';pooling=true";
            string sql = "select t.StartTime,u.UserName,t.Calling,t.TalkingTime from tuser u INNER JOIN sd_ttel t WHERE u.Mobile = t.Called";
            MySqlHelper mySqlHelper = new MySqlHelper();
            DataSet callRecords = mySqlHelper.GetDataSet(conn, CommandType.Text, sql, null);
            DataTable dt = callRecords.Tables[0];
            foreach(DataRow dr in dt.Rows)
            {
                if(anotherName.Contains(key)||dr["StartTime"].ToString().Contains(key)||
                    dr["UserName"].ToString().Contains(key)||dr["Calling"].ToString().Contains(key)||
                    dr["TalkingTime"].ToString().Contains(key))
                {
                    CallRecordDto callRecord = new CallRecordDto();
                    callRecord.databaseAnotherName = anotherName;
                    callRecord.startTime = dr["StartTime"].ToString();
                    callRecord.hostname = dr["UserName"].ToString();
                    callRecord.called = dr["Calling"].ToString();
                    callRecord.callTime = dr["TalkingTime"].ToString();
                    callRecordDtos.Add(callRecord);
                }
            }

            return callRecordDtos;
        }

        public StatisticsDto getDayWeekMonTime(string databaseAddress, string databaseName, string userName, string databasePwd, string anotherName)
        {
            string conn = "Database='" + databaseName + "';Data Source='" + databaseAddress + "';User Id='" + userName + "';Password='" + databasePwd + "';charset='utf8';pooling=true";
            string daySql = "SELECT SUM(a.TalkingTime) TalkingTime from (select * from sd_tTel LIMIT 0,1) as a";
            string weekSql = "SELECT SUM(a.TalkingTime) TalkingTime from (select * from sd_tTel LIMIT 0,7) as a";
            string monthSql = "SELECT SUM(a.TalkingTime) TalkingTime from (select * from sd_tTel LIMIT 0,30) as a";

            MySqlHelper mySqlHelper = new MySqlHelper();
            DataSet dayData = mySqlHelper.GetDataSet(conn, CommandType.Text, daySql, null);
            DataSet weekData = mySqlHelper.GetDataSet(conn, CommandType.Text, weekSql, null);
            DataSet monthData = mySqlHelper.GetDataSet(conn, CommandType.Text, monthSql, null);
            DataRow ddr = dayData.Tables[0].Rows[0];
            DataRow wdr = weekData.Tables[0].Rows[0];
            DataRow mdr = monthData.Tables[0].Rows[0];

            StatisticsDto statisticsDto = new StatisticsDto();
            statisticsDto.databaseAnotherName = anotherName;
            statisticsDto.dayTotal = ddr["TalkingTime"].ToString();
            statisticsDto.weekTotal = wdr["TalkingTime"].ToString();
            statisticsDto.monthTotal = mdr["TalkingTime"].ToString();

            return statisticsDto;
        }
    }
}
