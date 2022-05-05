using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;

namespace QLThuVien.Controllers
{
    public class ListBookController : Controller
    {
        QuanLyThuVienEntities1 data = new QuanLyThuVienEntities1();
        // GET: ListBook
        public ActionResult Index(string category)
        {
            if (category == null)
            {
                var sach = data.Saches.OrderByDescending(x => x.TheLoai);
                return View(sach);
            }
            else
            {

                var sach = data.Saches.OrderByDescending(x => x.TheLoai).Where(x => x.TheLoai == int.Parse(category));
                return View(sach);
            }
        }

        public ActionResult TimKiem(string _name)
        {
            
            if (_name == null )
            {
                return View(data.Saches.ToList());                
            }
            else
            {
                return(View(data.Saches.Where(s => s.TenSach.Contains(_name)).ToList()));               
            }
        }

        public ActionResult Details(int id)
        {
            return View();
        }
    }
}