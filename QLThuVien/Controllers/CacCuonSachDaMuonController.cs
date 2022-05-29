using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;
namespace QLThuVien.Controllers
{
    public class CacCuonSachDaMuonController : Controller
    {
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
        // GET: CacCuonSachDaMuon
        public ActionResult Index()
        {
            if (Session["DocGia"] == null) return RedirectToAction("Index","Login");
            DocGia docGia = new DocGia();
            docGia = (DocGia)Session["DocGia"];
            List<PhieuMuon> listPhieuMuon = new List<PhieuMuon>();
            listPhieuMuon = data.PhieuMuons.Where(c => c.IDDG == docGia.IDDG).ToList();
            List<CT_PM> listCT_PMs = new List<CT_PM>();
            foreach (PhieuMuon item in listPhieuMuon)
            {
                List<CT_PM> temp = data.CT_PM.Where(c => c.IDPM == item.IDPM).ToList();
                listCT_PMs = listCT_PMs.Concat(temp).ToList();
            }
            listCT_PMs = listCT_PMs.Distinct().ToList();
            List<Sach> listSach = new List<Sach>();
            foreach (CT_PM item in listCT_PMs)
            {
                List<Sach> temp = data.Saches.Where(c => c.IDSach == item.IDSach).ToList();
                listSach = listSach.Concat(temp).ToList();
            }
            listSach = listSach.Distinct().ToList();
           
            return View(listSach);
        }
    }
}