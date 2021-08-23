using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmsUpdateCustomer_Api.Migrations
{
    public partial class AddRankTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartCoverDate",
                schema: "ss",
                table: "PolicySnapshot",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CancelDate",
                schema: "ss",
                table: "PolicySnapshot",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "RankPolicyCustomer",
                schema: "ifo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeDetail = table.Column<string>(maxLength: 100, nullable: true),
                    ProductTypeRank = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankPolicyCustomer", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankPolicyCustomer",
                schema: "ifo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartCoverDate",
                schema: "ss",
                table: "PolicySnapshot",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CancelDate",
                schema: "ss",
                table: "PolicySnapshot",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
