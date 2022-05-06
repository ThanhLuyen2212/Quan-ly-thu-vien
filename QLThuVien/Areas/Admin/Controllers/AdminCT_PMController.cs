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
    public class AdminCT_PMController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        // GET: Admin/AdminCT_PM
        public ActionResult Index(string IDPM)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "AdminLogin", new { Areas = "Admin" });
            }          
        
            else
            {
                if(IDPM == null)
                {
                    return View(db.CT_PM.Include(c => c.DocGia).Include(c => c.PhieuMuon).Include(c => c.Sach));
                }
                else if (IDPM.Equals(""))
                {
                    return View(db.CT_PM.Include(c => c.DocGia).Include(c => c.PhieuMuon).Include(c => c.Sach));
                }
                else
                {
                    int id = int.Parse(IDPM);
                    return View(db.CT_PM.Include(c => c.DocGia).Include(c => c.PhieuMuon).Include(c => c.Sach).Where(c => c.IDPM == id));
                }
                
            }
            
        }

        // GET: Admin/AdminCT_PM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PM cT_PM = db.CT_PM.Find(id);
            if (cT_PM == null)
            {
                return HttpNotFound();
            }
            return View(cT_PM);
        }

        // GET: Admin/AdminCT_PM/Create
        public ActionResult Create()
        {
            ViewBag.IDDG = new SelectList(db.DocGias, "IDDG", "TenDG");
            ViewBag.IDPM = new SelectList(db.PhieuMuons, "IDPM", "TenDG");
            ViewBag.IDSach = new SelectList(db.Saches, "IDSach", "TenSach");
            return View();
        }

        // POST: Admin/AdminCT_PM/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDPM,IDDG,TenDG,IDSach,TenSach,SoLuong,NgayTraThucTe")] CT_PM cT_PM)
        {
            if (ModelState.IsValid)
            {
                db.CT_PM.Add(cT_PM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDG = new SelectList(db.DocGias, "IDDG", "TenDG", cT_PM.IDDG);
            ViewBag.IDPM = new SelectList(db.PhieuMuons, "IDPM", "TenDG", cT_PM.IDPM);
            ViewBag.IDSach = new SelectList(db.Saches, "IDSach", "TenSach", cT_PM.IDSach);
            return View(cT_PM);
        }

        // GET: Admin/AdminCT_PM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PM cT_PM = db.CT_PM.Find(id);
            if (cT_PM == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDG = new SelectList(db.DocGias, "IDDG", "TenDG", cT_PM.IDDG);
            ViewBag.IDPM = new SelectList(db.PhieuMuons, "IDPM", "TenDG", cT_PM.IDPM);
            ViewBag.IDSach = new SelectList(db.Saches, "IDSach", "TenSach", cT_PM.IDSach);
            return View(cT_PM);
        }

        // POST: Admin/AdminCT_PM/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDPM,IDDG,TenDG,IDSach,TenSach,SoLuong,NgayTraThucTe")] CT_PM cT_PM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cT_PM).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDG = new SelectList(db.DocGias, "IDDG", "TenDG", cT_PM.IDDG);
            ViewBag.IDPM = new SelectList(db.PhieuMuons, "IDPM", "TenDG", cT_PM.IDPM);
            ViewBag.IDSach = new SelectList(db.Saches, "IDSach", "TenSach", cT_PM.IDSach);
            return View(cT_PM);
        }

        // GET: Admin/AdminCT_PM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PM cT_PM = db.CT_PM.Find(id);
            if (cT_PM == null)
            {
                return HttpNotFound();
            }
            return View(cT_PM);
        }

        // POST: Admin/AdminCT_PM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_PM cT_PM = db.CT_PM.Find(id);
            db.CT_PM.Remove(cT_PM);
            db.SaveChanges();
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
