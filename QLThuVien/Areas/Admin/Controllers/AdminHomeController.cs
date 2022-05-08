using QLThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLThuVien.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        // GET: Admin/AdminHome
        public ActionResult Index()
        { 

            if(Session["UserName"] == null)
            {
                return RedirectToAction("Index", "AdminLogin",new {Areas = "Admin"});
            }
            List<PhieuMuon> phieuMuons = db.PhieuMuons.Where(c => c.TrangThai == 1).ToList();
            Session["PhieuMuonDangCho"] = phieuMuons.Count();

            List<DocGia> docGias = db.DocGias.ToList();
            Session["Độc giả"] = docGias.Count();

            List<Sach> saches = db.Saches.ToList();
            Session["Số sách"] = saches.Count();

            List<PhieuMuon> phieuMuons1 = db.PhieuMuons.ToList();
            Session["Số phiếu mượn"] = phieuMuons1.Count();

            List<PhieuMuon> phieuMuons2 = db.PhieuMuons.Where(c => c.TrangThai == 2).ToList();
            Session["Số phiếu mượn chưa được trả"] = phieuMuons2.Count();


            List<TheLoai> theLoais = db.TheLoais.ToList();
            List<TheLoai_Sach> theLoai_Saches = new List<TheLoai_Sach>();
         /*   foreach (TheLoai item in theLoais)
            {                
                int sum = (int)db.Saches.Where(c => c.TheLoai1 == theLoais[0]).Sum(s => s.SoLuong);
                TheLoai_Sach theLoai_Sach = new TheLoai_Sach(item.NameCate, sum);
                theLoai_Saches.Add(theLoai_Sach);
            }*/

           /* Session["Số Sách Theo Thệ Loại"] = theLoai_Saches;*/

            return View();      
        }
    }

    public class TheLoai_Sach
    {
        string theloai { get; set; }
        int soluong { get; set; }
        
        public TheLoai_Sach(string theloai, int soluong)
        {
            this.theloai = theloai;
            this.soluong = soluong;
        }
    }
}