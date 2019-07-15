using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace D16_EFCore_CodeFirst.Models
{
    [Table("Loai")]
    public class Loai
    {
        [Key]
        public int MaLoai { get; set; }
        [Required(ErrorMessage = "Chưa nhập tên hàng hóa")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        [MaxLength(200, ErrorMessage = "Tối đa 200 kí tự")]
        public string Hinh { get; set; }
    }
}
