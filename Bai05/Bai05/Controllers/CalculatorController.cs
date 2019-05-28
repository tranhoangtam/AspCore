using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Bai05.Controllers
{
    //[Route("MayTinh")]
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calculate(double A,double B,string pheptoan)
        {
            double kq = 0;
            switch (pheptoan)
            {
                case "+":kq = A + B;break;
                case "-": kq = A - B; break;
                case "*": kq = A * B; break;
                case "/": kq = A / B; break;
                case "^": kq =Math.Pow(A,B); break;
                case "%": kq = A % B; break;
                default:
                    break;
            }
            ViewBag.A = A;
            ViewBag.B = B;
            ViewBag.KQ = kq;
            return View("Index");
        }
    }
}