using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Bai14.Models
{
    public class MyDbContext:DbContext
    {
        //Khai báo các thuộc tính bên trong (tương ứng tạo thành các bảng)
        public DbSet<Loai> Loais { get; set; }

        public DbSet<HangHoa> HangHoas { get; set; }

        public DbSet<KhachHang> KhachHangs { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder
            optionsBuilder)
        {
            //chỉ định CSDKL dùng và chuỗi kết nối

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) //Nếu thư mục con thì nên Path.Combine để nối đường dẫn
                                                              //Ví dụ: file json nằm trong thư mục  Configs thì Path.Combine(Directory.GetCurrentDirectory(),"Config");
                .AddJsonFile("appsettings.json");
            //.AddJsonFile("myappsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("MyStoreEF");
            optionsBuilder.UseSqlServer(connectionString);
        }

        //hàm tạo có tham số sử dụng chuỗi kết nối ở ConfirgureService() trong class startup
        public MyDbContext() 
        {
            //this.Database.ProviderName
        }

        public MyDbContext(DbContextOptions opt) : base(opt)
        {
            //Add-Migration v1 trong cửa sổ Nuget để tạo script qua db


        }
    }
}
