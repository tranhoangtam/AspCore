using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bai05.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai05.Controllers
{
    public class ProductController : Controller
    {

        Product FindByid(int id)
        {
            //dùng LINQ để tìm kiếm trên mảng
            //SingleOrDefault trả về duy nhất một phần tử hoặc null
            return danhsach.SingleOrDefault(p => p.ID == id);            
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product sp,string Hinh)
        {
            danhsach.Add(sp);
            return View("Index",danhsach);
        }
        static List<Product> danhsach = new List<Product>();
        public IActionResult Index()
        {
           
            //danhsach.Add(new Product
            //{
            //    ID=1,
            //    Name="Iphone X",
            //    Price=199,
            //    Quantity=3
            //});

            //danhsach.Add(new Product
            //{
            //    ID = 2,
            //    Name = "Iphone 6",
            //    Price = 299,
            //    Quantity = 23
            //});

            //danhsach.Add(new Product
            //{
            //    ID = 3,
            //    Name = "Samsung J7",
            //    Price = 1299,
            //    Quantity = 10
            //});

            //danhsach.Add(new Product
            //{
            //    ID = 4,
            //    Name = "Samsung Note 8",
            //    Price = 2499,
            //    Quantity = 23
            //});

            return View(danhsach);
        }

        public IActionResult Details()
        {
            throw new NotImplementedException();
        }

        public IActionResult Edit(int mahh)
        {
            Product sp = FindByid(mahh);
            if (sp != null)//tim thay
            {
                return View(sp);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(int mahh,Product sp)
        {
            Product item = FindByid(mahh);
            if (item != null)//tim thay
            {
                item.Name = sp.Name;
                item.Quantity = sp.Quantity;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Product item = FindByid(id);
            if (item != null)//tim thay
            {
                danhsach.Remove(item);
            }
            return RedirectToAction("Index");
        }
    }
}