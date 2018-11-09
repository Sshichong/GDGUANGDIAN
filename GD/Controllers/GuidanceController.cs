using BLL.serviceImp;
using System;
using System.Collections.Generic;
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
            int flag = gs.AddGuidanceInformation(databaseAddress, databaseName, userName, anotherName, databasePwd, recordAddress, recordUserName, recordPwd);
            
            if (flag == 1)
            {

                return View();
            }
            else
            {
                return Redirect("/Guidance/Index?msg=加入失败！");
            }

        }
    }
}