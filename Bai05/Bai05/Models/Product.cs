using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bai05.Models
{
    public class Product
    {
        [Display(Name="Mã hàng hóa")]
        public int ID { get; set; }
        [Display(Name = "Tên hàng hóa")]
        public string Name { get; set; }
        [Display(Name = "Đơn giá")]
        public double Price { get; set; }
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        [Display(Name = "Hết hàng")]
        public bool Empty => Quantity == 0;
        [Display(Name = "Diễn giải")]
        public string Description { get; set; }


    }
}
