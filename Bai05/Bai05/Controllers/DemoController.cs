using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bai05.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            //ViewBag để truyền dữ liệu tới View
            ViewBag.Ten = "Index";
            return View();
        }

        //Khi đinh nghĩa Route thì action này sẽ ko mặc định
        [Route("A")]//http://localhost/A
        [Route("A/B")]//http://localhost/A/B
        [Route("/C")]//http://localhost/C
        //[Route("/")] //http://localhost/: dấu / tương tự thư mục gốc 
        // [Route("")]//http://localhost/: bỏ action
        public IActionResult Test1()
        {
            ViewBag.Ten = "Test";
            //goi toi view index de hien thi
            return View("Index");
        }

        [Route("Test/{chuoi}")]
        [Route("dien-thoai/{chuoi}")]
        public IActionResult Test2(string chuoi)
        {
            ViewBag.Ten = chuoi;
            //goi toi view index de hien thi
            return View("Index");
        }

        [Route("{loai}/{chuoi}")]
        public IActionResult Test3(string loai,string chuoi)
        {
            ViewBag.Ten =$"{loai} --> {chuoi}";//$ tương đương string.format C#7.0
            //goi toi view index de hien thi
            return View("Index");
        }

        //[Route("danhgia?lo={loai}&ch={chuoi}")] ko định nghĩa đc dạng này
        //public IActionResult Test4(string loai, string chuoi)
        //{
        //    ViewBag.Ten = $"Kết quả: {loai} --> {chuoi}";//$ tương đương string.format C#7.0
        //    //goi toi view index de hien thi
        //    return View("Index");
        //}      
        
    }
}