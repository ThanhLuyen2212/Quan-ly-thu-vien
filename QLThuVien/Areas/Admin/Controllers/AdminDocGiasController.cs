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
    public class AdminDocGiasController : Controller
    {
        private QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        // GET: Admin/AdminDocGias
        public ActionResult Index(string TenDG)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "AdminLogin", new { Areas = "Admin" });
            }
            else
            {
                if(TenDG != null)
                {
                    List<DocGia> docGias = db.DocGias.Where(c => c.TenDG == TenDG).ToList();
                    if (docGias.Count > 0)
                    {
                        return View(docGias);
                    }
                    return View(db.DocGias.ToList());
                }
                return View(db.DocGias.ToList());
            }
            
        }

        // GET: Admin/AdminDocGias/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocGia docGia = db.DocGias.Find(int.Parse(id));
            if (docGia == null)
            {
                return HttpNotFound();
            }
            return View(docGia);
        }

        // GET: Admin/AdminDocGias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminDocGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDG,TenDG,DienThoai,DiaChi,UserName,Password")] DocGia docGia)
        {
            if (ModelState.IsValid)
            {
                db.DocGias.Add(docGia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(docGia);
        }

        // GET: Admin/AdminDocGias/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocGia docGia = db.DocGias.Find(int.Parse(id));
            if (docGia == null)
            {
                return HttpNotFound();
            }
            return View(docGia);
        }

        // POST: Admin/AdminDocGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDG,TenDG,DienThoai,DiaChi,UserName,Password")] DocGia docGia)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(docGia).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(docGia);
        }

        // GET: Admin/AdminDocGias/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocGia docGia = db.DocGias.Find(int.Parse(id));
            if (docGia == null)
            {
                return HttpNotFound();
            }
            return View(docGia);
        }

        // POST: Admin/AdminDocGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DocGia docGia = db.DocGias.Find(int.Parse(id));
            db.DocGias.Remove(docGia);
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
