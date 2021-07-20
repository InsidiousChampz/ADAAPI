using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerProFileAPI.Migrations
{
    public partial class AddHeaderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ifo");

            migrationBuilder.CreateTable(
                name: "HeaderCustomer",
                schema: "ifo",
                columns: table => new
                {
                    HeaderCustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayerPersonId = table.Column<int>(nullable: true),
                    LoginIdentityCard = table.Column<string>(maxLength: 13, nullable: true),
                    LoginLastName = table.Column<string>(maxLength: 100, nullable: true),
                    LoginRefCode = table.Column<string>(maxLength: 255, nullable: true),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeaderCustomer",
                schema: "ifo");
        }
    }
}
