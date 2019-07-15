using Bai13.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai13.Controllers
{
    public class LoaiController : Controller
    {
        public IActionResult Index()
        {
            LoaiDataAccessLayer dal = new LoaiDataAccessLayer();
            return View(dal.GetLoais());
        }

        public IActionResult Create()
        {
            //LoaiDataAccessLayer dal = new LoaiDataAccessLayer();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Loai lo)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            LoaiDataAccessLayer dal = new LoaiDataAccessLayer();
            int result = dal.AddLoai(lo);
            if (result == 0)
            {
                return View();
            }

            return RedirectToAction("Edit", new {id=result});
        }

        public IActionResult Edit(int id)
        {
            Loai lo=new LoaiDataAccessLayer().GetLoai(id);
            if (lo != null)
            {
                return View(lo);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id,Loai item)
        {
            Loai lo = new LoaiDataAccessLayer().GetLoai(id);
            if (lo != null)
            {
                new LoaiDataAccessLayer().UpdateLoai(item);
                return View(lo);
            }

            return RedirectToAction("Index");
        }
    }
}