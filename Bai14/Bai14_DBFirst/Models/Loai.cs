using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bai14_DBFirst.Models
{
    public partial class Loai
    {
        public Loai()
        {
            HangHoa = new HashSet<HangHoa>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Ma Loai is required")]
        [Display(Name = "Ma Loai")]
        public int MaLoai { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Ten Loai is required")]
        [Display(Name = "Ten Loai")]
        public string TenLoai { get; set; }
        [MaxLength]
        [Display(Name = "Mo Ta")]
        public string MoTa { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Hinh")]
        public string Hinh { get; set; }
        public ICollection<HangHoa> HangHoa { get; set; }
    }
}
