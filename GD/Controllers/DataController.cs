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
using System.Web.Security;
using log4net;


namespace GD.Controllers
{
    

    public class DataController : ApiController
    {
        /// <summary>
        /// 检查数据库的有效性
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IHttpActionResult CheckDataBase(string databaseAddress, string databaseName, string username, string databasePwd)
        {

            if (databaseAddress != null && databasePwd != null)
            {
                Check check = new Check();
                int flag = check.checkDatabase(databaseAddress, databaseName, username, databasePwd, out string constr);
                if (flag == 1)
                {
                    return Ok(new { Code = 0, Msg = "连接成功！" });
                }
                else if(flag==-1)
                {
                    return Ok(new { Code = 1, Msg = "数据库结构异常！" });
                }
                else
                {
                    return Ok(new { Code = 1, Msg = "连接失败！" });
                }
            }
            return Ok(new { Code = 1, Msg = "连接失败！" });

        }
        /// <summary>
        /// 添加数据库配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IHttpActionResult AddDataBase(string databaseAddress, string databaseName, string databasePwd, string username, string anothername)
        {
            //验证数据库有效性
            Check check = new Check();
            int num = check.checkDatabase(databaseAddress, databaseName, username, databasePwd, out string constr);
            if (num == 1)   //有效
            {
                try
                {
                    DatabaseInfoServiceImp gs = new DatabaseInfoServiceImp();

                    //1.检验数据库是否重复
                    int DatabaseNum = gs.getDatabaseNumByDatabaseName(databaseAddress, databaseName);
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
                        return Ok(new { Code = 1, Msg = "添加失败" });
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
                if (num == 0)
                {
                    return Ok(new { Code = 1, Msg = "连接数据库失败" });
                }
                else
                {
                    return Ok(new { Code = 1, Msg = "数据库结构异常" });
                }
            }
            //  return Ok(new { Code = 1, Msg = "连接数据库失败" });
        }
        /// <summary>
        /// 数据库配置列表
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IHttpActionResult DataBaseList()
        {
            DatabaseInfoServiceImp gs = new DatabaseInfoServiceImp();
            List<DatabaseInfo> list = gs.getDatabaseAll();
            return Ok(new { Code = 0, Data = list });
        }

        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IHttpActionResult DeleteDatabase(int pkid)
        {
            DatabaseInfoServiceImp databaseInfoServiceImp = new DatabaseInfoServiceImp();
            int flag = databaseInfoServiceImp.deleteDatabase(pkid);
            if (flag == 1)
            {
                return Ok(new { Code = 0, Msg = "删除成功！" });
            }
            else
            {
                return Ok(new { Code  =1,Msg ="删除失败！"});
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IHttpActionResult Login(string username, string password)
        {
            string username1 = username.Trim();

            UserServiceImp user = new UserServiceImp();
            List<User> l = user.getUser(username);
            if (l==null||l.Count==0)
            {
                return Ok(new { Code = 1, Msg = "登录失败,用户名或密码错误！" });
            }
            else
            {
                User u = l[0];
                if (u.userName.Equals(username1) && u.userPwd.Equals(password))
                {

                    FormsAuthentication.SetAuthCookie(username, false);
                    return Ok(new { Code = 0, Msg = "登录成功！" });
                }
                else
                {
                    return Ok(new { Code = 1, Msg = "登录失败,用户名或密码错误！" });
                }
               
            }

        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult LogOut()
        {
            try
            {
                FormsAuthentication.SignOut();
                return Ok(new { Code = 0, Msg = "退出成功" });
            }
            catch (Exception e) {
                return Ok(new { Code = 1, Msg = "退出失败" });
            }
            
        }

        /// <summary>
        /// 获取全部通话记录
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        //public IHttpActionResult GetCallRecordAll()
        //{
        //    try
        //    {
        //        //查出所有数据库
        //        DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
        //        List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
        //        CallRecordServiceImp callRecord = new CallRecordServiceImp();
        //        List<CallRecordDto> c = new List<CallRecordDto>();
        //        List<string> errname = new List<string>();

        //        //遍历数据库
        //        foreach (DatabaseInfo d in databaseInfos)
        //        {
        //            Check check = new Check();

        //            int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd,out string constr);
        //            if (flag == 1)
        //            {
        //                List<CallRecordDto> callRecords = callRecord.getCallRecordAll(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd);
        //                foreach (CallRecordDto callRecordDto in callRecords)
        //                {

        //                    c.Add(callRecordDto);
        //                }
        //            }
        //            else
        //            {
        //                errname.Add(d.anotherName);

        //                //return Ok(new { Code = 1, msg = d.databaseName+"数据库异常" });
        //            }


        //        }
        //        if (errname == null || errname.Count == 0)
        //        {
        //            return Ok(new { Code = 0, Data = c });
        //        }
        //        else
        //        {
        //            string str = "";
        //            for (int i = 0; i < errname.Count; i++)
        //            {

        //                if (i == errname.Count - 1)
        //                {
        //                    str += errname[i].ToString();
        //                }
        //                str += errname[i].ToString() + ",";

        //            }
        //            return Ok(new { Code = 1, Msg = str + "频道异常！" });

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(new { Code = 1, Msg = e.Message });
        //    }
        //}

        /// <summary>
        /// 获取全部录音记录
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IHttpActionResult getSoundRecordAll(int pageNum, int pageSize, string starttime, string endtime, string keyword)
        {
            int start = (pageNum - 1) * pageSize + 1;
            int end = pageSize * pageNum;
            //获取全部数据库
            DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
            List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
            SoundRecordServiceImp soundRecordServiceImp = new SoundRecordServiceImp();
            List<SoundRecordDto> s = new List<SoundRecordDto>();
            List<string> errname = new List<string>();

            //遍历数据库
            foreach (DatabaseInfo d in databaseInfos)
            {
                Check check = new Check();
                int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd, out string constr);
                if (flag == 1)
                {
                    //时间或者keyword为空
                    if (string.IsNullOrEmpty(keyword) || starttime == null || starttime.Equals("Invalid date") || endtime == null || endtime.Equals("Invalid date"))
                    {
                        List<SoundRecordDto> soundRecords = soundRecordServiceImp.getSoundRecordAll(d.databaseAddress, d.databaseName, d.anotherName, d.userName, d.databasePwd,start,end);
                        foreach (SoundRecordDto soundRecord in soundRecords)
                        {
                            s.Add(soundRecord);
                        }
                    }
                    else//不为空
                    {
                        List<SoundRecordDto> soundRecords = soundRecordServiceImp.getSoundRecordByKey(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd, starttime, endtime, keyword,start,end);

                        foreach (SoundRecordDto soundRecord in soundRecords)
                        {
                            s.Add(soundRecord);
                        }
                    }


                }
                else
                {
                    errname.Add(d.anotherName);
                }
            }
            //获取试听地址
            s.ForEach(sound =>
            {
                sound.filePath = "/AudioFiles/"+Path.GetFileNameWithoutExtension(sound.fileName)+".mp3";
            });

            if (errname == null || errname.Count == 0)
            {
                //返回pageDto
                //List<SoundRecordDto> l = new List<SoundRecordDto>();
                //if (end < s.Count)
                //{
                //    for (int i = start - 1; i < end; i++)
                //    {
                //        l.Add(s[i]);
                //    }
                //}
                //else
                //{
                //    for (int i = start - 1; i < s.Count; i++)
                //    {
                //        l.Add(s[i]);
                //    }
                //}
                PageDto<SoundRecordDto> p = new PageDto<SoundRecordDto>();
                p.currPage = pageNum;
                p.totalPage = s.Count % pageSize == 0 ? s.Count / pageSize : (s.Count / pageSize) + 1;
                p.total = s.Count;
                p.pageSize = pageSize;
                p.data = s;

                return Ok(new { Code = 0, Data = p });
                // return Ok(new { Code = 0, Data = s });
            }
            else
            {
                string str = string.Join(",",errname);
                //for (int i = 0; i < errname.Count; i++)
                //{
                //    str += errname[i].ToString() + ",";
                //    if (i == errname.Count - 1)
                //    {
                //        str += errname[i].ToString();
                //    }
                //}
                //返回pageDto
                //List<SoundRecordDto> l = new List<SoundRecordDto>();
                //if (end < s.Count)
                //{
                //    for (int i = start - 1; i < end; i++)
                //    {
                //        l.Add(s[i]);
                //    }
                //}
                //else
                //{
                //    for (int i = start - 1; i < s.Count; i++)
                //    {
                //        l.Add(s[i]);
                //    }
                //}
                PageDto<SoundRecordDto> p = new PageDto<SoundRecordDto>();
                p.currPage = pageNum;
                p.totalPage = s.Count % pageSize == 0 ? s.Count / pageSize : (s.Count / pageSize) + 1;
                p.total = s.Count;
                p.pageSize = pageSize;
                p.data = s;

                return Ok(new { Code = 1, Msg = str + "频道异常！", Data = p });

            }

        }

        /// <summary>
        /// 模糊查询通话记录
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[Authorize]
        //public IHttpActionResult getCallRecordByKey(string keyword,int pageNum,int pageSize)
        //{
        //    int start = (pageNum - 1) * pageSize + 1;
        //    int end = pageSize * pageNum;
        //    //查询所有数据库
        //    DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
        //    List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
        //    CallRecordServiceImp callRecordServiceImp = new CallRecordServiceImp();
        //    List<CallRecordDto> s = new List<CallRecordDto>();

        //    //遍历数据库
        //    foreach (DatabaseInfo d in databaseInfos)
        //    {
        //        Check check = new Check();
        //        int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd,out string constr);
        //        if (flag == 1)
        //        {
        //            if (keyword == null || keyword.Equals(""))
        //            {
        //                List<CallRecordDto> callRecordDtos = callRecordServiceImp.getCallRecordAll(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd);
        //                foreach (CallRecordDto call in callRecordDtos)
        //                {
        //                    s.Add(call);
        //                }
        //            }
        //            else
        //            {
        //                List<CallRecordDto> callRecordDtos = callRecordServiceImp.getCallRecordByKey(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd, keyword);
        //                foreach (CallRecordDto call in callRecordDtos)
        //                {
        //                    s.Add(call);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }

        //    if (s == null || s.Count == 0)
        //    {
        //        return Ok(new { Code = 1, Msg = "查询记录为空！" });
        //    }
        //    else
        //    {
        //        //返回pageDto
        //        List<CallRecordDto> l = new List<CallRecordDto>();
        //        if (end < s.Count)
        //        {
        //            for (int i = start - 1; i < end; i++)
        //            {
        //                l.Add(s[i]);
        //            }
        //        }
        //        else
        //        {
        //            for (int i = start - 1; i < s.Count; i++)
        //            {
        //                l.Add(s[i]);
        //            }
        //        }
        //        PageDto<CallRecordDto> p = new PageDto<CallRecordDto>();
        //        p.currPage = pageNum;
        //        p.totalPage = s.Count % pageSize == 0 ? s.Count / pageSize : (s.Count / pageSize) + 1;
        //        p.total = s.Count;
        //        p.pageSize = pageSize;
        //        p.data = l;
        //        return Ok(new { Code = 0, Data = p });
        //    }


        //}

        /// <summary>
        /// 模糊查询录音记录
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[Authorize]
        //public IHttpActionResult getSoundRecordByKey(string starttime,string endtime,string keyword,int pageNum,int pageSize)
        //{
        //    int start = (pageNum - 1) * pageSize + 1;
        //    int end = pageSize * pageNum;
        //    //查询所有数据库
        //    DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
        //    List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
        //    SoundRecordServiceImp soundRecordServiceImp = new SoundRecordServiceImp();
        //    List<SoundRecordDto> soundRecordDtos = new List<SoundRecordDto>();

        //    //遍历所有数据库
        //    foreach(DatabaseInfo d in databaseInfos)
        //    {
        //        Check check = new Check();
        //        int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd,out string constr);
        //        if (flag == 1)
        //        {
        //            if (keyword == null || keyword.Equals("")||starttime==null||starttime.Equals("Invalid date") ||endtime==null||endtime.Equals("Invalid date"))
        //            {
        //                List<SoundRecordDto> soundRecords = soundRecordServiceImp.getSoundRecordAll(d.databaseAddress, d.databaseName, d.anotherName, d.userName, d.databasePwd);
        //                foreach (SoundRecordDto soundRecord in soundRecords)
        //                {
        //                    soundRecordDtos.Add(soundRecord);
        //                }
        //            }
        //            else
        //            {//keyword为空

        //                List<SoundRecordDto> soundRecords = soundRecordServiceImp.getSoundRecordByKey(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd, starttime, endtime, keyword);

        //                foreach (SoundRecordDto soundRecord in soundRecords)
        //                {
        //                    soundRecordDtos.Add(soundRecord);
        //                }

        //            }

        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }

        //    if (soundRecordDtos == null || soundRecordDtos.Count == 0)
        //    {
        //        return Ok(new { Code = 1, Msg = "查询记录为空！" });
        //    }
        //    else
        //    {

        //        //返回pageDto
        //        List<SoundRecordDto> l = new List<SoundRecordDto>();
        //        if (end < soundRecordDtos.Count)
        //        {
        //            for (int i = start - 1; i < end; i++)
        //            {
        //                l.Add(soundRecordDtos[i]);
        //            }
        //        }
        //        else
        //        {
        //            for (int i = start - 1; i < soundRecordDtos.Count; i++)
        //            {
        //                l.Add(soundRecordDtos[i]);
        //            }
        //        }
        //        PageDto<SoundRecordDto> p = new PageDto<SoundRecordDto>();
        //        p.data = l;
        //        p.pageSize = pageSize;
        //        p.total = soundRecordDtos.Count;
        //        p.currPage = pageNum;
        //        p.totalPage = soundRecordDtos.Count%pageSize==0?soundRecordDtos.Count/pageSize:(soundRecordDtos.Count/pageSize)+1;

        //        return Ok(new { Code = 0, Data = p });
        //    }

        //}
        /// <summary>
        /// 统计
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IHttpActionResult getStatistics()
        {
            DatabaseInfoServiceImp databaseInfo = new DatabaseInfoServiceImp();
            List<DatabaseInfo> databaseInfos = databaseInfo.getDatabaseAll();
            CallRecordServiceImp callRecordServiceImp = new CallRecordServiceImp();
            List<StatisticsDto> statistics = new List<StatisticsDto>();
            foreach (DatabaseInfo d in databaseInfos)
            {
                Check check = new Check();
                int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd, out string constr);
                if (flag == 1)
                {
                    StatisticsDto statisticsDtos = callRecordServiceImp.getDayWeekMonTime(d.databaseAddress, d.databaseName, d.userName, d.databasePwd, d.anotherName);
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
        [Authorize]
        public IHttpActionResult exportExcel(string keyword)
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
                int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd, out string constr);
                if (flag == 1)
                {
                    if (keyword == null || keyword.Equals(""))
                    {
                        List<CallRecordDto> callRecordDtos = callRecordServiceImp.getExportCallRecordAll(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd);
                        foreach (CallRecordDto call in callRecordDtos)
                        {
                            s.Add(call);
                        }
                    }
                    else
                    {


                        List<CallRecordDto> callRecordDtos = callRecordServiceImp.getExportCallRecordByKey(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd, keyword);
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
            string savepath = "/Export/通话记录.xlsx";
            try
            {
                string fullpath = AppDomain.CurrentDomain.BaseDirectory + savepath;
                if (File.Exists(fullpath))
                    File.Delete(fullpath);
                if (!Directory.Exists(Path.GetDirectoryName(fullpath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
                exportExcel.ListToExcel(s, fullpath);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return Ok(new { Code = 1, Msg = "导出失败!" });
            }
            return Ok(new { Code = 0, Data = savepath, Msg = "导出成功!" });

        }
        /// <summary>
        /// 获取全部数据库信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
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
        [Authorize]
        public IHttpActionResult getPages(int pageNum, int pageSize, string keyword)
        {

            int start = (pageNum - 1) * pageSize + 1;
            int end = pageSize * pageNum;
            int f = 0;
            //获取全部数据库
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

                    int flag = check.checkDatabase(d.databaseAddress, d.databaseName, d.userName, d.databasePwd, out string constr);
                    if (flag == 1)
                    {

                        if (keyword == null || keyword.Equals(""))
                        {
                            List<CallRecordDto> callRecords = callRecord.getCallRecordAll(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd,start,end);
                            foreach (CallRecordDto callRecordDto in callRecords)
                            {

                                c.Add(callRecordDto);
                            }
                        }
                        else
                        {
                            List<CallRecordDto> callRecords = callRecord.getCallRecordByKey(d.databaseAddress, d.databaseName, d.userName, d.anotherName, d.databasePwd, keyword,start,end);
                            f++;
                            foreach (CallRecordDto callRecordDto in callRecords)
                            {
                                c.Add(callRecordDto);
                            }
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
                    int currpage = pageNum;
                    int totalpage = c.Count % pageSize == 0 ? c.Count / pageSize : (c.Count / pageSize) + 1;
                    int total = c.Count;

                    //返回pageDto
                    //List<CallRecordDto> l = new List<CallRecordDto>();
                    //if (end < c.Count)
                    //{
                    //    for (int i = start - 1; i < end; i++)
                    //    {
                    //        l.Add(c[i]);
                    //    }
                    //}
                    //else
                    //{
                    //    for (int i = start - 1; i < c.Count; i++)
                    //    {
                    //        l.Add(c[i]);
                    //    }
                    //}
                    PageDto<CallRecordDto> p = new PageDto<CallRecordDto>();
                    p.data = c;
                    p.pageSize = pageSize;
                    p.total = total;
                    p.currPage = pageNum;
                    p.totalPage = totalpage;


                    //if (f ==0) {
                        return Ok(new { Code = 0, Data = p });
                    //}
                    //else {
                    //    if (c == null || c.Count == 0) {
                    //        return Ok(new { Code = 1, Msg = "查询结果为空！" });
                    //    }
                    //    else {
                    //        return Ok(new { Code = 0, Data = p });
                    //    }
                    //}
                    
                   
                    
                }
                else
                {
                    string str =string.Join(",",errname);
                    //for (int i = 0; i < errname.Count; i++)
                    //{

                    //    if (i == errname.Count - 1)
                    //    {
                    //        str += errname[i].ToString();
                    //    }
                    //    str += errname[i].ToString() + ",";

                    //}

                    //返回pageDto
                    //List<CallRecordDto> l = new List<CallRecordDto>();
                    //if (end < c.Count)
                    //{
                    //    for (int i = start - 1; i < end; i++)
                    //    {
                    //        l.Add(c[i]);
                    //    }
                    //}
                    //else
                    //{
                    //    for (int i = start - 1; i < c.Count; i++)
                    //    {
                    //        l.Add(c[i]);
                    //    }
                    //}
                    PageDto<CallRecordDto> p = new PageDto<CallRecordDto>();
                    p.currPage = pageNum;
                    p.totalPage = c.Count % pageSize == 0 ? c.Count / pageSize : (c.Count / pageSize) + 1;
                    p.total = c.Count;
                    p.pageSize = pageSize;
                    p.data = c;

                    return Ok(new { Code = 1, Msg = str + "频道异常！", Data = p });
                    //return Ok(new { Code = 1,  Data = p });

                }
            }
            catch (Exception e)
            {
                return Ok(new { Code = 1, Msg = e.Message });
            }

        }







    }
}
