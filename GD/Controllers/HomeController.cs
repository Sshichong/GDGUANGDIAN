using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.serviceImp;
using Model;

namespace GD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string msg)
        {
            ViewBag.Message = msg;
            return View();
        }

        public ActionResult Login()
        {
            string userName = Request["username"];
            string password = Request["password"];
            UserServiceImp user = new UserServiceImp();
            User u = user.getUser(userName);
            if (u.userName.Equals(userName) && u.userPwd.Equals(password))
            {
                return View();
            }
            else
            {
                return Redirect("/Home/Index?msg=用户名或密码错误！");
            }
            

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}