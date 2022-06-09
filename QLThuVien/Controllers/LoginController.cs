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
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAcc(DocGia user)
        {
            DocGia check = data.DocGias.Where(s => s.UserName == user.UserName && s.Password == user.Password).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai thông tin tài khoản";
                return View("Index");
            }
            else
            {
                data.Configuration.ValidateOnSaveEnabled = false;

                Session["UserName"] = user.UserName;
                Session["Password"] = user.Password;
                Session["TenDocGia"] = check.TenDG;
                Session["DocGia"] = check;


                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(DocGia user)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    DocGia check = data.DocGias.Where(s => s.IDDG == user.IDDG).FirstOrDefault();
                    if (check == null)
                    {
                        data.Configuration.ValidateOnSaveEnabled = false;
                        data.DocGias.Add(user);
                        data.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorRegister = "Kiểm tra lại thông tin";
                        return View();
                    }
                }
            }
            catch
            {
                ViewBag.ErrorRegister = "Vui lòng điền đầy đủ và kiểm tra lại thông tin !";
            }
            return View();

        }


    }
}