using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bai09.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            //sinh mã
            Random rd=new Random();
            string pattern = "1234567897643534534524242342423412235365";
            StringBuilder ma=new StringBuilder();//Quản lý bộ nhớ tốt hơn string + dễ thu hồi
            for (int i = 0; i < 5; i++)
            {
                ma.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            //Dùng session để lưu mã ngẩu nhiên
            HttpContext.Session.SetString("MaNgauNhien",ma.ToString());//của netCore

            return View();//thực hiên tại view empty
        }

        public string KiemTraBaoMat(string MaBaoMat) // Mã bảo mật:<input name="MaBaoMat"/>
        {
            string maBMTrenServer = HttpContext.Session.GetString("MaNgauNhien");
            return maBMTrenServer == MaBaoMat ? "true" : "false"; //giá trị trả về đúng giá trị trong javascript

        }

        public IActionResult T1()
        {
            return View();
        }

        public IActionResult T2()
        {
            return View();
        }

        public IActionResult T3()
        {
            string[] danhmuc = { "Bia","Tivi","Ốc","Ghẹ","Mì xào"};
            return PartialView("_Category", danhmuc);
        }
    }
}