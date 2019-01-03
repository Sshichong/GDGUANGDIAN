using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dto;
using MySql.Data.MySqlClient;

namespace BLL.serviceImp
{
    public class SoundRecordServiceImp : SoundRecordService
    {
        public List<SoundRecordDto> getSoundRecordAll(string databaseAddress, string databaseName, string anotherName, string userName, string databasePwd, int start, int end)
        {
            List<SoundRecordDto> soundRecordDtos = new List<SoundRecordDto>();
           
                //string sql = "SELECT * FROM (select PKID, FileName,FileLen from sd_tRec) as a ORDER BY 1 OFFSET " + (start-1)+" ROWS FETCH NEXT "+(end-start+1)+" ROWS ONLY";
                string sql = "SELECT * FROM (select PKID, StartTime,EndTime,FileName,FileLen,FilePath, ROW_NUMBER() OVER(ORDER BY starttime) as R FROM sd_tRec) t WHERE R >=" + start + " and R<=" + end;
                // MySqlHelper mySqlHelper = new MySqlHelper();
                string Conn = "server = " + databaseAddress + ";uid = " + userName + ";pwd = " + databasePwd + "; database = " + databaseName;
                //string conn = "Database='" + databaseName + "';Data Source='" + databaseAddress + "';User Id='" + userName + "';Password='" + databasePwd + "';charset='utf8';pooling=true";
                SqlDataAdapter ada = new SqlDataAdapter(sql, Conn);
                DataSet soundRecords = new DataSet();
                ada.Fill(soundRecords);
                // DataSet soundRecords = mySqlHelper.GetDataSet(conn, System.Data.CommandType.Text, sql, null);
                DataTable dt = soundRecords.Tables[0];


                foreach (DataRow dr in dt.Rows)
                {
                    SoundRecordDto soundRecordDto = new SoundRecordDto();
                    soundRecordDto.databaseAnotherName = anotherName;
                    soundRecordDto.pkid = Convert.ToInt32(dr["PKID"].ToString());
                    soundRecordDto.starttime = dr["StartTime"].ToString();
                    soundRecordDto.endtime = dr["EndTime"].ToString();
                    soundRecordDto.fileName = dr["FileName"].ToString();
                    
                    //soundRecordDto.filePath = dr["FilePath"].ToString();
                    soundRecordDto.fileLen = dr["FileLen"].ToString();
                    soundRecordDtos.Add(soundRecordDto);
                }

                return soundRecordDtos;
         
        }

