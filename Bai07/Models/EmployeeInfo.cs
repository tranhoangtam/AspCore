using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bai07.Models
{
	public class EmployeeInfo
	{
		[Display(Name = "Mã nhân viên")]
		[Required(ErrorMessage = "*")]
		public string EmployeeNo { get; set; }

		[Display(Name = "Họ và tên")]
		[Required(ErrorMessage = "*")]
		[MinLength(5, ErrorMessage = "Tên ít nhất 5 ký tự !")]
		public String FullName { get; set; }
		[Display(Name = "Tuổi")]
		[Required(ErrorMessage = "Không để trống!")]
		[Range(18, 60, ErrorMessage = "Tuổi phải từ 18 đến 60 !")]
		public int Age { get; set; }

		[Required(ErrorMessage = "Chưa nhập")]
		[Display(Name = "Sở thích")]
		[DataType(DataType.MultilineText)]
		public string Hobbie { get; set; }

		[Display(Name = "Email")]
		[EmailAddress]
		public string Email { get; set; }
		[Display(Name = "Website")]
		[Url]
		public string Website { get; set; }
		[Display(Name = "Ngày sinh")]
		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }

		[Display(Name = "Giới tính")]
		public Gender Gender { get; set; }

		[Display(Name = "Số tài khoản")]
		[CreditCard]
		public string CreditCard { get; set; }
	}//end class

	public enum Gender
	{
		Nam = 0, Nữ = 1
	}
}
