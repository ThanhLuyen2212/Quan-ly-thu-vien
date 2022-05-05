using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;

namespace QLThuVien.Controllers
{
    public class LoginController : Controller
    {
        QuanLyThuVienEntities1 data = new QuanLyThuVienEntities1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAcc(DocGia user)
        {
            var check = data.DocGias.Where(s => s.UserName == user.UserName && s.Password == user.Password).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfor = "Sai thông tin tài khoản";
                return View("Index");
            }
            else
            {
                data.Configuration.ValidateOnSaveEnabled = false;
                Session["UserNmae"] = user.UserName;
                Session["Password"] = user.Password;
                return RedirectToAction("Index", "ListBook");
            }
        }

        

    }
}