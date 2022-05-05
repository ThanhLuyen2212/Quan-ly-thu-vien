using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;


namespace QLThuVien.Areas.Admin.Controllers
{
    
    public class AdminLoginController : Controller
    {

        QuanLyThuVienEntities1 data = new QuanLyThuVienEntities1();

        // GET: Admin/AdminLoign
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(Models.Admin admin)
        {
            var check = data.Admins.Where(s => s.UserName == admin.UserName && s.Password == admin.Password).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai thông tin tài khoản";
                return View("Index");
            }
            else
            {
                data.Configuration.ValidateOnSaveEnabled = false;
                Session["Username"] = admin.UserName;
                Session["Password"] = admin.Password;
                return RedirectToAction("Index", "AdminHome", new {Areas = "Admin"});
            }
        }

    }
}