using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecuritiesApi.Domain;

namespace SecuritiesApi.Data
{
    public class FinanceSecurityContext : DbContext
    {
        
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<MutualFund> MutualFunds { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        public FinanceSecurityContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // inherited table types
            // Map these class names to the table names in the DB
            modelBuilder.Entity<Security>().ToTable("Securities");
            modelBuilder.Entity<Stock>().ToTable("Stock");
            modelBuilder.Entity<MutualFund>().ToTable("MutualFund");

            
        }
        
    }
}
