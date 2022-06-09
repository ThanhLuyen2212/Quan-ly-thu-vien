using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;
using QLThuVien.Areas.Admin.Model;

namespace QLThuVien.Areas.Admin.Controllers
{
    public class AdminThongKeController : Controller
    {
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
        // GET: Admin/AdminThongKe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongKeTheoSach()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Index", "AdminLogin", new { Areas = "Admin" });
            }

            List<Sach> listSach = new List<Sach>();

            listSach = data.Saches.ToList();
            List<ThongKeTheoSach> thongke = new List<ThongKeTheoSach>();

            foreach (Sach item in listSach)
            {
                if (data.CT_PM.FirstOrDefault(c => c.IDSach == item.IDSach) == null) continue;
                ThongKeTheoSach sach = new ThongKeTheoSach();
                sach.TenSach = item.TenSach;

                sach.SoSachDUocMuon = data.CT_PM.Count(c => c.IDSach == item.IDSach);
                sach.SoSachConSuDungDuoc = data.ChiTietSaches.Where(c => c.TinhTrang != "Đang còn sử dụng").Where(c => c.IDSach == item.IDSach).Count();
                sach.SoSachHuHong = data.ChiTietSaches.Where(c => c.TinhTrang == "Hư hỏng").Count();
                thongke.Add(sach);
            }

            return View(thongke);
        }

        // thống kê theo độc giả
        public ActionResult ThongKeTheoDocGia()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Index", "AdminLogin", new { Areas = "Admin" });
            }

            List<DocGia> listDocGia = new List<DocGia>();

            listDocGia = data.DocGias.ToList();
            List<ThongKeTheoDocGia> thongke = new List<ThongKeTheoDocGia>();
            
            foreach(DocGia item in listDocGia)
            {
                if (data.PhieuMuons.FirstOrDefault(c => c.IDDG == item.IDDG) == null) continue;
                ThongKeTheoDocGia dg = new ThongKeTheoDocGia();
                dg.TenDocGia = item.TenDG;
                dg.SoLuongSachMuon = 0;
                List<PhieuMuon> phieuMuons = new List<PhieuMuon>();
                phieuMuons = data.PhieuMuons.Where(c => c.IDDG == item.IDDG).ToList();
                foreach(PhieuMuon phieuMuon in phieuMuons)
                {
                    dg.SoLuongSachMuon += (int)data.CT_PM.Where(c => c.IDPM == phieuMuon.IDPM).Sum(c => c.SoLuong);
                    dg.SoTienPhat += (int)data.PhieuMuons.Sum(c => c.TienPhat);
                }
                dg.doanhThu = dg.SoTienPhat;
                thongke.Add(dg);
            }
            return View(thongke);
        }
    }
}