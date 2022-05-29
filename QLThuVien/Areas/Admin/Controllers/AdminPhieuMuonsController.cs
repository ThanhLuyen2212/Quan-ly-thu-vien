using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;

namespace QLThuVien.Areas.Admin.Controllers
{
    public class AdminPhieuMuonsController : Controller
    {
        private QuanLyThuVienEntities db = new QuanLyThuVienEntities();

        // GET: Admin/AdminPhieuMuons
        public ActionResult Index(string timkiem)
        {    
           if(timkiem != null)
            {
                List<PhieuMuon> pm = new List<PhieuMuon>();
                if (int.TryParse(timkiem, out int id))
                {
                   pm = db.PhieuMuons.Where(c => c.IDPM == id).ToList();                                
                }
                else
                {
                   pm = db.PhieuMuons.Where(c => c.TrangThai1.TenTrangThai == timkiem || c.TenDG == timkiem).ToList();
                }
                
                if (pm.Count>0)
                {
                    return View(pm);
                }
            }

            var phieuMuons = db.PhieuMuons;
            return View(phieuMuons.ToList());
        }

        // GET: Admin/AdminPhieuMuons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuon);
        }

        // GET: Admin/AdminPhieuMuons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            List<PhieuMuon> phieuMuons = db.PhieuMuons.Where(c => c.TrangThai == 1).ToList();
            Session["PhieuMuonDangCho"] = phieuMuons.Count();
            ViewData["trangthai"] = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThai", phieuMuon.TrangThai);
            Session["TrangThaiTruocKhiThaiDoi"] = phieuMuon.TrangThai;
            return View(phieuMuon);
        }

        // POST: Admin/AdminPhieuMuons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhieuMuon phieuMuon)
        {
            List<PhieuMuon> phieuMuons = new List<PhieuMuon>();
            if (ModelState.IsValid)
            {                
                db.Entry(phieuMuon).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                phieuMuons = db.PhieuMuons.Where(c => c.TrangThai == 1).ToList();
                Session["PhieuMuonDangCho"] = phieuMuons.Count();

                // update number of book
                UpdateNumberOfBook(phieuMuon);

                return RedirectToAction("Index");
            }
          
            ViewData["trangthai"] = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThai", phieuMuon.TrangThai);
            phieuMuons = db.PhieuMuons.Where(c => c.TrangThai == 1).ToList();
            Session["PhieuMuonDangCho"] = phieuMuons.Count();
            return View(phieuMuon);
        }



        // tang, giam so luong sach trong khi tra sach
        public void UpdateNumberOfBook(PhieuMuon phieuMuon)
        {  
            int idpm = phieuMuon.IDPM;
            var trangthai = phieuMuon.TrangThai;
            if(Session["TrangThaiTruocKhiThaiDoi"] != null)
            {
                if (Session["TrangThaiTruocKhiThaiDoi"].ToString() == "1" || Session["TrangThaiTruocKhiThaiDoi"].ToString() == "2")
                {
                    if (trangthai == 3)
                    {
                        List<CT_PM> ctpm = db.CT_PM.Where(c => c.IDPM == idpm).ToList();
                        if (ctpm.Count > 0)
                        {
                            foreach (CT_PM item in ctpm)
                            {
                                Sach sach = db.Saches.FirstOrDefault(c => c.IDSach == item.IDSach);
                                sach.SoLuong = sach.SoLuong + item.SoLuong;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                else if(Session["TrangThaiTruocKhiThaiDoi"].ToString() == "3")
                {
                    if (trangthai == 2 || trangthai == 1)
                    {
                        List<CT_PM> ctpm = db.CT_PM.Where(c => c.IDPM == idpm).ToList();
                        if (ctpm.Count > 0)
                        {
                            foreach (CT_PM item in ctpm)
                            {
                                Sach sach = db.Saches.FirstOrDefault(c => c.IDSach == item.IDSach);
                                sach.SoLuong = sach.SoLuong - item.SoLuong;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                
            }
            
        }



        // GET: Admin/AdminPhieuMuons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            List<PhieuMuon> phieuMuons = db.PhieuMuons.Where(c => c.TrangThai == 1).ToList();
            Session["PhieuMuonDangCho"] = phieuMuons.Count();
            return View(phieuMuon);
        }

        // POST: Admin/AdminPhieuMuons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            db.PhieuMuons.Remove(phieuMuon);
            db.SaveChanges();
            List<PhieuMuon> phieuMuons = db.PhieuMuons.Where(c => c.TrangThai == 1).ToList();
            Session["PhieuMuonDangCho"] = phieuMuons.Count();
            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
             
            }
            base.Dispose(disposing);
        }
    }
}
