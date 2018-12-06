using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dto;
using MySql.Data.MySqlClient;

namespace BLL.serviceImp
{
    public class SoundRecordServiceImp : SoundRecordService
    {
        public List<SoundRecordDto> getSoundRecordAll(string databaseAddress, string databaseName, string anotherName, string userName, string databasePwd)
        {
            string sql = "select FileName,FileLen from sd_tRec";
            MySqlHelper mySqlHelper = new MySqlHelper();
            string conn = "Database='" + databaseName + "';Data Source='" + databaseAddress + "';User Id='" + userName + "';Password='" + databasePwd + "';charset='utf8';pooling=true";
            DataSet soundRecords = mySqlHelper.GetDataSet(conn, System.Data.CommandType.Text, sql, null);
            DataTable dt = soundRecords.Tables[0];
            List<SoundRecordDto> soundRecordDtos = new List<SoundRecordDto>();

            foreach(DataRow dr in dt.Rows)
            {
                SoundRecordDto soundRecordDto = new SoundRecordDto();
                soundRecordDto.databaseAnotherName = anotherName;
                soundRecordDto.fileName = dr["FileName"].ToString();
                soundRecordDto.fileSize = "";
                soundRecordDto.fileLen = dr["FileLen"].ToString();
                soundRecordDtos.Add(soundRecordDto);
            }

            return soundRecordDtos;
        }

        public List<SoundRecordDto> getSoundRecordByKey(string databaseAddress, string databaseName, string userName, string anotherName, string databasePwd, string starttime, string endtime, string key)
        {
            //string sql = "select FileName,FileLen from tRec";
            string sql = "SELECT PKID, FileName,FileLen from sd_tRec where StartTime> @starttime and StartTime < @endtime";
            string conn = "Database='" + databaseName + "';Data Source='" + databaseAddress + "';User Id='" + userName + "';Password='" + databasePwd + "';charset='utf8';pooling=true";
            List<SoundRecordDto> soundRecordDtos = new List<SoundRecordDto>();

            MySqlParameter p1 = new MySqlParameter("@starttime", MySqlDbType.VarChar);
            p1.Value = starttime;
            MySqlParameter p2 = new MySqlParameter("@endtime", MySqlDbType.VarChar);
            p2.Value = endtime;

            MySqlParameter[] parameters = { p1, p2 };

            MySqlHelper mySqlHelper = new MySqlHelper();

            DataSet soundRecords = mySqlHelper.GetDataSet(conn, CommandType.Text, sql, parameters);
            DataTable dt = soundRecords.Tables[0];

            foreach(DataRow dr in dt.Rows)
            {
                SoundRecordDto s = new SoundRecordDto();
                s.pkid = Convert.ToInt32(dr["PKID"].ToString());
                s.databaseAnotherName = anotherName;
                s.fileName = dr["FileName"].ToString();
                s.fileLen = dr["FileLen"].ToString();
                s.fileSize = "";

                if (s.databaseAnotherName.Contains(key) || s.fileName.Contains(key) || s.fileLen.Contains(key) || s.fileSize.Contains(key))
                {
                    soundRecordDtos.Add(s);
                }

            }

            return soundRecordDtos;
        }
    }
}
