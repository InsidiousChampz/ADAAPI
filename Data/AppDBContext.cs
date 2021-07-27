using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsUpdateCustomer_Api.Models;
using SmsUpdateCustomer_Api.Models.Customer_Snapshots;
using SmsUpdateCustomer_Api.Models.Customer_Infomations;
using SmsUpdateCustomer_Api.Models.Customer_Profiles;


namespace SmsUpdateCustomer_Api.Data
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

        public DbSet<Payer_Snapshot> Payer_Snapshots { get; set; }
        public DbSet<Policy_Snapshot> Policy_Snapshots { get; set; }
        public DbSet<Customer_Snapshot> Customer_Snapshots { get; set; }
        public DbSet<Customer_Header> Customer_Headers { get; set; }
        public DbSet<Customer_Detail> Customer_Details { get; set; }
        public DbSet<Customer_NewProfile> Customer_NewProfiles { get; set; }
        public DbSet<Customer_Profile_Hotline> Customer_Profile_Hotlines { get; set; }
        public DbSet<Customer_Profile_Transaction> Customer_Profile_Transactions { get; set; }

        



    }
}