        public List<SoundRecordDto> getSoundRecordByKey(string databaseAddress, string databaseName, string userName, string anotherName, string databasePwd, string starttime, string endtime, string key, int start, int end)
        {
            List<SoundRecordDto> soundRecordDtos = new List<SoundRecordDto>();
            //string sql = "select FileName,FileLen from tRec";
            //string sql = "SELECT PKID, FileName,FileLen from sd_tRec where StartTime> '" + starttime + "' and StartTime < '" + endtime + "' AND FileName LIKE '%" + key + "%'";
            //string sql = "SELECT * FROM(SELECT PKID, FileName,FileLen from sd_tRec where StartTime> '"+starttime+"' and StartTime < '"+endtime+"' and (FileName like '%"+key+"%' OR FileLen LIKE '%"+key+"%')) AS a ORDER BY 1 OFFSET "+(start-1)+" ROWS FETCH NEXT "+(end-start+1)+" ROWS ONLY";
            
            string Conn = "server = " + databaseAddress + ";uid = " + userName + ";pwd = " + databasePwd + "; database = " + databaseName;
            //string conn = "Database='" + databaseName + "';Data Source='" + databaseAddress + "';User Id='" + userName + "';Password='" + databasePwd + "';charset='utf8';pooling=true";


            if (anotherName.Contains(key))
            {
                //string sql1 = "SELECT * FROM(SELECT PKID, FileName,FileLen from sd_tRec where StartTime> '" + starttime + "' and StartTime < '" + endtime + "' ) AS a ORDER BY 1 OFFSET " + (start - 1) + " ROWS FETCH NEXT " + (end - start + 1) + " ROWS ONLY";
                string sql1 = "SELECT * FROM (select PKID, StartTime,EndTime, FileName,FileLen,FilePath, ROW_NUMBER() OVER(ORDER BY starttime) as R FROM sd_tRec  where StartTime> '" + starttime + "' and StartTime < '" + endtime + "' ) t WHERE R >=" + start + " and R<=" + end;
                SqlDataAdapter ada1 = new SqlDataAdapter(sql1, Conn);
                DataSet soundRecords1 = new DataSet();
                ada1.Fill(soundRecords1);
                DataTable dt1 = soundRecords1.Tables[0];

                foreach (DataRow dr in dt1.Rows)
                {
                    SoundRecordDto s = new SoundRecordDto();
                    s.pkid = Convert.ToInt32(dr["PKID"].ToString());
                    s.databaseAnotherName = anotherName;
                    s.starttime = dr["StartTime"].ToString();
                    s.endtime = dr["EndTime"].ToString();
                    s.fileName = dr["FileName"].ToString();
                    s.fileLen = dr["FileLen"].ToString();
                    // s.fileSize = "";
                    //s.filePath = dr["FilePath"].ToString();
                    soundRecordDtos.Add(s);
                }

            }
            else
            {

                string sql = "SELECT * FROM(SELECT PKID, StartTime,EndTime, FileName,FileLen,FilePath , ROW_NUMBER() OVER(ORDER BY starttime) as R from sd_tRec where StartTime> '" + starttime + "' and StartTime < '" + endtime + "' and (FileName like '%" + key + "%')) t WHERE R>=" + start + " AND R<=" + end;
                SqlDataAdapter ada = new SqlDataAdapter(sql, Conn);
                DataSet soundRecords = new DataSet();
                ada.Fill(soundRecords);


                //MySqlParameter p1 = new MySqlParameter("@starttime", MySqlDbType.VarChar);
                //p1.Value = starttime;
                //MySqlParameter p2 = new MySqlParameter("@endtime", MySqlDbType.VarChar);
                //p2.Value = endtime;

                //MySqlParameter[] parameters = { p1, p2 };



                // MySqlHelper mySqlHelper = new MySqlHelper();

                //DataSet soundRecords = mySqlHelper.GetDataSet(conn, CommandType.Text, sql, parameters);
                DataTable dt = soundRecords.Tables[0];



                foreach (DataRow dr in dt.Rows)
                {

                    //if (anotherName.Contains(key) || dr["FileName"].ToString().Contains(key) || dr["FileLen"].ToString().Contains(key))
                    //{
                    SoundRecordDto s = new SoundRecordDto();
                    s.pkid = Convert.ToInt32(dr["PKID"].ToString());
                    s.databaseAnotherName = anotherName;
                    s.starttime = dr["StartTime"].ToString();
                    s.endtime = dr["EndTime"].ToString();
                    s.fileName = dr["FileName"].ToString();
                    s.fileLen = dr["FileLen"].ToString();
                    //s.fileSize = "";
                    //s.filePath = dr["FilePath"].ToString();
                    soundRecordDtos.Add(s);
                    //}

                }
            }

            //if (soundRecordDtos == null || soundRecordDtos.Count == 0)
            //{
                //if (anotherName.Contains(key))
                //{
                //    //string sql1 = "SELECT * FROM(SELECT PKID, FileName,FileLen from sd_tRec where StartTime> '" + starttime + "' and StartTime < '" + endtime + "' ) AS a ORDER BY 1 OFFSET " + (start - 1) + " ROWS FETCH NEXT " + (end - start + 1) + " ROWS ONLY";
                //    string sql1 = "SELECT * FROM (select PKID, StartTime,EndTime, FileName,FileLen,FilePath, ROW_NUMBER() OVER(ORDER BY starttime) as R FROM sd_tRec) t WHERE R >=" + start + " and R<=" + end;
                //    SqlDataAdapter ada1 = new SqlDataAdapter(sql1, Conn);
                //    DataSet soundRecords1 = new DataSet();
                //    ada1.Fill(soundRecords1);
                //    DataTable dt1 = soundRecords1.Tables[0];

                //    foreach (DataRow dr in dt1.Rows)
                //    {
                //        SoundRecordDto s = new SoundRecordDto();
                //        s.pkid = Convert.ToInt32(dr["PKID"].ToString());
                //        s.databaseAnotherName = anotherName;
                //        s.starttime = dr["StartTime"].ToString();
                //        s.endtime = dr["EndTime"].ToString();
                //        s.fileName = dr["FileName"].ToString();
                //        s.fileLen = dr["FileLen"].ToString();
                //       // s.fileSize = "";
                //        //s.filePath = dr["FilePath"].ToString();
                //        soundRecordDtos.Add(s);
                //    }

                //}

            //}

            return soundRecordDtos;
        }
    }
}
