using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLThuVien.Areas.Admin.Model
{
    public class ThongKeTheoSach
    {
        public string TenSach { get; set; }
        public int SoSachDUocMuon { get; set; }
        public int SoSachConSuDungDuoc { get; set; }
        public int SoSachHuHong { get; set; }

        public ThongKeTheoSach()
        {
        }
    }
}