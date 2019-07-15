using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bai14.Models
{
    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        [Display(Name = "Mã hàng hóa")]
        public int MaHH
        {
            get;
            set;
        }

        [Required]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [Display(Name = "Tên hàng hóa")]
        public string TenHH
        {
            get;
            set;
        }

        [Display(Name = "Đơn giá")]
        public double DonGia
        {
            get;
            set;
        }

        [Display(Name = "Số lượng")]
        public int SoLuong
        {
            get;
            set;
        }

        [MaxLength(250)]
        [Display(Name = "Hình")]
        public string Hinh
        {
            get;
            set;
        }

        [Display(Name = "Mã loại")]
        public int MaLoai
        {
            get;
            set;
        }

        [ForeignKey("MaLoai")]
        public Loai Loai
        {
            get;
            set;
        }
    }

}
