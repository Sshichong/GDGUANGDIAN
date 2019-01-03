using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public List<CallRecordDto> getCallRecordAll(string databaseAddress, string databaseName, string username, string anotherName,string databasePwd,int start,int end)
        {
            List<CallRecordDto> list = new List<CallRecordDto>();
           
                // string sql = "select t.PKID,t.StartTime,u.UserName,t.Calling,t.TalkingTime from tUser u INNER JOIN sd_tTel t WHERE u.MOBILE = t.Called ";
                //string sql = "select t.PKID,t.StartTime,u.UserName,t.Calling,t.TalkingTime from tUser u INNER JOIN sd_tTel t on u.MOBILE = t.Called ORDER BY 1 OFFSET "+start+" ROWS FETCH NEXT "+ limit + " ROWS ONLY;";
                //string sql = "SELECT * FROM (select t.PKID,t.StartTime,t.Called,u.UserName,t.Calling,t.TalkingTime from sd_tTel t LEFT JOIN tUser u on u.MOBILE = t.Called) as a ORDER BY 1 OFFSET "+(start-1)+" ROWS FETCH NEXT "+(end-start+1)+" ROWS ONLY;";
                string sql = "SELECT * FROM (select t.PKID,t.StartTime,t.Called,u.UserName,t.Calling,t.TalkingTime,ROW_NUMBER() OVER(ORDER BY t.starttime) as R  from sd_tTel t LEFT JOIN tUser u on u.MOBILE = t.Called) t WHERE R>=" + start + " AND R<=" + end;
                //MySqlHelper mySqlHelper = new MySqlHelper();
                //string conn = "Database='"+databaseName+"';Data Source='"+ databaseAddress + "';User Id='"+username+"';Password='"+databasePwd+"';charset='utf8';pooling=true";
                string Conn = "server = " + databaseAddress + ";uid = " + username + ";pwd = " + databasePwd + "; database = " + databaseName;
                //SqlConnection mycon = new SqlConnection(Conn);
                //mycon.Open();
                SqlDataAdapter ada = new SqlDataAdapter(sql, Conn);
                DataSet callRecords = new DataSet();
                ada.Fill(callRecords);
                // mycon.Close();
                // DataSet callRecords = mySqlHelper.GetDataSet(conn, CommandType.Text, sql, null);
                DataTable dt = callRecords.Tables[0];


                foreach (DataRow dr in dt.Rows)
                {
                    CallRecordDto callRecordDto = new CallRecordDto();
                    callRecordDto.pkid = Convert.ToInt32(dr["PKID"].ToString());
                    callRecordDto.databaseAnotherName = anotherName;
                    callRecordDto.startTime = dr["StartTime"].ToString();
                    //if (dr["UserName"].ToString() == null|| dr["UserName"].ToString().Equals(""))
                    //{
                    //    callRecordDto.hostname = dr["Called"].ToString();
                    //}
                    //else {
                    //    callRecordDto.hostname = dr["UserName"].ToString();
                    //}
                    callRecordDto.hostname = (dr["UserName"].ToString() == null || dr["UserName"].ToString().Equals("")) ? dr["Called"].ToString() : dr["UserName"].ToString();
                    callRecordDto.called = dr["Calling"].ToString();
                    callRecordDto.callTime = dr["TalkingTime"].ToString();
                    list.Add(callRecordDto);
                }


                return list;
          
        }

        public List<CallRecordDto> getCallRecordByKey(string databaseAddress,string databaseName,string userName,string anotherName,string databasePwd,string key,int start,int end)
        {

            List<CallRecordDto> callRecordDtos = new List<CallRecordDto>();
            string Conn = "server = " + databaseAddress + ";uid = " + userName + ";pwd = " + databasePwd + "; database = " + databaseName;
            //string conn = "Database='" + databaseName + "';Data Source='" + databaseAddress + "';User Id='" + userName + "';Password='" + databasePwd + "';charset='utf8';pooling=true";
            //string sql = "select t.StartTime,u.UserName,t.Calling,t.TalkingTime from tuser u INNER JOIN sd_ttel t where u.Mobile = t.Called";
            //string sql = "select t.StartTime,u.UserName,t.Calling,t.TalkingTime from tuser u INNER JOIN sd_ttel t on u.Mobile = t.Called";
            //string sql = "SELECT * from (select t.StartTime,t.called,u.UserName,t.Calling,t.TalkingTime from sd_ttel t LEFT  JOIN tuser u on u.Mobile = t.Called WHERE starttime LIKE '%"+key+"%' OR called like '%"+key+"%' or username LIKE '%"+key+"%' OR Calling LIKE '%"+key+"%' OR talkingtime LIKE '%"+key+"%') as a ORDER BY 1 OFFSET "+(start-1)+" ROWS FETCH NEXT "+(end-start+1)+" ROWS ONLY;";


            if (anotherName.Contains(key))
            {
                string sql1 = "SELECT * from (select t.PKID,t.StartTime,t.called,u.UserName,t.Calling,t.TalkingTime,ROW_NUMBER() OVER(ORDER BY t.starttime) as R from sd_ttel t LEFT JOIN tuser u on u.Mobile = t.Called) t WHERE R>=" + start + " AND R<=" + end;
                SqlDataAdapter ada1 = new SqlDataAdapter(sql1, Conn);
                DataSet callRecords1 = new DataSet();
                ada1.Fill(callRecords1);
                DataTable dt1 = callRecords1.Tables[0];
                foreach (DataRow dr in dt1.Rows)
                {
                    CallRecordDto callRecord = new CallRecordDto();
                    callRecord.pkid = Convert.ToInt32(dr["PKID"].ToString());
                    callRecord.databaseAnotherName = anotherName;
                    callRecord.startTime = dr["StartTime"].ToString();
                    callRecord.hostname = (dr["UserName"].ToString() == null || dr["UserName"].ToString().Equals("")) ? dr["Called"].ToString() : dr["UserName"].ToString();
                    callRecord.called = dr["Calling"].ToString();
                    callRecord.callTime = dr["TalkingTime"].ToString();
                    callRecordDtos.Add(callRecord);
                }
            }
            else
            {
                string sql = "SELECT * from (select t.PKID,t.StartTime,t.called,u.UserName,t.Calling,t.TalkingTime,ROW_NUMBER() OVER(ORDER BY t.starttime) as R from sd_ttel t LEFT  JOIN tuser u on u.Mobile = t.Called WHERE  called like '%" + key + "%' or username LIKE '%" + key + "%' OR Calling LIKE '%" + key + "%' ) t WHERE R>=" + start + " AND R<=" + end;
                SqlDataAdapter ada = new SqlDataAdapter(sql, Conn);
                DataSet callRecords = new DataSet();
                ada.Fill(callRecords);
                //MySqlHelper mySqlHelper = new MySqlHelper();
                //DataSet callRecords = mySqlHelper.GetDataSet(conn, CommandType.Text, sql, null);
                DataTable dt = callRecords.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    CallRecordDto callRecord = new CallRecordDto();
                    callRecord.pkid = Convert.ToInt32(dr["PKID"].ToString());
                    callRecord.databaseAnotherName = anotherName;
                    callRecord.startTime = dr["StartTime"].ToString();
                    callRecord.hostname = (dr["UserName"].ToString() == null || dr["UserName"].ToString().Equals("")) ? dr["Called"].ToString() : dr["UserName"].ToString();
                    callRecord.called = dr["Calling"].ToString();
                    callRecord.callTime = dr["TalkingTime"].ToString();
                    callRecordDtos.Add(callRecord);
                }
            }
            //若结果为0,anotherName包含key
            //if(callRecordDtos==null|| callRecordDtos.Count == 0)
            //{
            //    if (anotherName.Contains(key))
            //    {
            //        string sql1 = "SELECT * from (select t.PKID,t.StartTime,t.called,u.UserName,t.Calling,t.TalkingTime,ROW_NUMBER() OVER(ORDER BY t.starttime) as R from sd_ttel t LEFT JOIN tuser u on u.Mobile = t.Called) t WHERE R>="+start+" AND R<="+end;
            //        SqlDataAdapter ada1 = new SqlDataAdapter(sql1, Conn);
            //        DataSet callRecords1 = new DataSet();
            //        ada1.Fill(callRecords1);
            //        DataTable dt1 = callRecords1.Tables[0];
            //        foreach (DataRow dr in dt1.Rows)
            //        {
            //            CallRecordDto callRecord = new CallRecordDto();
            //            callRecord.pkid = Convert.ToInt32(dr["PKID"].ToString());
            //            callRecord.databaseAnotherName = anotherName;
            //            callRecord.startTime = dr["StartTime"].ToString();
            //            callRecord.hostname = (dr["UserName"].ToString() == null || dr["UserName"].ToString().Equals("")) ? dr["Called"].ToString() : dr["UserName"].ToString();
            //            callRecord.called = dr["Calling"].ToString();
            //            callRecord.callTime = dr["TalkingTime"].ToString();
            //            callRecordDtos.Add(callRecord);
            //        }
            //    }
            //}

            return callRecordDtos;
        }

        public StatisticsDto getDayWeekMonTime(string databaseAddress, string databaseName, string userName, string databasePwd, string anotherName)
        {
            string Conn = "server = " + databaseAddress + ";uid = " + userName + ";pwd = " + databasePwd + "; database = " + databaseName;
            //string conn = "Database='" + databaseName + "';Data Source='" + databaseAddress + "';User Id='" + userName + "';Password='" + databasePwd + "';charset='utf8';pooling=true";
            //string daySql = "SELECT SUM(a.TalkingTime) TalkingTime from (select * from sd_tTel LIMIT 0,1) as a";
            //string weekSql = "SELECT SUM(a.TalkingTime) TalkingTime from (select * from sd_tTel LIMIT 0,7) as a";
            //string monthSql = "SELECT SUM(a.TalkingTime) TalkingTime from (select * from sd_tTel LIMIT 0,30) as a";

            //string daySql = "SELECT SUM(a.TalkingTime) TalkingTime from (SELECT top 1 * from sd_tTel ORDER BY StartTime DESC) as a";
            //string weekSql = "SELECT SUM(a.TalkingTime) TalkingTime from (SELECT top 7 * from sd_tTel ORDER BY StartTime DESC) as a";
            //string monthSql = "SELECT SUM(a.TalkingTime) TalkingTime from (SELECT top 30 * from sd_tTel ORDER BY StartTime DESC) as a";
            string daySql = "SELECT SUM(talkingtime) talk from sd_tTel WHERE StartTime=GETDATE()";
            string weekSql = "SELECT SUM(talkingtime) talk from sd_tTel WHERE StartTime>GETDATE()-7";
            string monthSql = "SELECT SUM(talkingtime) talk from sd_tTel WHERE StartTime>GETDATE()-30";

            SqlDataAdapter adaday = new SqlDataAdapter(daySql, Conn);
            SqlDataAdapter adaweek = new SqlDataAdapter(weekSql, Conn);
            SqlDataAdapter adamonth = new SqlDataAdapter(monthSql, Conn);
            DataSet dayData = new DataSet();
            DataSet weekData = new DataSet();
            DataSet monthData = new DataSet();
            adaday.Fill(dayData);
            adaweek.Fill(weekData);
            adamonth.Fill(monthData);


            //MySqlHelper mySqlHelper = new MySqlHelper();
            //DataSet dayData = mySqlHelper.GetDataSet(conn, CommandType.Text, daySql, null);
            //DataSet weekData = mySqlHelper.GetDataSet(conn, CommandType.Text, weekSql, null);
            //DataSet monthData = mySqlHelper.GetDataSet(conn, CommandType.Text, monthSql, null);
            DataRow ddr = dayData.Tables[0].Rows[0];
            DataRow wdr = weekData.Tables[0].Rows[0];
            DataRow mdr = monthData.Tables[0].Rows[0];

            StatisticsDto statisticsDto = new StatisticsDto();
            statisticsDto.databaseAnotherName = anotherName;

            
            statisticsDto.dayTotal = ddr["talk"].ToString().Equals("")?"0": ddr["talk"].ToString();
            statisticsDto.weekTotal = wdr["talk"].ToString().Equals("") ? "0" : wdr["talk"].ToString();
            statisticsDto.monthTotal = mdr["talk"].ToString().Equals("") ? "0" : mdr["talk"].ToString();

            return statisticsDto;
        }

        public List<CallRecordDto> getExportCallRecordAll(string databaseAddress, string databaseName, string userName, string anotherName, string databasePwd)
        {
            string sql = "select t.PKID,t.StartTime,t.Called,u.UserName,t.Calling,t.TalkingTime from sd_tTel t LEFT JOIN tUser u on u.MOBILE = t.Called";
            string Conn = "server = " + databaseAddress + ";uid = " + userName + ";pwd = " + databasePwd + "; database = " + databaseName;
            SqlDataAdapter ada = new SqlDataAdapter(sql, Conn);
            DataSet callRecords = new DataSet();
            ada.Fill(callRecords);
            DataTable dt = callRecords.Tables[0];
            List<CallRecordDto> list = new List<CallRecordDto>();

            foreach (DataRow dr in dt.Rows)
            {
                CallRecordDto callRecordDto = new CallRecordDto();
                callRecordDto.pkid = Convert.ToInt32(dr["PKID"].ToString());
                callRecordDto.databaseAnotherName = anotherName;
                callRecordDto.startTime = dr["StartTime"].ToString();
                callRecordDto.hostname = dr["UserName"].ToString() == null ? dr["Called"].ToString() : dr["UserName"].ToString();
                callRecordDto.called = dr["Calling"].ToString();
                callRecordDto.callTime = dr["TalkingTime"].ToString();
                list.Add(callRecordDto);
            }


            return list;
        }

        public List<CallRecordDto> getExportCallRecordByKey(string databaseAddress, string databaseName, string userName, string anotherName, string databasePwd, string keyword)
        {
            List<CallRecordDto> callRecordDtos = new List<CallRecordDto>();
            string Conn = "server = " + databaseAddress + ";uid = " + userName + ";pwd = " + databasePwd + "; database = " + databaseName;
            //string conn = "Database='" + databaseName + "';Data Source='" + databaseAddress + "';User Id='" + userName + "';Password='" + databasePwd + "';charset='utf8';pooling=true";
            //string sql = "select t.StartTime,u.UserName,t.Calling,t.TalkingTime from tuser u INNER JOIN sd_ttel t where u.Mobile = t.Called";
            //string sql = "select t.StartTime,u.UserName,t.Calling,t.TalkingTime from tuser u INNER JOIN sd_ttel t on u.Mobile = t.Called";

            if (anotherName.Contains(keyword))
            {
                string sql1 = "select t.starttime,t.called,u.username,t.calling,t.talkingtime from sd_ttel t left join tuser u on u.mobile = t.called";
                SqlDataAdapter ada1 = new SqlDataAdapter(sql1, Conn);
                DataSet callrecords1 = new DataSet();
                ada1.Fill(callrecords1);
                DataTable dt1 = callrecords1.Tables[0];
                foreach (DataRow dr in dt1.Rows)
                {
                    CallRecordDto callrecord = new CallRecordDto();
                    callrecord.databaseAnotherName = anotherName;
                    callrecord.startTime = dr["starttime"].ToString();
                    callrecord.hostname = (dr["username"].ToString() == null || dr["username"].ToString().Equals("")) ? dr["called"].ToString() : dr["username"].ToString();
                    callrecord.called = dr["calling"].ToString();
                    callrecord.callTime = dr["talkingtime"].ToString();
                    callRecordDtos.Add(callrecord);
                }
            }
            else
            {


                string sql = "select t.StartTime,u.UserName,t.Calling,t.Called,t.TalkingTime from sd_ttel t  LEFT  JOIN tuser u on u.Mobile = t.Called WHERE  username LIKE '%" + keyword + "%' OR calling LIKE '%" + keyword + "%' ";
                SqlDataAdapter ada = new SqlDataAdapter(sql, Conn);
                DataSet callRecords = new DataSet();
                ada.Fill(callRecords);
                //MySqlHelper mySqlHelper = new MySqlHelper();
                //DataSet callRecords = mySqlHelper.GetDataSet(conn, CommandType.Text, sql, null);
                DataTable dt = callRecords.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    CallRecordDto callRecord = new CallRecordDto();
                    callRecord.databaseAnotherName = anotherName;
                    callRecord.startTime = dr["StartTime"].ToString();
                    callRecord.hostname = (dr["UserName"].ToString() == null || dr["UserName"].ToString().Equals("")) ? dr["Called"].ToString() : dr["UserName"].ToString();
                    callRecord.called = dr["Calling"].ToString();
                    callRecord.callTime = dr["TalkingTime"].ToString();
                    callRecordDtos.Add(callRecord);
                }
            }
            //若结果为0,anothername包含key
            //if ((callRecordDtos == null || callRecordDtos.Count == 0) && anotherName.Contains(keyword))
            //{
            //    string sql1 = "select t.starttime,t.called,u.username,t.calling,t.talkingtime from sd_ttel t left join tuser u on u.mobile = t.called";
            //    SqlDataAdapter ada1 = new SqlDataAdapter(sql1, Conn);
            //    DataSet callrecords1 = new DataSet();
            //    ada1.Fill(callrecords1);
            //    DataTable dt1 = callrecords1.Tables[0];
            //    foreach (DataRow dr in dt1.Rows)
            //    {
            //        CallRecordDto callrecord = new CallRecordDto();
            //        callrecord.databaseAnotherName = anotherName;
            //        callrecord.startTime = dr["starttime"].ToString();
            //        callrecord.hostname = (dr["username"].ToString() == null || dr["username"].ToString().Equals("")) ? dr["called"].ToString() : dr["username"].ToString();
            //        callrecord.called = dr["calling"].ToString();
            //        callrecord.callTime = dr["talkingtime"].ToString();
            //        callRecordDtos.Add(callrecord);
            //    }
            //}

            return callRecordDtos;
        }
    }
}
