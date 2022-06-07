using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;
using PagedList;

namespace QLThuVien.Controllers
{
    public class SachController : Controller
    {
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
        // GET: Sach
        public ActionResult Index(string theloai, string tensach, int page = 1, int pagelist = 6)
        {
            ViewBag.TheLoai = data.TheLoais.ToList();            

            if (theloai != null)
            {
                return View(data.Saches.Where(c => c.TheLoai1.NameCate.ToLower().Contains(theloai.ToLower())).OrderByDescending(c => c.NgayXuatBan).ToPagedList(page,pagelist));
            }
            if (tensach != null)
            {
                
                return View(data.Saches.Where(c => c.TenSach.ToLower().Contains(tensach.ToLower())).OrderByDescending(c => c.NgayXuatBan).ToPagedList(page, pagelist));
            }
            return View(data.Saches.OrderByDescending(c => c.NgayXuatBan).ToPagedList(page, pagelist));
        }
    }
}