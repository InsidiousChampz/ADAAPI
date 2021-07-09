using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INFOEDITORAPI.Migrations
{
    public partial class AuditandOrderCreate : Migration
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
                    Status = table.Column<bool>(nullable: false),
                    OrderNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAuditTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAuditTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLists_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProductAudits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    StockCount = table.Column<int>(nullable: false),
                    AuditAmount = table.Column<int>(nullable: false),
                    AuditTotalAmount = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ProductAuditTypeId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    ProductGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAudits_ProductAuditTypes_ProductAuditTypeId",
                        column: x => x.ProductAuditTypeId,
                        principalTable: "ProductAuditTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductAudits_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductAudits_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("bce94f81-e3cd-4436-99ed-07773c2ced55"), "user" },
                    { new Guid("6ee8e6b1-7deb-402d-af82-67eef9d194ea"), "Manager" },
                    { new Guid("ddcb0fd6-e367-4d12-9f26-c89ced0bb1fe"), "Admin" },
                    { new Guid("bd9077ce-c0a9-4522-a44a-179abafc7085"), "Developer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLists_OrderId",
                table: "OrderLists",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAudits_ProductAuditTypeId",
                table: "ProductAudits",
                column: "ProductAuditTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAudits_ProductGroupId",
                table: "ProductAudits",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAudits_ProductId",
                table: "ProductAudits",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLists");

            migrationBuilder.DropTable(
                name: "ProductAudits");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductAuditTypes");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("6ee8e6b1-7deb-402d-af82-67eef9d194ea"));

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("bce94f81-e3cd-4436-99ed-07773c2ced55"));

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("bd9077ce-c0a9-4522-a44a-179abafc7085"));

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("ddcb0fd6-e367-4d12-9f26-c89ced0bb1fe"));

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
