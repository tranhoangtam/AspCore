using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bai14.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
        [Display(Name = "Mã khách hàng")]
        public string MaKH
        {
            get;
            set;
        }

        [Required]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        [Display(Name = "Họ và tên")]
        public string HoTen
        {
            get;
            set;
        }

        [MaxLength(50)]
        [Display(Name = "Điện thoại")]
        public string DienThoai
        {
            get;
            set;
        }

        [MaxLength(150)]
        [Display(Name = "Email")]
        public string Email
        {
            get;
            set;
        }
    }

}
