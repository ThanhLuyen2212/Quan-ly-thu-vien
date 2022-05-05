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
        public ActionResult LoginAdmin(DangNhap admin)
        {
            var check = data.DangNhaps.Where(s => s.UserName == admin.UserName && s.Password == admin.Password).FirstOrDefault();
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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Resister(DocGia user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var check = data.DocGias.Where(s => s.IDDG == user.IDDG).FirstOrDefault();
                    if (check == null)
                    {
                        /*Disable entity validation tắt xác thực thực thể*/
                        data.Configuration.ValidateOnSaveEnabled = false;
                        data.DocGias.Add(user);
                        data.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorRegister = "ID này đã tồn tại";
                        return View();
                    }
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "Vui lòng nhập đầy đủ thông tin";
            }
            return View();
        }
    }
}