//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLThuVien.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietSach
    {
        public string IDChiTietSach { get; set; }
        public string IDSach { get; set; }
        public string TinhTrang { get; set; }
    
        public virtual Sach Sach { get; set; }
    }
}
