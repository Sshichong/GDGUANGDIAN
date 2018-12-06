using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using BLL.serviceImp;
using MySql.Data.MySqlClient;
using System.Data;
using BLL.utils;
using System.Collections;
using Model.Dto;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Web;

namespace GD.Controllers
{
    public class DataController : ApiController
    {
        /// <summary>
        /// 检查数据库的有效性
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CheckDataBase(string databaseAddress, string databaseName, string username, string databasePwd)
        {

            if (databaseAddress != null && databasePwd != null)
            {
                Check check = new Check();
                int flag = check.checkDatabase(databaseAddress, databaseName, username, databasePwd);
                if (flag == 1)
                {
                    return Ok(new { Code = 0, Msg = "连接成功！" });
                }
                else
                {
                    return Ok(new { Code = 1, Msg = "连接失败！" });
                }
            }
            return Ok("");

        }


        /// <summary>
        /// 添加数据库配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddDataBase(string databaseAddress,string databaseName,string databasePwd,string username,string anothername)
        {
            //验证数据库有效性
            Check check = new Check();
            int num = check.checkDatabase(databaseAddress, databaseName, username,databasePwd);
            if (num == 1)   //有效
            {
                try
                {
                    DatabaseInfoServiceImp gs = new DatabaseInfoServiceImp();

                    //1.检验数据库是否重复
                    int DatabaseNum = gs.getDatabaseNumByDatabaseName(databaseName);
                    if (DatabaseNum >= 1)
                    {
                        return Ok(new { Code = 1, Msg = "已存在数据库" });
                    }
                    else
                    {
                        //不重复，插入操作
                        int flag = gs.AddDatabaseInfo(databaseAddress, databaseName, username, anothername, databasePwd);
                        if (flag == 1)
                        {
                            return Ok(new { Code = 0, Msg = "添加成功" });
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    return Ok(new { Code = 1, Msg = "添加失败" });
                }
            }
            else    //无效
            {
                return Ok(new { Code = 1, Msg = "连接数据库失败" });
            }
                return null;
        }
        /// <summary>
        /// 数据库配置列表
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult DataBaseList()
        {
            DatabaseInfoServiceImp gs = new DatabaseInfoServiceImp();
            List<DatabaseInfo> list = gs.GetDatabaseInfo();
            return Ok(new { Code=0,Data=list });
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IHttpActionResult Login(string username,string password)
        {
            UserServiceImp user = new UserServiceImp();
            User u = user.getUser(username);
            if (u.userName.Equals(username) && u.userPwd.Equals(password))
            {
                return Ok(new { Code = 0, Msg = "登录成功！" });
            }
            else
            {
                return Ok(new { Code = 1, Msg = "登录失败,用户名或密码错误！" });
            }
        }

        /// <summary>
        /// 获取全部通话记录
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetCallRecordAll()
        {
            try
            {
                //查出所有数据库
                DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
                List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
                CallRecordServiceImp callRecord = new CallRecordServiceImp();
                List<CallRecordDto> c = new List<CallRecordDto>();
                List<string> errname = new List<string>();

                //遍历数据库
                foreach (DatabaseInfo d in databaseInfos)
                {
                    Check check = new Check();

                    int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd);
                    if (flag == 1)
                    {
                        List<CallRecordDto> callRecords = callRecord.getCallRecordAll(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd);
                        foreach (CallRecordDto callRecordDto in callRecords)
                        {
                            
                            c.Add(callRecordDto);
                        }
                    }
                    else
                    {
                        errname.Add(d.anotherName);

                        //return Ok(new { Code = 1, msg = d.databaseName+"数据库异常" });
                    }


                }
                if (errname == null || errname.Count == 0)
                {
                    return Ok(new { Code = 0, Data = c });
                }
                else
                {
                    string str = "";
                    for (int i = 0; i < errname.Count; i++)
                    {
                        
                        if (i == errname.Count - 1)
                        {
                            str += errname[i].ToString();
                        }
                        str += errname[i].ToString() + ",";
                     
                    }
                    return Ok(new { Code = 1, Msg = str + "频道异常！" });

                }
            }catch(Exception e)
            {
                return Ok(new { Code = 1, Msg = e.Message });
            }
        }

        /// <summary>
        /// 获取全部录音记录
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult getSoundRecordAll()
        {
            //获取全部数据库
            DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
            List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
            SoundRecordServiceImp soundRecordServiceImp = new SoundRecordServiceImp();
            List<SoundRecordDto> s = new List<SoundRecordDto>();
            List<string> errname = new List<string>();

            //遍历数据库
            foreach(DatabaseInfo d in databaseInfos)
            {
                Check check = new Check();
                int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd);
                if (flag == 1)
                {
                    List<SoundRecordDto> soundRecords = soundRecordServiceImp.getSoundRecordAll(d.databaseAddress, d.databaseName, d.anotherName, d.userName, d.databasePwd);
                    foreach(SoundRecordDto soundRecord in soundRecords)
                    {
                        s.Add(soundRecord);
                    }
                    
                }
                else
                {
                    errname.Add(d.anotherName);
                }
            }
            if (errname == null || errname.Count == 0)
            {
                return Ok(new { Code = 0, Data = s });
            }
            else
            {
                string str = "";
                for (int i = 0; i < errname.Count; i++)
                {
                    str += errname[i].ToString() + ",";
                    if (i == errname.Count - 1)
                    {
                        str += errname[i].ToString();
                    }
                }
                return Ok(new { Code = 1, Msg = str + "频道异常！",Data = s });

            }
          
        }

        /// <summary>
        /// 模糊查询通话记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult getCallRecordByKey(string keyword)
        {
            //查询所有数据库
            DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
            List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
            CallRecordServiceImp callRecordServiceImp = new CallRecordServiceImp();
            List<CallRecordDto> s = new List<CallRecordDto>();

            //遍历数据库
            foreach(DatabaseInfo d in databaseInfos)
            {
                Check check = new Check();
                int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd);
                if (flag == 1)
                {
                    if (keyword == null || keyword.Equals(""))
                    {
                        List<CallRecordDto> callRecordDtos = callRecordServiceImp.getCallRecordAll(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd);
                        foreach (CallRecordDto call in callRecordDtos)
                        {
                            s.Add(call);
                        }
                    }
                    else
                    {
                        List<CallRecordDto> callRecordDtos = callRecordServiceImp.getCallRecordByKey(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd, keyword);
                        foreach (CallRecordDto call in callRecordDtos)
                        {
                            s.Add(call);
                        }
                    }
                }
                else
                {
                    continue;
                }
            }

            if (s == null || s.Count == 0)
            {
                return Ok(new { Code = 1, Msg = "查询记录为空！" });
            }
            else
            {
                return Ok(new { Code = 0, Data = s });
            }
            

        }

