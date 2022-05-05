using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLThuVien.Models
{
    public class CartItem
    {
        public Sach giosach { get; set; }   
        public int _soluongSach { get; set; }
    }
    public class GioSach
    {
        List<CartItem> item = new List<CartItem> ();

        public IEnumerable<CartItem> Item
        {
            get { return item; }
        }

        public void Add(Sach _sach, int _quatity = 1)
        {
            var i = item.FirstOrDefault(s => s.giosach.IDSach == _sach.IDSach);
            if (i == null)
            {
                item.Add(new CartItem
                {
                    giosach = _sach,
                    _soluongSach = _quatity
                });                    
            }
            else
            {
                i._soluongSach += _quatity;
            }
        }

        public void Update (string id, int _quantity)
        {
            var i = item.Find(s => s.giosach.IDSach.Contains(id.ToString()));
            if(i != null)
            {
                if(item.Find(s => s.giosach.SoLuong > _quantity) != null)                
                    i._soluongSach = _quantity;
                else i._soluongSach = 1;
                if (item.Find(s => s.giosach.SoLuong == 0) != null)
                    i._soluongSach = 0;                
            }
        }

        public double sum()
        {
            var sum = item.Sum(s => s._soluongSach * 1);
            return sum;
        }

        public void Remove(string id)
        {
            item.RemoveAll(s => s.giosach.IDSach == id.ToString());
        }

        public int Total()
        {
            return item.Sum(s => s._soluongSach);
        }

        public void clear()
        {
            item.Clear();
        }

        public void ssss()
        {
            DateTime aDateTime = DateTime.Now;

            Console.WriteLine("Now is " + aDateTime);

            // Một khoảng thời gian. 
            // 1 giờ + 1 phút
            TimeSpan aInterval = new System.TimeSpan(0, 1, 1, 0);

            // Thêm một khoảng thời gian.
            DateTime newTime = aDateTime.Add(aInterval);
        }
    }
}