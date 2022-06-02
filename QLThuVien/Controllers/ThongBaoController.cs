using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;
namespace QLThuVien.Controllers
{
    public class ThongBaoController : Controller
    {
        QuanLyThuVienEntities data = new QuanLyThuVienEntities();
        // GET: PhieuMuon
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CacCuonSachDaMuon()
        {
            if (Session["DocGia"] == null) return RedirectToAction("Index", "Login");
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

            return View(listCT_PMs);
        }

        public ActionResult TatCaCacPhieuMuon()
        {
            if (Session["DocGia"] == null) return RedirectToAction("Index", "Login");
            DocGia docGia = new DocGia();

            docGia = (DocGia)Session["DocGia"];

            List<PhieuMuon> listPhieuMuon = new List<PhieuMuon>();

            listPhieuMuon = data.PhieuMuons.Where(c => c.IDDG == docGia.IDDG).ToList();

            return View(listPhieuMuon);
        }
                

        public ActionResult CacphieuMuonDangChoMuon()
        {
            if (Session["DocGia"] == null) return RedirectToAction("Index", "Login");
            
            DocGia khachhang = (DocGia)Session["DocGia"];
            List<PhieuMuon> ListPheuChoMuon = data.PhieuMuons.Where(c => c.IDDG == khachhang.IDDG && c.TrangThai == 1).ToList();

           
            return View(ListPheuChoMuon);
        }

        public ActionResult HuyPhieuMuon(FormCollection form)
        {
            if (Session["DocGia"] == null) return RedirectToAction("Index", "Login");

            int id = int.Parse(form["ID PhieuMuon"].ToString());

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                PhieuMuon pm = data.PhieuMuons.FirstOrDefault(c => c.IDPM == id);

                if (pm == null)
                {
                    return HttpNotFound();
                }

                data.PhieuMuons.Remove(pm);
                data.SaveChanges();

                return RedirectToAction("CacphieuMuonDangChoMuon", "ThongBao");
           

        }

    }
}