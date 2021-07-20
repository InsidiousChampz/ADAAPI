using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerProFileAPI.Migrations
{
    public partial class AddSnapTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ss");

            migrationBuilder.CreateTable(
                name: "SnapCustomer",
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
                    Birthdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdentityCard = table.Column<string>(maxLength: 13, nullable: true),
                    PrimaryPhone = table.Column<string>(maxLength: 40, nullable: true),
                    SecondaryPhone = table.Column<string>(maxLength: 40, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    LineID = table.Column<string>(maxLength: 255, nullable: true),
                    WorkAddressId = table.Column<int>(nullable: true),
                    WorkAddressName = table.Column<string>(maxLength: 255, nullable: true),
                    WorkAddress1 = table.Column<string>(maxLength: 255, nullable: true),
                    WorkAddress2 = table.Column<string>(maxLength: 255, nullable: true),
                    WorkAddressSubDistrictCode = table.Column<string>(maxLength: 20, nullable: true),
                    WorkAddressSubDistrict = table.Column<string>(maxLength: 255, nullable: true),
                    WorkAddressDistrict = table.Column<string>(maxLength: 255, nullable: true),
                    WorkAddressProvince = table.Column<string>(maxLength: 255, nullable: true),
                    WorkAddressZipCode = table.Column<string>(maxLength: 5, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnapCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnapPolicy",
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
                    Cust_guid = table.Column<Guid>(nullable: true),
                    CustName = table.Column<string>(maxLength: 255, nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    Payer_guid = table.Column<Guid>(nullable: true),
                    PayerName = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnapPolicy", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnapCustomer",
                schema: "ss");

            migrationBuilder.DropTable(
                name: "SnapPolicy",
                schema: "ss");
        }
    }
}
