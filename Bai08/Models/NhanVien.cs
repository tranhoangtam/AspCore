using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bai08.Models
{
	public class NhanVien
	{
		[Display(Name = "Mã nhân viên")]
		[Required(ErrorMessage = "*")]
		[Remote(controller: "Demo", action: "KiemTraMaNV")]
		public string MaNv { get; set; }
		[Display(Name = "Họ tên")]
		[Required(ErrorMessage = "*")]
		public string HoTen { get; set; }
		[Display(Name = "Địa chỉ")]
		[RegularExpression(@"[1-9](\d)*(/[1-9](\d)*){0,4}([ a-zA-Z])+")]
		public string DiaChi { get; set; }
		[Display(Name = "Điện thoại")]
		[RegularExpression(@"0[35789]\d{8}")]
		public string DienThoai { get; set; }

		[DataType(DataType.Date)]
		[BirthDateCheck]
		[Display(Name = "Ngày sinh")]
		public DateTime BirthDate { get; set; }
	}
	/*
     * Mã nhân viên: Kiểm tra không trùng (trên server)
     * Địa chỉ chỉ cho nhập số, chữ, khoảng trắng, sẹc
     * Điện thoại áp dụng cho số di động 0[35789]
     * Regular Expression:
     *  kí tự: \w
     *  kí số (0 ---> 9): \d hoặc [0-9]
     *  lấy 1 trong những phần tử: [0-9], [mn]
     *  không bắt đầu bởi 1 trong những ký tự: [^mn]
     *  {m,n}   : lặp từ m --> n lần
     *  {m}     : lặp đúng m lần
     *  *       : lặp từ 0 --> n lần
     *  +       : lặp từ 1 --> n lần
     *  
     */
}