using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bai13.Models
{
    public class Loai
    {
        [Display(Name="Mã loại")]
        [Key]
        public int MaLoai { get; set; }
        [Display(Name="Tên loại")]
        [Required]
        public string TenLoai { get; set; }

        [Display(Name = "Hình ")]
        public string Hinh { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
    }
}
