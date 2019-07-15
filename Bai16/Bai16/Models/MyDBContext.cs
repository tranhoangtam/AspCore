using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D16_EFCore_CodeFirst.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Loai> Loais { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext>
       options) : base(options)
        {
        }
    }
}
