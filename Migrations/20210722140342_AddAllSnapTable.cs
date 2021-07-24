﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerProFileAPI.Migrations
{
    public partial class AddAllSnapTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ifo");

            migrationBuilder.EnsureSchema(
                name: "ss");

            migrationBuilder.CreateTable(
                name: "DetailCustomer",
                schema: "ifo",
                columns: table => new
                {
                    DetailCustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayerID = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    PersonType = table.Column<string>(maxLength: 20, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailCustomer", x => x.DetailCustomerID);
                });

            migrationBuilder.CreateTable(
                name: "HeaderCustomer",
                schema: "ifo",
                columns: table => new
                {
                    HeaderCustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayerPersonId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    LoginIdentityCard = table.Column<string>(maxLength: 13, nullable: true),
                    LoginLastName = table.Column<string>(maxLength: 100, nullable: true),
                    LoginRefCode = table.Column<string>(maxLength: 255, nullable: true),
                    SMSFormat = table.Column<string>(maxLength: 255, nullable: true),
                    PrimaryPhone = table.Column<string>(maxLength: 40, nullable: true),
                    IsCustomerReply = table.Column<bool>(nullable: false),
                    IsSMSSended = table.Column<bool>(nullable: false),
                    IsAgentConfirm = table.Column<bool>(nullable: false),
                    ReplyDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ConfirmDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderCustomer", x => x.HeaderCustomerID);
                });

            migrationBuilder.CreateTable(
                name: "PayerSnapshot",
                schema: "ss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: true),
                    Customer_guid = table.Column<Guid>(nullable: false),
                    TitleId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    IdentityCard = table.Column<string>(maxLength: 13, nullable: true),
                    PrimaryPhone = table.Column<string>(maxLength: 40, nullable: true),
                    SecondaryPhone = table.Column<string>(maxLength: 40, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    LineID = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayerSnapshot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicySnapshot",
                schema: "ss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationCode = table.Column<string>(maxLength: 255, nullable: true),
                    ProductType = table.Column<string>(maxLength: 255, nullable: true),
                    Product = table.Column<string>(maxLength: 255, nullable: true),
                    Premium = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    CustPersonId = table.Column<int>(nullable: true),
                    Customer_guid = table.Column<Guid>(nullable: true),
                    CustName = table.Column<string>(maxLength: 255, nullable: true),
                    PayerPersonId = table.Column<int>(nullable: true),
                    Payer_guid = table.Column<Guid>(nullable: true),
                    PayerName = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    Payer_SnapshotId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicySnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicySnapshot_PayerSnapshot_Payer_SnapshotId",
                        column: x => x.Payer_SnapshotId,
                        principalSchema: "ss",
                        principalTable: "PayerSnapshot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSnapshot",
                schema: "ss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: true),
                    Customer_guid = table.Column<Guid>(nullable: false),
                    TitleId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    IdentityCard = table.Column<string>(maxLength: 13, nullable: true),
                    PrimaryPhone = table.Column<string>(maxLength: 40, nullable: true),
                    SecondaryPhone = table.Column<string>(maxLength: 40, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    LineID = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    Policy_SnapshotId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSnapshot_PolicySnapshot_Policy_SnapshotId",
                        column: x => x.Policy_SnapshotId,
                        principalSchema: "ss",
                        principalTable: "PolicySnapshot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSnapshot_Policy_SnapshotId",
                schema: "ss",
                table: "CustomerSnapshot",
                column: "Policy_SnapshotId",
                unique: true,
                filter: "[Policy_SnapshotId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PolicySnapshot_Payer_SnapshotId",
                schema: "ss",
                table: "PolicySnapshot",
                column: "Payer_SnapshotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailCustomer",
                schema: "ifo");

            migrationBuilder.DropTable(
                name: "HeaderCustomer",
                schema: "ifo");

            migrationBuilder.DropTable(
                name: "CustomerSnapshot",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "PolicySnapshot",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "PayerSnapshot",
                schema: "ss");
        }
    }
}
