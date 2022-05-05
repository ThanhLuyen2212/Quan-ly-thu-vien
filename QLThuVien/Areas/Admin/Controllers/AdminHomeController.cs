using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLThuVien.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
  
            if(Session["UserName"] == null)
            {
                return RedirectToAction("Index", "AdminLogin",new {Areas = "Admin"});
            }
            else
            {
                return View();
            }
        }


    }
}