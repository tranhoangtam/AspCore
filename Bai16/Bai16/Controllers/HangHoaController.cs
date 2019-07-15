using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D16_EFCore_CodeFirst.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace D16_EFCore_CodeFirst.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly MyDbContext _context;

        public HangHoaController(MyDbContext context)
        {
            _context = context;
        }

        // GET: HangHoa
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.HangHoas.Include(h => h.Loai);
            return View(await myDbContext.ToListAsync());
        }

        // GET: HangHoa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .Include(h => h.Loai)
                .FirstOrDefaultAsync(m => m.MaHH == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // GET: HangHoa/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            return View();
        }

        // POST: HangHoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHH,TenHH,Hinh,MoTa,DonGia,SoLuong,MaLoai")] HangHoa hangHoa, List<IFormFile> fHinh)
        {
            if (ModelState.IsValid)
            {
                foreach (var myfile in fHinh)
                {
                    string filename = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "HangHoa", myfile.FileName);
                    using (var f = new FileStream(filename, FileMode.Create))
                    {
                        myfile.CopyTo(f);
                    }
                }
                //Hinh: h1.gif;h2.png;h3.jpg
                var filenames = fHinh.Select(p => p.FileName).ToList();
                hangHoa.Hinh = string.Join(";", filenames);

                //xóa hình
                //System.IO.File.Exists() --> System.IO.File.Delete()

                //if(fHinh != null)
                //{
                //    string filename = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "HangHoa", fHinh.FileName);
                //    using (var f = new FileStream(filename, FileMode.Create))
                //    {
                //        fHinh.CopyTo(f);
                //        hangHoa.Hinh = fHinh.FileName;
                //    }
                //}
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        // GET: HangHoa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        // POST: HangHoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHH,TenHH,Hinh,MoTa,DonGia,SoLuong,MaLoai")] HangHoa hangHoa, IFormFile fHinh)
        {
            if (id != hangHoa.MaHH)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (fHinh != null)
                {
                    string filename = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "HangHoa", fHinh.FileName);
                    using (var f = new FileStream(filename, FileMode.Create))
                    {
                        fHinh.CopyTo(f);
                        hangHoa.Hinh = fHinh.FileName;
                    }
                }
                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.MaHH))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        // GET: HangHoa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .Include(h => h.Loai)
                .FirstOrDefaultAsync(m => m.MaHH == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // POST: HangHoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            _context.HangHoas.Remove(hangHoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoas.Any(e => e.MaHH == id);
        }


        public IActionResult CreateOrEdit(int? id)
        {
            HangHoa hh = new HangHoa();
            if(id.HasValue)
            {
                hh = _context.HangHoas.SingleOrDefault(p => p.MaHH == id.Value);

                if (hh == null) hh = new HangHoa();
            }

            ViewBag.Loai = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            return PartialView(hh);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(int? id, HangHoa hangHoa, IFormFile fHinh)
        {
            if (ModelState.IsValid)
            {
                if (fHinh != null)
                {
                    string filename = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "HangHoa", fHinh.FileName);
                    using (var f = new FileStream(filename, FileMode.Create))
                    {
                        fHinh.CopyTo(f);
                        hangHoa.Hinh = fHinh.FileName;
                    }
                }
                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.MaHH))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }            

            return RedirectToAction("Index");
        }
    }
}
