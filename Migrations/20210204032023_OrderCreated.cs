using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace STANDARDAPI.Migrations
{
    public partial class OrderCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("3003d234-9100-4d91-bcfa-79d77837e5fc"));

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("4ce6e34c-82b3-4d22-9613-2689625874b0"));

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("6fbf5422-07ad-410b-a548-09427cc9dd53"));

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("cc45320e-5e09-462f-83bc-c50906ef5713"));

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOrder = table.Column<DateTime>(nullable: false),
                    ItemCount = table.Column<int>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    Discount = table.Column<int>(nullable: false),
                    Net = table.Column<int>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLists_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("b5b4cbed-435e-416e-9b09-8d2faf938563"), "user" },
                    { new Guid("911238c3-7bf3-4562-99c9-6f33f127c265"), "Manager" },
                    { new Guid("fb0d5917-dde0-455a-85f8-f29fc4f17ae8"), "Admin" },
                    { new Guid("b164ec54-7209-4c4f-a09d-781863c22cc9"), "Developer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLists_OrderId",
                table: "OrderLists",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLists");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("911238c3-7bf3-4562-99c9-6f33f127c265"));

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b164ec54-7209-4c4f-a09d-781863c22cc9"));

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b5b4cbed-435e-416e-9b09-8d2faf938563"));

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("fb0d5917-dde0-455a-85f8-f29fc4f17ae8"));

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("cc45320e-5e09-462f-83bc-c50906ef5713"), "user" },
                    { new Guid("4ce6e34c-82b3-4d22-9613-2689625874b0"), "Manager" },
                    { new Guid("6fbf5422-07ad-410b-a548-09427cc9dd53"), "Admin" },
                    { new Guid("3003d234-9100-4d91-bcfa-79d77837e5fc"), "Developer" }
                });
        }
    }
}
