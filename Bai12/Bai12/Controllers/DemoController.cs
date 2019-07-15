using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using D12_ADONET.Models;
using Microsoft.AspNetCore.Mvc;

namespace D12_ADONET.Controllers
{
    public class DemoController : Controller
    {
        string chuoiKetNoi = "Server =.; Database=MyeStore; Integrated security=true";
        public IActionResult Index()
        {
            SqlConnection connection = new SqlConnection(chuoiKetNoi);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Loai", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            ViewBag.Data = dataTable;
            return View();
        }

        public IActionResult GetAll()
        {
            SqlParameter[] pa = new SqlParameter[1];
            pa[0] = new SqlParameter("TuKhoa", "");

            DataTable dataTable = DataProvider.SelectData("spLayTatCaLoai", CommandType.StoredProcedure, pa);

            ViewBag.Data = dataTable;
            return View("Index");
        }
    }
}