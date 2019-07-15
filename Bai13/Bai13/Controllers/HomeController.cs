using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bai13.Models;
using Microsoft.Extensions.Configuration;

namespace Bai13.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DSLoai()
        {
            LoaiDataAccessLayer dal = new LoaiDataAccessLayer();
            return View(dal.GetLoais());
        }

        public IActionResult Them()
        {
            //LoaiDataAccessLayer dal = new LoaiDataAccessLayer();
            return View("DSLoai");
        }
        [HttpPost]
        public IActionResult Them(Loai lo)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            LoaiDataAccessLayer dal = new LoaiDataAccessLayer();
            int result=dal.AddLoai(lo);
            if (result == 0)
            {
                return View();
            }

            return RedirectToAction("Sua", result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ReadConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())//Nếu thư mục con thì nên Path.Combine để nối đường dẫn
                //Ví dụ: file json nằm trong thư mục  Configs thì Path.Combine(Directory.GetCurrentDirectory(),"Config");
                //.AddJsonFile("appsettings.json")
                .AddJsonFile("myappsettings.json");
                
            var config = builder.Build();
            ViewBag.Message = config["Message"];
            ViewBag.Address = config["MyConfigs:Address"];
            ViewBag.Tel = config["MyConfigs:Tel"];
            ViewBag.Hotline = config["MyConfigs:Hotline"];
            ViewBag.ConnectionString = config.GetConnectionString("MyeStore");
            ViewBag.Course = config["MyConfigs:Data:Name"];
            //var data = config["MyConfigs"];
            //var data = config.GetSection("MyConfigs");
            //var ob=data.Get<MyConfig>();
            return View("Index");
        }
    }

    //class MyConfig
    //{
    //    public string Address { get; set; }
    //    public string Tel { get; set; }
    //    public string  Hotline { get; set; }

    //    public Coures Data { get; set; }

    //    public class Coures
    //    {
    //        public string Name { get; set; }
    //        public string Version { get; set; }
    //    }
    //}
}
