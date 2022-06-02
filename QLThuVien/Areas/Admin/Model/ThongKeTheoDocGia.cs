using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLThuVien.Areas.Admin.Model
{
    public class ThongKeTheoDocGia
    {
        public string TenDocGia { get; set; }
        public int SoLuongSachMuon { get; set; }
        public int SoTienPhat { get; set; }
        public int doanhThu { get; set; }

        public ThongKeTheoDocGia()
        {
        }
    }
}