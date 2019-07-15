using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bai15.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bai15.Controllers
{
    public class HangHoaController : Controller
    {

        private readonly MyeStoreContext ctx;
        public HangHoaController(MyeStoreContext db)
        {
            ctx = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TimKiem(string TuKhoa = "", double GiaTu = 0, double GiaDen = 0)
        {
            //Lấy hàng theo tiêu chí
            var dsHangHoa = ctx.HangHoa.AsQueryable();//ở mức định nghĩa câu Sql chưa tác động đến cơ sở dữ liệu
            if (!string.IsNullOrEmpty(TuKhoa))
            {
                dsHangHoa = dsHangHoa.Where(p => p.TenHh.Contains(TuKhoa))
                    .AsQueryable(); //&& (p.DonGia>=GiaTu || GiaTu==0) && (p.DonGia<=GiaDen || GiaDen==0));
            }

            if (GiaTu > 0)
            {
                dsHangHoa = dsHangHoa.Where(p => p.DonGia >= GiaTu).AsQueryable();
            }

            if (GiaDen > 0)
            {
                dsHangHoa = dsHangHoa.Where(p => p.DonGia <= GiaDen).AsQueryable();
            }
            //khai báo lấy thêm thông tin loại
            dsHangHoa = dsHangHoa.Include(p => p.MaLoaiNavigation);
            return View("Index",dsHangHoa);
        }

        public IActionResult DoanhSo()
        {
            //thống kê doanh thu theo loại
            var data = ctx.ChiTietHd
                //gom nhóm theo hàng hóa
                .GroupBy(p => p.MaHhNavigation)
                //sau khi gom key chinh là Hàng hóa
                .Select(p => new
                {
                    HangHoa=p.Key.TenHh,
                    SoLuong=p.Sum(q=>q.SoLuong),
                    DoanhThu=p.Sum(q=>q.SoLuong*q.DonGia)
                });

            return Json(data);
        }

        public IActionResult DoanhSoTheoLoai()
        {
            //thống kê doanh thu theo loại
            var data = ctx.ChiTietHd
                //gom nhóm theo hàng hóa
                .GroupBy(p => p.MaHhNavigation.MaLoaiNavigation)
                //sau khi gom key chinh là Hàng hóa
                .Select(p => new
                {
                    HangHoa = p.Key.TenLoai,
                    SoLuong = p.Sum(q => q.SoLuong),
                    DoanhThu = p.Sum(q => q.SoLuong * q.DonGia),
                    GiaTrungBinh=p.Sum(q=>q.SoLuong*q.DonGia)/p.Sum(q=>q.SoLuong),
                    GiaThapNhat=p.Min(q=>q.DonGia),
                    GiaCaoNhat=p.Max(q=>q.DonGia)
                });

            return Json(data);
        }

        public IActionResult ThongKe()
        {
            //thống kê doanh thu theo loại
            var data = ctx.ChiTietHd
                //gom nhóm theo hàng hóa
                .GroupBy(p => new
                {
                    Thang = p.MaHdNavigation.NgayDat.Month,
                    Nam = p.MaHdNavigation.NgayDat.Year,
                    Loai = p.MaHhNavigation.MaLoaiNavigation.TenLoai
                })
                .Select(p => new
                {
                    p.Key.Loai,
                    NamThang = $"{p.Key.Thang}/{p.Key.Nam}",
                    DoanhThu = p.Sum(q => q.SoLuong * q.DonGia)
                })
                //sắp xếp theo doanh thu
                .OrderBy(p => p.Loai)
                .ThenByDescending(p => p.DoanhThu)
                .ThenBy(p => p.NamThang);
            return Json(data);
        }

        const int SoHangHoaMoiTrang = 5;
        public IActionResult PhanTrang(int page = 1)
        {
            var data = ctx.HangHoa
                .Skip((page - 1) * SoHangHoaMoiTrang)
                .Skip(SoHangHoaMoiTrang)
                .Select(p => new HangHoaView()
                {
                    MaHh = p.MaHh,
                    TenHh = p.TenHh,
                    Loai = p.MaLoaiNavigation.TenLoai,
                    GiaBan = p.DonGia.Value,
                    NgaySX = p.NgaySx,
                    SoLuong = new Random().Next()
                });
            return View(data);
        }
    }
}