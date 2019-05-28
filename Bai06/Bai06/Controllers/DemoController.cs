using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bai06.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bai06.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            List<HangHoa> ds=new List<HangHoa>();
            ds.Add(
                new HangHoa()
                {
                    MaHH = 1,
                    TenHH = "Opp 1",
                    DonGia = 1999,
                    Hinh = "oppo-f11-pro-128gb-3-400x460.png",
                    SoLuong = 10

                });

            ds.Add(
                new HangHoa()
                {
                    MaHH = 2,
                    TenHH = "huawei",
                    DonGia = 2999,
                    Hinh = "huawei-p30-lite-400x460.png",
                    SoLuong = 20

                });

            ds.Add(
                new HangHoa()
                {
                    MaHH = 3,
                    TenHH = "iphone-6s",
                    DonGia = 599,
                    Hinh = "iphone-6s-plus-32gb-400x460.png",
                    SoLuong = 100

                });
            ds.Add(
                new HangHoa()
                {
                    MaHH = 4,
                    TenHH = "samsung-galaxy",
                    DonGia = 399,
                    Hinh = "samsung-galaxy-fold-400x460.png",
                    SoLuong = 30

                });

            //cách 1
            //ViewBag.Data = "105 Nhất nghệ";
            //return View(ds);

            ViewBag.HangHoa = ds;
            return View();
        }

        public IActionResult DocGhiFile()
        {
            return View();
        }

        public IActionResult ABC(string ghi)
        {
            return null;
        }
                 [HttpPost]
        public IActionResult DocGhiFile(HangHoa hh,string Ghi)
        {
            try
            {
                if (Ghi == "Ghi file text")
                {

                    string[] content =
                    {
                        hh.MaHH.ToString(),
                        hh.TenHH.ToString(),
                        hh.Hinh.ToString(),
                        hh.DonGia.ToString(),
                        hh.SoLuong.ToString()
                    };

                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "hanghoa.txt");

                    System.IO.File.WriteAllLines(fullPath, content);

                    ViewBag.ThongBao = "Ghi file thành công";
                }
                else if (Ghi == "Ghi file json")
                {
                    string jsonstring = JsonConvert.SerializeObject(hh);
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "hanghoa.json");

                    System.IO.File.WriteAllText(fullPath, jsonstring);
                    ViewBag.ThongBao = "Ghi file thành công";
                }
            }
            catch (Exception e)
            {
                ViewBag.Loi = "Lỗi ghi file chi tiết\n"+ e.Message;
            }
            
            return View();
        }

        public IActionResult ReadTextFile()
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "hanghoa.txt");
            string[] content = System.IO.File.ReadAllLines(fullPath);

            HangHoa hh = new HangHoa()
            {
                MaHH = Convert.ToInt32(content[0]),
                TenHH = content[1],
                Hinh = content[2],
                DonGia = Convert.ToDouble(content[3]),
                SoLuong = Convert.ToInt32(content[4])
            };


            ViewBag.HangHoa = hh;

            return View("DocGhiFile");
        }

        public IActionResult ReadJsonFile()
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "hanghoa.json");
            string content=System.IO.File.ReadAllText(fullPath);
            HangHoa hh = JsonConvert.DeserializeObject<HangHoa>(content);
            ViewBag.HangHoa = hh;
            return View("DocGhiFile", hh);
        }

        public string Sync()
        {
            Stopwatch sw=new Stopwatch();
            sw.Start();
            Demo d=new Demo();
            d.Test01();
            d.Test02();
            d.Test03();
            sw.Stop();
            return $"chạy hết {sw.ElapsedMilliseconds} ms";
        }

        public async Task<IActionResult> Async()
        {
            Stopwatch sw = new Stopwatch();
            Demo demo = new Demo();
            sw.Start();
            var a = demo.Test01Async();
            var b = demo.Test02Async();
            var c = demo.Test03Async();
            await a; await b; await c;
            sw.Stop();
            return Content($"Chạy hết {sw.ElapsedMilliseconds} ms.");
        }
    }
}