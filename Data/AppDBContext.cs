using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADAAPI.Models;

namespace ADAAPI.Data
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

            modelBuilder.Entity<tbl_A>(entity =>
            {
                entity.Property(e => e.empFirstname).IsUnicode(false);

                entity.Property(e => e.empId).IsUnicode(false);

                entity.Property(e => e.empLastname).IsUnicode(false);

                entity.Property(e => e.empPhoneNumber).IsUnicode(false);
            });

            modelBuilder.Entity<tbl_B>(entity =>
            {
                entity.Property(e => e.empFirstname).IsUnicode(false);

                entity.Property(e => e.empId).IsUnicode(false);

                entity.Property(e => e.empLastname).IsUnicode(false);

                entity.Property(e => e.empPhoneNumber).IsUnicode(false);
            });



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<tbl_A> tbl_A { get; set; }
        public DbSet<tbl_B> tbl_B { get; set; }

    }
}