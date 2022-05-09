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
            return View(data.Saches.ToList());
        }

        public ActionResult SachDocNhieu()
        {
            List<sosachmuon> list = new List<sosachmuon>();

            List<Sach> sach = new List<Sach>();
            sach = data.Saches.ToList();

            foreach (Sach item in sach)
            {
                var sum = data.CT_PM.Where(c => c.IDSach == item.IDSach).Sum(c => c.SoLuong);

                int tem = (sum == null ? 0 : sum.Value);

                if (tem > 0)
                {
                    list.Add(new sosachmuon(item.IDSach, tem));
                }
            }
            list.Sort((a, b) => a.SoPhieuMuon.CompareTo(b.SoPhieuMuon));

            List<Sach> sachs = new List<Sach>();

            foreach (sosachmuon item in list.Take(5))
            {
                sachs.Add(data.Saches.FirstOrDefault(c => c.IDSach == item.IDsach));
            }

            return View(sachs.Take(5));
        }
        public ActionResult SachMoiCapNhat()
        {
            return View(data.Saches.Take(5).OrderByDescending(s => s.NgayXuatBan).ToList());
        }

        public ActionResult SachMuonNhieu()
        {
            List<sosachmuon> list = new List<sosachmuon>();

            List<Sach> sach = new List<Sach>();
            sach = data.Saches.ToList();

            foreach(Sach item in sach)
            {
                var sum = data.CT_PM.Where(c => c.IDSach == item.IDSach).Sum(c => c.SoLuong);
                
                int tem = (sum == null ? 0 : sum.Value);

                if(tem > 0)
                {                    
                    list.Add(new sosachmuon(item.IDSach, tem));          
                }                
            }
            list.Sort((a,b) => a.SoPhieuMuon.CompareTo(b.SoPhieuMuon));
            
            List<Sach> sachs = new List<Sach>();
            
            foreach(sosachmuon item in list.Take(5))
            {
                sachs.Add(data.Saches.FirstOrDefault(c => c.IDSach == item.IDsach));
            }

            return View( sachs.Take(5));           

        }       

        
        public ActionResult SachTheoTheLoai(string theloai)
        {
            if(theloai == null)
            {
                return View(data.Saches.ToList());
            }
            else
            {
                return View(data.Saches.Where(c => c.TheLoai1.NameCate == theloai).ToList());
            }
        }


    }
    class sosachmuon
    {
        public string IDsach { get; set; }
        public int SoPhieuMuon { get; set; }
        
        public sosachmuon(string idsach , int sophieumuon)
        {
            this.IDsach = idsach;
            this.SoPhieuMuon=sophieumuon;
        }


    }
}