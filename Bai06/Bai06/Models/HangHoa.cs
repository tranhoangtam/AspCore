using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bai06.Models
{
    public class HangHoa
    {
        //asp- là mã C# không phải thuộc tính html
        [Display(Name = "Mã hàng hóa ")]
        [Key]
        [Required(AllowEmptyStrings = false,ErrorMessage = "Không được bỏ trống và duy nhất")]
        

        public int MaHH { get; set; }
        [Display(Name = "Tên hàng hóa")]
        [Required]
        public string TenHH { get; set; }
        [Display(Name = "Hình")]
        public string Hinh { get; set; }
        [Display(Name = "Đơn giá")]
        public double DonGia { get; set; }
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }
        
    }
}
