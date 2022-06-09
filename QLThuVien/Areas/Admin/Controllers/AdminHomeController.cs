using QLThuVien.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using QLThuVien.Areas.Admin.Model;

namespace QLThuVien.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();

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

            List<ChiTietSach> saches = db.ChiTietSaches.ToList();
            Session["Số sách"] = saches.Count();

            List<PhieuMuon> phieuMuons1 = db.PhieuMuons.ToList();
            Session["Số phiếu mượn"] = phieuMuons1.Count();

            List<PhieuMuon> phieuMuons2 = db.PhieuMuons.Where(c => c.TrangThai == 2).ToList();
            Session["Số phiếu mượn chưa được trả"] = phieuMuons2.Count();

            ViewBag.SoLuongSachDangDuocMuon = db.CT_PM.Sum(c => c.SoLuong);


            //thong kê theo sách
            List<Sach> listSach = new List<Sach>();

            listSach = db.Saches.ToList();
            List<ThongKeTheoSach> thongke = new List<ThongKeTheoSach>();

            foreach (Sach item in listSach)
            {
                if (db.CT_PM.FirstOrDefault(c => c.IDSach == item.IDSach) == null) continue;
                ThongKeTheoSach sach = new ThongKeTheoSach();
                sach.TenSach = item.TenSach;

                sach.SoSachDUocMuon = db.CT_PM.Count(c => c.IDSach == item.IDSach);
                sach.SoSachConSuDungDuoc = db.ChiTietSaches.Where(c => c.TinhTrang != "Đang còn sử dụng" && c.IDSach == item.IDSach).Count();
                sach.SoSachHuHong = db.ChiTietSaches.Where(c => c.TinhTrang == "Hư hỏng" && c.IDSach == item.IDSach).Count();
                thongke.Add(sach);
            }

            ViewBag.ThongKeTheoSach = thongke;


            List<DocGia> listDocGia = new List<DocGia>();

            listDocGia = db.DocGias.ToList();
            List<ThongKeTheoDocGia> thongke1 = new List<ThongKeTheoDocGia>();

            foreach (DocGia item in listDocGia)
            {
                if (db.PhieuMuons.FirstOrDefault(c => c.IDDG == item.IDDG) == null) continue;
                ThongKeTheoDocGia dg = new ThongKeTheoDocGia();
                dg.TenDocGia = item.TenDG;
                dg.SoLuongSachMuon = 0;
                List<PhieuMuon> phieuMuonss = new List<PhieuMuon>();
                phieuMuonss = db.PhieuMuons.Where(c => c.IDDG == item.IDDG).ToList();
                foreach (PhieuMuon phieuMuon in phieuMuonss)
                {
                    dg.SoLuongSachMuon += (int)db.CT_PM.Where(c => c.IDPM == phieuMuon.IDPM).Sum(c => c.SoLuong);
                    dg.SoTienPhat += (int)db.PhieuMuons.Sum(c => c.TienPhat);
                }
                dg.doanhThu = dg.SoTienPhat;
                thongke1.Add(dg);
            }

            ViewBag.ThongKeTheoDocGia = thongke1;
           
            return View();      
        }
    }
}