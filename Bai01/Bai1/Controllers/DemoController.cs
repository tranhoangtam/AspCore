using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bai1.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult MyForm()
        {
            return View();
        }

        public IActionResult XuLyHinhTron(int? banKinh)
        {
            if (banKinh.HasValue)
            {
                //ViewBag để gửi giá trị qua view
                ViewBag.ChuVi =Math.Round(2 * banKinh.Value*Math.PI,2);
                ViewBag.DienTich =Math.Round(Math.Pow(banKinh.Value, 2) * Math.PI,2);
            }
            return View();
        }

        public IActionResult XyLyHCN(int? dai,int? rong)
        {
            if (dai.HasValue && rong.HasValue)
            {
                var hcn = new Models.HinhChuNhat();
                hcn.Dai = dai.Value;
                hcn.Rong = rong.Value;
                ViewBag.ChuVi = hcn.ChuVi;
                ViewBag.DienTich = hcn.DienTich;
                ViewBag.DienGiai = hcn.Chuoi();
                return Json(hcn);
            }
            
            return View();
        }

        public IActionResult Mang()
        {
            return View();
        }

        public int SoNguyen()
        {
            return 2;
        }

        public int Tong(int a,int b)
        {
            return a+b;
        }

        public string Chao(string ten="bạn")
        {
            return $"Xin chào {ten.ToUpper()}";
        }
    }
}