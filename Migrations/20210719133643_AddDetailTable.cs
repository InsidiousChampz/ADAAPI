using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerProFileAPI.Migrations
{
    public partial class AddDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailCustomer",
                schema: "ifo");
        }
    }
}
