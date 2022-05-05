using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;

namespace QLThuVien.Controllers
{
    public class RegisterController : Controller
    {

        QuanLyThuVienEntities1 data = new QuanLyThuVienEntities1();

        [HttpGet]
        // GET: Register
        public ActionResult Index()
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
                    var check = data.DocGias.Where(s => s.IDDG == user.IDDG).FirstOrDefault();
                    if (check == null)
                    {
                        data.Configuration.ValidateOnSaveEnabled = false;
                        data.DocGias.Add(user);
                        data.SaveChanges();
                        return RedirectToAction("Index","Login");
                    }
                    else
                    {
                        ViewBag.ErrorRegister = "this id is exist";
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