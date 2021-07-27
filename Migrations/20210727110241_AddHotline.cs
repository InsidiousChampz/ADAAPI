using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmsUpdateCustomer_Api.Migrations
{
    public partial class AddHotline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerHotline",
                schema: "scp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: true),
                    TitleId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    PrimaryPhone = table.Column<string>(maxLength: 40, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerHotline", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerHotline",
                schema: "scp");
        }
    }
}
