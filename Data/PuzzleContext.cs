using DnDPuzzles.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDPuzzles.Data
{
    public class PuzzleContext : DbContext
    {
        private readonly IConfiguration config;

        public PuzzleContext(IConfiguration config)
        {
            this.config = config;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(config["ConnectionStrings:PuzzleContextDb"]);
        }
    }
}
