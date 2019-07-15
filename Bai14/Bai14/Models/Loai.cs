using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bai14.Models
{
    [Table("Loai")]
    public class Loai
    {
        [Key]
        [Display(Name = "Mã loại")]
        public int MaLoai
        {
            get;
            set;
        }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Tên loại")]
        public string TenLoai
        {
            get;
            set;
        }

        [MaxLength(250)]
        [Display(Name = "Mô tả")]
        public string MoTa
        {
            get;
            set;
        }

        [Display(Name = "Hình")]
        [MaxLength(250)]
        public string Hinh
        {
            get;
            set;
        }

        [Display(Name = "Sử dụng")]
        public bool SuDung
        {
            get;
            set;
        } = true;

    }
}
