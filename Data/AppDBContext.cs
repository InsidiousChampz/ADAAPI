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
using SmsUpdateCustomer_Api.Models.Admin;

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

            modelBuilder.Entity<Customer_Header>().Property(e => e.ConfirmDate).HasColumnType("datetime");
            modelBuilder.Entity<Customer_Header>().Property(e => e.ReplyDate).HasColumnType("datetime");
            modelBuilder.Entity<Customer_Header>().Property(e => e.SentDate).HasColumnType("datetime");
            modelBuilder.Entity<Customer_Header>().Property(e => e.LastUpdated).HasColumnType("datetime");

            modelBuilder.Entity<Policy_Snapshot>().Property(e => e.StartCoverDate).HasColumnType("date");
            modelBuilder.Entity<Policy_Snapshot>().Property(e => e.CancelDate).HasColumnType("date");
            

            base.OnModelCreating(modelBuilder);
        }


        // for payer.
        public DbSet<Payer_Snapshot> Payer_Snapshots { get; set; }

        // fpr policy.
        public DbSet<Policy_Snapshot> Policy_Snapshots { get; set; }

        //for customerDetail.
        public DbSet<Customer_Snapshot> Customer_Snapshots { get; set; }

        // for Log SMS.
        public DbSet<Customer_Header> Customer_Headers { get; set; }

        // for now don't use.
        public DbSet<Customer_Detail> Customer_Details { get; set; }

        // Keep record for UI12
        public DbSet<Customer_FollowUp> Customer_FollowUps { get; set; }

        //for keep new record of customer.
        public DbSet<Customer_NewProfile> Customer_NewProfiles { get; set; }

        //for keep record hotline.
        public DbSet<Customer_Profile_Hotline> Customer_Profile_Hotlines { get; set; }

        //for keep record hotline Description.
        public DbSet<Customer_Profile_Hotline_Detail> Customer_Profile_Hotline_Details { get; set; }

        //for keep transaction what's changed.
        public DbSet<Customer_Profile_Transaction> Customer_Profile_Transactions { get; set; }

        //for keep new record of customer by "Admin".
        public DbSet<AdminApprove> AdminApproves { get; set; }

        //for keep transaction record of customer by "Admin".
        public DbSet<AdminApproveTransaction> AdminApproveTransactions { get; set; }

        //for Check Rank of Policy.
        public DbSet<Customer_Policy_Rank> Customer_Policy_Ranks { get; set; }

        





    }
}