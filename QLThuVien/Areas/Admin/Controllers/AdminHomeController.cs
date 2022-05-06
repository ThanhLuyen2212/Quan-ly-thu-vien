using QLThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLThuVien.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        QuanLyThuVienEntities1 db = new QuanLyThuVienEntities1();

        // GET: Admin/AdminHome
        public ActionResult Index()
        { 

            if(Session["UserName"] == null)
            {
                return RedirectToAction("Index", "AdminLogin",new {Areas = "Admin"});
            }
            List<PhieuMuon> phieuMuons = db.PhieuMuons.Where(c => c.TrangThai == 1).ToList();
            Session["PhieuMuonDangCho"] = phieuMuons.Count();
            return View();
         

            
        }


    }
}