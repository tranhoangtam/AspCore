using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bai08.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai08.Controllers
{
    public class DemoController : Controller
    {
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult KiemTraMaNV(string MaNv)
		{
			string[] dsnv = { "admin", "employee", "nhatnghe" };
			if (dsnv.Contains(MaNv))//đã có
			{
				return Json(data: "Mã này đã có");
			}
			return Json(data: true);
		}

		[HttpPost]
		public IActionResult Index(NhanVien nv)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("loi", "Chưa hợp lệ");
			}
			return View();
		}

		public IActionResult jQueryValidate()
		{
			return View();
		}
	}
}