using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerProFileAPI.Models;
using CustomerProFileAPI.Models.Product;
using CustomerProFileAPI.Models.Order;

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
            //modelBuilder.Entity<UserRole>().HasKey(x => new { x.UserId, x.RoleId });
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAudit> ProductAudits { get; set; }
        public DbSet<ProductAuditType> ProductAuditTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }



    }
}