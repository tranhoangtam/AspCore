using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bai15.Models
{
    public class HangHoaView
    {
        [Display(Name = "Mã hàng")]
        public int MaHh { get; set; }
        [Display(Name = "Tên hàng")]
        public string TenHh { get; set; }
        [Display(Name = "Loại")]
        public string Loai { get; set; }
        [Display(Name = "Ngày sản xuất")]
        public DateTime NgaySX { get; set; }
        [Display(Name = "Giá bán")]
        public double GiaBan { get; set; }
        [Display(Name = "Số lượng")]

        public int SoLuong { get; set; }
        [Display(Name = "Tồn")]
        public bool ConHang => SoLuong > 0;

    }
}
