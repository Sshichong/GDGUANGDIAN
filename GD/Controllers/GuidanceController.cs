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
    public class GuidanceController : Controller
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


            GuidanceServiceImp gs = new GuidanceServiceImp();
            //插入数据库
            int flag = gs.AddGuidanceInformation(databaseAddress, databaseName, userName, anotherName, databasePwd, recordAddress, recordUserName, recordPwd);
            
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
        public List<Guidance> GetGuidances()
        {
            GuidanceServiceImp gs = new GuidanceServiceImp();
            List<Guidance> list = gs.GetGuidance();
            return list;
        }
    }
}