        /// <summary>
        /// 模糊查询录音记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult getSoundRecordByKey(string starttime,string endtime,string keyword)
        {
            //查询所有数据库
            DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
            List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
            SoundRecordServiceImp soundRecordServiceImp = new SoundRecordServiceImp();
            List<SoundRecordDto> soundRecordDtos = new List<SoundRecordDto>();

            //遍历所有数据库
            foreach(DatabaseInfo d in databaseInfos)
            {
                Check check = new Check();
                int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd);
                if (flag == 1)
                {
                    if (keyword == null || keyword.Equals(""))
                    {
                        List<SoundRecordDto> soundRecords = soundRecordServiceImp.getSoundRecordAll(d.databaseAddress, d.databaseName, d.anotherName, d.userName, d.databasePwd);
                        foreach (SoundRecordDto soundRecord in soundRecords)
                        {
                            soundRecordDtos.Add(soundRecord);
                        }
                    }
                    else
                    {//keyword为空

                        List<SoundRecordDto> soundRecords = soundRecordServiceImp.getSoundRecordByKey(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd, starttime, endtime, keyword);

                        foreach (SoundRecordDto soundRecord in soundRecords)
                        {
                            soundRecordDtos.Add(soundRecord);
                        }

                    }

                }
                else
                {
                    continue;
                }
            }

            if (soundRecordDtos == null || soundRecordDtos.Count == 0)
            {
                return Ok(new { Code = 1, Msg = "查询记录为空！" });
            }
            else
            {
                return Ok(new { Code = 0, Data = soundRecordDtos });
            }
            
        }



        /// <summary>
        /// 统计
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult getStatistics()
        {
            DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
            List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
            CallRecordServiceImp callRecordServiceImp = new CallRecordServiceImp();
            List<StatisticsDto> statistics = new List<StatisticsDto>();
            foreach(DatabaseInfo d in databaseInfos)
            {
                Check check = new Check();
                int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd);
                if (flag == 1)
                {
                    StatisticsDto statisticsDtos = callRecordServiceImp.getDayWeekMonTime(d.databaseAddress,d.databaseName,d.userName,d.databasePwd,d.anotherName);
                    statistics.Add(statisticsDtos);
                }
                else
                {
                    continue;
                }
            }
            if (statistics == null || statistics.Count == 0)
            {
                return Ok(new { Code = 1, Msg = "无数据!" });
            }
            else
            {
                return Ok(new { Code = 0, Data = statistics });
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult exportExcel(string keyword,string file)
        {
            //根据keyword查记录
            CallRecordServiceImp callRecordServiceImp = new CallRecordServiceImp();
            DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
            List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
            List<CallRecordDto> s = new List<CallRecordDto>();

            //遍历数据库
            foreach (DatabaseInfo d in databaseInfos)
            {
                Check check = new Check();
                int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd);
                if (flag == 1)
                {
                    if (keyword == null || keyword.Equals(""))
                    {
                        List<CallRecordDto> callRecordDtos = callRecordServiceImp.getCallRecordAll(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd);
                        foreach (CallRecordDto call in callRecordDtos)
                        {
                            s.Add(call);
                        }
                    }
                    else {
                        

                        List<CallRecordDto> callRecordDtos = callRecordServiceImp.getCallRecordByKey(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd, keyword);
                        foreach (CallRecordDto call in callRecordDtos)
                        {
                            s.Add(call);
                        }
                    }
                    
                }
                else
                {
                    continue;
                }
            }

            //导出
            ExportExcel exportExcel = new ExportExcel();

            try
            {
                exportExcel.ListToExcel(s, file);
               
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Ok(new { Code = 1, Msg = "导出失败!" });
            }
            return Ok(new { Code = 0, Msg = "导出成功!" });
            
        }

        /// <summary>
        /// 获取全部数据库信息
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult getDatabaseInfoAll()
        {
            DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
            List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
            if (databaseInfos == null || databaseInfos.Count == 0)
            {
                return Ok(new { Code = 1, Msg = "数据为空!" });
            }
            else
            {
                return Ok(new { Code = 0, Data = databaseInfos });
            }
        }


        


      

    }
}
