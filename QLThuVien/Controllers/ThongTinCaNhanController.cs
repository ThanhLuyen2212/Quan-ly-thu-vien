using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;

namespace QLThuVien.Controllers
{
   
    public class ThongTinCaNhanController : Controller
    {
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
        // GET: ThongTinCaNhan
        public ActionResult Index()
        {
            if (Session["DocGia"] == null) return RedirectToAction("Index", "Login");
            return View((DocGia)Session["DocGia"]);
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection form)
        {
            DocGia docgia = new DocGia();
            string oldpassword = form["Old password"];
            string newpassword = form["New password"];
            string confirmpassword = form["Confirm password"];
            docgia = (DocGia)Session["DocGia"];
            if (docgia.Password != oldpassword)
            {                
                return Content("<script language='javascript' type='text/javascript'>alert     ('Thất bại! Bạn đã nhập sai mật khẩu cũ');</script>");
            }
            else           
                if (newpassword != confirmpassword)
                {                   
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Thất bại ! Mật khẩu nhập lại không đúng');</script>");
                }
            
            if (docgia.Password == oldpassword && newpassword == confirmpassword)
            {
                docgia.Password = newpassword;
                data.SaveChanges();
                return Content("<script language='javascript' type='text/javascript'>alert     ('Thành công ! Chúc mừng bạn đã đổi mật khẩu thành công');</script>");
            }

            return Content("<script language='javascript' type='text/javascript'>alert     ('Thất bại ! Có thể bạn đã nhập sai thông tin');</script>");
        
    }
    }
}