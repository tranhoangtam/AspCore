using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bai14.Models
{
    [Table("Customer")]
    public class Customer: KhachHang
    {
        [MaxLength(250)]
        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

    }
}
