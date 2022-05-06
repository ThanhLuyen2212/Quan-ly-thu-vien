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
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        // GET: Admin/AdminPhieuMuons
        public ActionResult Index(string trangthai)
        {    
            var phieuMuons = db.PhieuMuons.Include(p => p.DocGia);
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

        // GET: Admin/AdminPhieuMuons/Create
        public ActionResult Create()
        {
            ViewBag.IDDG = new SelectList(db.DocGias, "IDDG", "TenDG");
            return View();
        }

        // POST: Admin/AdminPhieuMuons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPM,IDDG,TenDG,NgayMuon,NgayTra,TienPhat,GhiChu,TrangThai")] PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                db.PhieuMuons.Add(phieuMuon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<PhieuMuon> phieuMuons = db.PhieuMuons.Where(c => c.TrangThai == 1).ToList();
            Session["PhieuMuonDangCho"] = phieuMuons.Count();
            ViewBag.IDDG = new SelectList(db.DocGias, "IDDG", "TenDG", phieuMuon.IDDG);
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
            
            return View(phieuMuon);
        }

        // POST: Admin/AdminPhieuMuons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(phieuMuon).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<PhieuMuon> phieuMuons = db.PhieuMuons.Where(c => c.TrangThai == 1).ToList();
            Session["PhieuMuonDangCho"] = phieuMuons.Count();
            ViewData["trangthai"] = new SelectList(db.TrangThais, "IDTrangThai", "TenTrangThai", phieuMuon.TrangThai);

            return View(phieuMuon);
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
