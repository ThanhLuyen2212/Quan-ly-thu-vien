using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLThuVien.Models;

namespace QLThuVien.Controllers
{
    public class GioSachController : Controller
    {
        QuanLyThuVienEntities1 data = new QuanLyThuVienEntities1();
        // GET: GioSach
        public GioSach GetSach()
        {
            GioSach gio = Session["GioSach"] as GioSach;
            if (gio == null || Session["GioSach"] == null)
            {
                gio = new GioSach();
                Session["GioSach"] = gio;
            }
            return gio;
        }

        // GET: GioSach
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Addto(string id)
        {
            if (Session["UserName"] == null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert     ('Bạn phải đăng nhập để thực hiện chức năng này');</script>");
            }
            else
            {
                var gio = data.Saches.SingleOrDefault(s => s.IDSach == id);
                if (gio != null)
                {
                    GetSach().Add(gio);
                }
                return RedirectToAction("Show", "GioSach");
            }            
        }

        public ActionResult Show()
        {
            if (Session["GioSach"] == null)
            {
                return RedirectToAction("Show", "GioSach");
            }

            var username = Session["UserName"];
            var password = Session["Password"];
            DocGia dg = data.DocGias.FirstOrDefault(s => s.UserName == username && s.Password == password );

            if (dg != null)
            {
               
                ViewBag.IDDG = dg.IDDG;
                ViewBag.TenDG = dg.TenDG;
            }



            GioSach gio = Session["GioSach"] as GioSach;
            return View(gio);
        }


        //FormCollection is one way of retrieving view data in controller.
        //Depending on the type of value in input,
        //you can parse its non-string value to string in the Action method.
        public ActionResult Update_quantity(FormCollection form)
        {
            GioSach gio = Session["GioSach"] as GioSach;
            string id_sach = form["Id sach"];
            int quantity = int.Parse(form["quantity"]);
            gio.Update(id_sach, quantity);
            return RedirectToAction("Show", "GioSach");
        }

        public ActionResult Remove(string id)
        {
            GioSach gio = Session["GioSach"] as GioSach;
            gio.Remove(id);
            return RedirectToAction("Show", "GioSach");
        }


        //PartialViewResult is used to return the partial view.
        //Basically, it is a class which implements the ViewResultBase abstract class that used to render partial view.
        //PartialViewResult class inherit from ViewResultBase class
        public PartialViewResult BagBook()
        {
            int total = 0;
            GioSach gio = Session["GioSach"] as GioSach;
            if (gio != null)
            {
                total = gio.Total();
            }

            ViewBag.infocart = total;
            return PartialView("BagBook");
        }

        public ActionResult Muon_Success()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MuonSach(FormCollection form)
        {
            try
            {
                GioSach gio = Session["GioSach"] as GioSach;
                PhieuMuon muon = new PhieuMuon();
                muon.IDDG = int.Parse(form["IDDocgia"]);
                muon.TenDG = form["Tendg"];
                muon.NgayMuon = DateTime.Now;
                muon.IDDG = int.Parse(form["IDdocgia"]);
                muon.TienPhat = 0;
                muon.GhiChu = "";
                muon.TrangThai = 1;
                muon.NgayTra = DateTime.Parse(form["NgayTra"]);

                DateTime ngaymuon = Convert.ToDateTime(muon.NgayMuon);
                DateTime ngaytra = Convert.ToDateTime(muon.NgayTra);

                TimeSpan Time = ngaytra - ngaymuon;
                int TongSoNgay = Time.Days;

                if (TongSoNgay > 15)
                {
                    //return Content("Lỗi ! Thời gian mượn tối đa là 15 ngày");
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Lỗi ! Thời gian mượn tối đa là 15 ngày');</script>");
                }
                if (muon.TrangThai == 2)
                {
                    //return Content("Lỗi ! Thời gian mượn tối đa là 15 ngày");
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Lỗi ! Bạn chưa trả sách đã mượn nên không được mượn');</script>");
                }
                if (TongSoNgay <= 0)
                {
                    //return Content("Lỗi ! Vui lòng kiểm tra lại mốc thời gian");
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Lỗi ! Vui lòng kiểm tra lại mốc thời gian');</script>");
                }

                int total = gio.Total();
                if (total > 3)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert     ('Tối đa được mượn 3 loại/quyển sách');</script>");
                    //return Content("Tối đa được mượn 3 loại sách");
                }

                //Add phiếu mượn
                data.PhieuMuons.Add(muon);
                data.SaveChanges();


                foreach (var item in gio.Item)
                {
                    int tongsach = 0;
                    tongsach = tongsach + 1;


                    if (item._soluongSach == 0)
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert     ('Vui lòng kiểm tra số lượng! Do có sách không còn đủ số lượng');</script>");
                        //return Content("Vui lòng kiểm tra số lượng! Do có sách không còn đủ số lượng");

                    };
                    if (tongsach == 0)
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert     ('Không có sách thì không thể tạo phiếu mượn!');</script>");
                    }
                   
                    CT_PM Detail = new CT_PM();                  
                    Detail.IDPM = muon.IDPM;
                    Detail.IDSach = item.giosach.IDSach;
                    Detail.TenSach = item.giosach.TenSach;
                    Detail.IDDG = muon.IDDG;
                    Detail.TenDG = muon.TenDG;
                    Detail.SoLuong = item._soluongSach;
                    
          
                    foreach (var pm in data.PhieuMuons.Where(s => s.IDDG == muon.IDDG))
                    {
                        if (pm.TienPhat != 0)
                        {
                            //return Content("Độc giả chưa đóng tiền phạt thì không được mượn thêm sách!");
                            return Content("<script language='javascript' type='text/javascript'>alert     ('Độc giả chưa đóng tiền phạt thì không được mượn thêm sách!');</script>");
                        }
                    }

                    foreach (var p in data.Saches.Where(s => s.IDSach == Detail.IDSach))
                    {
                        var update_soluong = p.SoLuong - item._soluongSach;
                        p.SoLuong = update_soluong;
                    }
                    foreach (var p in data.Saches.Where(s => s.IDSach == Detail.IDSach))
                    {
                        if (p.SoLuong < item._soluongSach)
                        {
                            /* return Content("Không đủ sách theo yêu cầu của Độc Giả!");*/
                            return Content("<script language='javascript' type='text/javascript'>alert     ('Không đủ sách theo yêu cầu của Độc Giả!');</script>");
                        }
                    }

                  
                    //Add chi tiết phiếu mượn
                    data.CT_PM.Add(Detail);
                    data.SaveChanges();
                }
                data.SaveChanges();
                gio.clear();
                return RedirectToAction("Muon_Success", "GioSach");
            }
            catch
            {
                return Content("Vui lòng kiểm tra lại thông tin!");
            }
        }
    }
}