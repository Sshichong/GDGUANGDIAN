using BLL.serviceImp;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GD.Controllers
{
    public class DatabaseInfoController : Controller
    {
        // GET: Guidance
        public ActionResult Index(string msg)
        {
            ViewBag.Message = msg;
            return View();
        }

        public ActionResult SetGuidInformation()
        {
            string databaseAddress = Request["datebaseAddress"];
            string databaseName = Request["databaseName"];
            string userName = Request["userName"];
            string anotherName = Request["anotherName"];
            string databasePwd = Request["databasePwd"];
            string recordAddress = Request["recordAddress"];
            string recordUserName = Request["recordUserName"];
            string recordPwd = Request["recordPwd"];


            DatabaseInfoServiceImp gs = new DatabaseInfoServiceImp();
            //插入数据库
            int flag = gs.AddDatabaseInfo(databaseAddress, databaseName, userName, anotherName, databasePwd);
            
            if (flag == 1)
            {
                return Redirect("/Guidance/Index?msg=加入成功！");
            }
            else
            {
                return Redirect("/Guidance/Index?msg=加入失败！");
            }

        }

        /// <summary>
        /// 返回设置项下的guidance数据
        /// </summary>
        /// <returns></returns>
        public List<DatabaseInfo> GetGuidances()
        {
            DatabaseInfoServiceImp gs = new DatabaseInfoServiceImp();
            List<DatabaseInfo> list = gs.getDatabaseAll();
            return list;
        }
    }
}