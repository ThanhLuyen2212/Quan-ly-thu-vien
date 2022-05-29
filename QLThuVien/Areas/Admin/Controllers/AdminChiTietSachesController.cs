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
    public class AdminChiTietSachesController : Controller
    {
        private QuanLyThuVienEntities db = new QuanLyThuVienEntities();

        // GET: Admin/AdminChiTietSaches
        public ActionResult Index(string IDSach)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "AdminLogin", new { Areas = "Admin" });
            }
            else
            {
                if (IDSach == null)
                {
                    return View(db.ChiTietSaches.Include(c => c.Sach));
                }
                else if (IDSach.Equals(""))
                {
                    return View(db.ChiTietSaches.Include(c => c.Sach));
                }
                else
                {
                    List<ChiTietSach> chiTietSachList = db.ChiTietSaches.Where(c => c.IDSach == IDSach).ToList();    
                    if(chiTietSachList.Count > 0)
                    {
                        return View(chiTietSachList);
                    }
                    return View(db.ChiTietSaches.Where(c => c.Sach.TenSach == IDSach).ToList());
                }                 
            }           
        }

        // GET: Admin/AdminChiTietSaches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSach chiTietSach = db.ChiTietSaches.Find(id);
            if (chiTietSach == null)
            {
                return HttpNotFound();
            }
            return View(chiTietSach);
        }

        // GET: Admin/AdminChiTietSaches/Create
        public ActionResult Create()
        {
            ViewBag.IDSach = new SelectList(db.Saches, "IDSach", "TenSach");
            return View();
        }

        // POST: Admin/AdminChiTietSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDChiTietSach,IDSach,TinhTrang")] ChiTietSach chiTietSach)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietSaches.Add(chiTietSach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDSach = new SelectList(db.Saches, "IDSach", "TenSach", chiTietSach.IDSach);
            return View(chiTietSach);
        }

        // GET: Admin/AdminChiTietSaches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSach chiTietSach = db.ChiTietSaches.Find(id);
            if (chiTietSach == null)
            {
                return HttpNotFound();
            }


            ViewBag.IDSach = new SelectList(db.Saches, "IDSach", "TenSach", chiTietSach.IDSach);
            return View(chiTietSach);
        }

        // POST: Admin/AdminChiTietSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDChiTietSach,IDSach,TinhTrang")] ChiTietSach chiTietSach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietSach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.IDSach = new SelectList(db.Saches, "IDSach", "TenSach", chiTietSach.IDSach);
            return View(chiTietSach);
        }

        // GET: Admin/AdminChiTietSaches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSach chiTietSach = db.ChiTietSaches.Find(id);
            if (chiTietSach == null)
            {
                return HttpNotFound();
            }
            return View(chiTietSach);
        }

        // POST: Admin/AdminChiTietSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "IDChiTietSach,IDSach,TinhTrang")] string id)
        {
            ChiTietSach chiTietSach = db.ChiTietSaches.Find(id);

            int soluongsahc = db.ChiTietSaches.Count(c => c.IDSach == chiTietSach.IDSach && c.TinhTrang == "Đang dùng");
            Sach sach = db.Saches.Find(chiTietSach.IDSach);
            sach.SoLuong = soluongsahc;

            db.ChiTietSaches.Remove(chiTietSach);
         
            
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
