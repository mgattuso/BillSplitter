using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class BillSplitterContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BillSplitter.db;");
        }

        public BillSplitterContext (DbContextOptions<BillSplitterContext> options)
            : base(options)
        {
        }

        public DbSet<BillSplitter.Models.RoomMate> RoomMate { get; set; }

        public DbSet<BillSplitter.Models.Payment> Payment { get; set; }

        public DbSet<BillSplitter.Models.Bill> Bill { get; set; }
    }
}
