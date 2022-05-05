using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;

namespace QLThuVien.Controllers
{
    public class ChiTietCuonSachController : Controller
    {

        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();
        // GET: ChiTietCuonSach
        public ActionResult Index(string id)
        {
            
            if (id == null)
            {
                return RedirectToAction("Index", "ListBook");
            }
            else
            {
                Sach sach = new Sach();
                sach =  db.Saches.FirstOrDefault( n => n.IDSach.Equals(id));               
                ViewBag.sach = sach;
                return View();
            }
        }
    }
}