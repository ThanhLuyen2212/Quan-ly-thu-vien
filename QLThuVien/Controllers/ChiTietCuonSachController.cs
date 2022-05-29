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

        private QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        // GET: ChiTietCuonSach
        public ActionResult Index(string id)
        {
            
            if (id == null)
            {
                return RedirectToAction("Index", "Sach");
            }
            else
            {

                return View(db.Saches.FirstOrDefault(n => n.IDSach.Equals(id)));
            }
        }
    }
}