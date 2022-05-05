using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;
using System.Data.Entity;

namespace QLThuVien.Controllers
{
    public class HomeController : Controller
    {
        QuanLyThuVienEntities1 data = new QuanLyThuVienEntities1();
        public ActionResult Index()
        {
            return View();
            ViewBag.NewBook = data.Saches.Take(5).OrderByDescending(s => s.IDSach).ToList();

            // muon nhieu nhat 
            string[] idsach = new string[5];
            
            //lấy 5 cuốn sách mới nhất
            ViewBag.MostBorrowBook = data.Saches.Take(5).OrderByDescending((s) => s.IDSach).ToList();
            // lấy 5 cuốn sách được mượn nhiều nhất

            // tin tức

            // Số độc giả có trong thư viện
            ViewBag.CountDocGia = data.DocGias.Count();

            // Số cuốn sách được mượn
            ViewBag.CountPhieuMuon = data.PhieuMuons.Count();

            // Số cuốn sách có trong thư viện
            ViewBag.CountBook = data.Saches.Count();

            // Số 
            

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}