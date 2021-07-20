﻿// <auto-generated />
using System;
using CustomerProFileAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerProFileAPI.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomerProFileAPI.Models.Customer_Infomations.Customer_Detail", b =>
                {
                    b.Property<int>("DetailCustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime");

                    b.Property<int>("PayerID")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("PersonType")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("DetailCustomerID");

                    b.ToTable("DetailCustomer","ifo");
                });

            modelBuilder.Entity("CustomerProFileAPI.Models.Customer_Infomations.Customer_Header", b =>
                {
                    b.Property<int>("HeaderCustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ConfirmDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsAgentConfirm")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCustomerReply")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSMSSended")
                        .HasColumnType("bit");

                    b.Property<string>("LoginIdentityCard")
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.Property<string>("LoginLastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LoginRefCode")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("PayerPersonId")
                        .HasColumnType("int");

                    b.Property<string>("PrimaryPhone")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<DateTime>("ReplyDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime");

                    b.HasKey("HeaderCustomerID");

                    b.ToTable("HeaderCustomer","ifo");
                });

            modelBuilder.Entity("CustomerProFileAPI.Models.Customer_Snapshots.Customer_Snapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("Customer_guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("IdentityCard")
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime");

                    b.Property<string>("LineID")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("PrimaryPhone")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("SecondaryPhone")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<int?>("TitleId")
                        .HasColumnType("int");

                    b.Property<string>("WorkAddress1")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("WorkAddress2")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("WorkAddressDistrict")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("WorkAddressId")
                        .HasColumnType("int");

                    b.Property<string>("WorkAddressName")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("WorkAddressProvince")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("WorkAddressSubDistrict")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("WorkAddressSubDistrictCode")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("WorkAddressZipCode")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.ToTable("SnapCustomer","ss");
                });

            modelBuilder.Entity("CustomerProFileAPI.Models.Customer_Snapshots.Policy_Snapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationCode")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("CustName")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("CustPersonId")
                        .HasColumnType("int");

                    b.Property<Guid?>("Cust_guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime");

                    b.Property<string>("PayerName")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<Guid?>("Payer_guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Premium")
                        .HasColumnType("decimal(16,2)");

                    b.Property<string>("Product")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("ProductType")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("SnapPolicy","ss");
                });
#pragma warning restore 612, 618
        }
    }
}
