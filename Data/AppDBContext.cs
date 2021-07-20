using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerProFileAPI.Models;
using CustomerProFileAPI.Models.Product;
using CustomerProFileAPI.Models.Order;
using CustomerProFileAPI.Models.Customer_Snapshots;
using CustomerProFileAPI.Models.Customer_Infomations;

namespace CustomerProFileAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<Policy_Snapshot>()
                 .Property(x => x.Premium)
                 .HasColumnType("decimal(16,2)");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer_Snapshot> Customer_Snapshots { get; set; }
        public DbSet<Policy_Snapshot> Policy_Snapshots { get; set; }
        public DbSet<Customer_Header> Customer_Headers { get; set; }
        public DbSet<Customer_Detail> Customer_Details { get; set; }


        
    }
}