using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmsUpdateCustomer_Api.Migrations
{
    public partial class AllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sap");

            migrationBuilder.EnsureSchema(
                name: "ifo");

            migrationBuilder.EnsureSchema(
                name: "scp");

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
                name: "FollowupCustomer",
                schema: "ifo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    PayerFirstName = table.Column<string>(maxLength: 100, nullable: true),
                    PayerLastName = table.Column<string>(maxLength: 100, nullable: true),
                    PrimaryPhone = table.Column<string>(maxLength: 13, nullable: true),
                    OrganizeName = table.Column<string>(maxLength: 255, nullable: true),
                    District = table.Column<string>(maxLength: 255, nullable: true),
                    Province = table.Column<string>(maxLength: 255, nullable: true),
                    Area = table.Column<string>(maxLength: 255, nullable: true),
                    Branch = table.Column<string>(maxLength: 255, nullable: true),
                    AgentId = table.Column<int>(nullable: false),
                    AgentName = table.Column<string>(maxLength: 100, nullable: true),
                    AppID = table.Column<string>(maxLength: 50, nullable: true),
                    Result = table.Column<bool>(nullable: false),
                    AdminConfirm = table.Column<bool>(nullable: false),
                    CustomerConfirm = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowupCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeaderCustomer",
                schema: "ifo",
                columns: table => new
                {
                    HeaderCustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayerPersonId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    IdentityCard = table.Column<string>(maxLength: 13, nullable: true),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    PrimaryPhone = table.Column<string>(maxLength: 40, nullable: true),
                    LoginIdentityCard = table.Column<string>(maxLength: 13, nullable: true),
                    LoginLastName = table.Column<string>(maxLength: 100, nullable: true),
                    LoginRefCode = table.Column<string>(maxLength: 255, nullable: true),
                    SMSFormat = table.Column<string>(maxLength: 255, nullable: true),
                    SMSResult = table.Column<string>(maxLength: 255, nullable: true),
                    SMSCause = table.Column<string>(maxLength: 255, nullable: true),
                    IsAgentConfirm = table.Column<bool>(nullable: false),
                    IsCustomerReply = table.Column<bool>(nullable: false),
                    IsSMSSended = table.Column<bool>(nullable: false),
                    ConfirmDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReplyDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: true),
                    NumberofSended = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderCustomer", x => x.HeaderCustomerID);
                });

            migrationBuilder.CreateTable(
                name: "AdminApprove",
                schema: "sap",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    Customer_guid = table.Column<Guid>(nullable: false),
                    TitleId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    IdentityCard = table.Column<string>(maxLength: 13, nullable: false),
                    PrimaryPhone = table.Column<string>(maxLength: 40, nullable: false),
                    SecondaryPhone = table.Column<string>(maxLength: 40, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    LineID = table.Column<string>(maxLength: 255, nullable: true),
                    ImagePath = table.Column<string>(maxLength: 255, nullable: true),
                    ImageReferenceId = table.Column<string>(maxLength: 255, nullable: true),
                    DocumentId = table.Column<string>(maxLength: 255, nullable: true),
                    EditorId = table.Column<int>(maxLength: 255, nullable: false),
                    IsUpdated = table.Column<bool>(nullable: false),
                    IsCheckMerge = table.Column<bool>(nullable: false),
                    ListMergeFrom = table.Column<string>(maxLength: 255, nullable: true),
                    ListMergeTo = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminApprove", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminApproveTransaction",
                schema: "sap",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(maxLength: 255, nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    BeforeChange = table.Column<string>(maxLength: 1000, nullable: true),
                    AfterChange = table.Column<string>(maxLength: 1000, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminApproveTransaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerHotline",
                schema: "scp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    PrimaryPhone = table.Column<string>(maxLength: 40, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 255, nullable: true),
                    TypeHotLine = table.Column<int>(nullable: false),
                    InformDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerHotline", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerHotlineDetail",
                schema: "scp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotlineId = table.Column<int>(nullable: false),
                    HotlineDescriptions = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerHotlineDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProfile",
                schema: "scp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    Customer_guid = table.Column<Guid>(nullable: false),
                    TitleId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    IdentityCard = table.Column<string>(maxLength: 13, nullable: false),
                    PrimaryPhone = table.Column<string>(maxLength: 40, nullable: false),
                    SecondaryPhone = table.Column<string>(maxLength: 40, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    LineID = table.Column<string>(maxLength: 255, nullable: true),
                    ImagePath = table.Column<string>(maxLength: 255, nullable: true),
                    ImageReferenceId = table.Column<string>(maxLength: 255, nullable: true),
                    DocumentId = table.Column<string>(maxLength: 255, nullable: true),
                    EditorId = table.Column<int>(nullable: false),
                    IsUpdated = table.Column<bool>(nullable: false),
                    IsConfirm = table.Column<bool>(nullable: false),
                    ListMergeFrom = table.Column<string>(maxLength: 255, nullable: true),
                    ListMergeTo = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    ConfirmDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTransaction",
                schema: "scp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    EditorId = table.Column<int>(nullable: false),
                    FieldData = table.Column<string>(maxLength: 255, nullable: true),
                    BeforeChange = table.Column<string>(maxLength: 255, nullable: true),
                    AfterChange = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTransaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayerSnapshot",
                schema: "ss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
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
                    ProductTypeDetail = table.Column<string>(maxLength: 255, nullable: true),
                    Product = table.Column<string>(maxLength: 255, nullable: true),
                    Premium = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    CustPersonId = table.Column<int>(nullable: true),
                    Customer_guid = table.Column<Guid>(nullable: true),
                    CustName = table.Column<string>(maxLength: 255, nullable: true),
                    PayerPersonId = table.Column<int>(nullable: true),
                    Payer_guid = table.Column<Guid>(nullable: true),
                    PayerName = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    CustBuildingName = table.Column<string>(maxLength: 255, nullable: true),
                    CustSubDistrict = table.Column<string>(maxLength: 255, nullable: true),
                    CustDistrict = table.Column<string>(maxLength: 255, nullable: true),
                    CustProvince = table.Column<string>(maxLength: 255, nullable: true),
                    PayerBuildingName = table.Column<string>(maxLength: 255, nullable: true),
                    PayerSubDistrict = table.Column<string>(maxLength: 255, nullable: true),
                    PayerDistrict = table.Column<string>(maxLength: 255, nullable: true),
                    PayerProvince = table.Column<string>(maxLength: 255, nullable: true),
                    PayerBranch = table.Column<string>(maxLength: 255, nullable: true),
                    PayerBranchId = table.Column<int>(maxLength: 255, nullable: true),
                    PayerStudyArea = table.Column<string>(maxLength: 255, nullable: true),
                    PayerStudyAreaId = table.Column<int>(maxLength: 255, nullable: true),
                    SaleName = table.Column<string>(maxLength: 255, nullable: true),
                    SaleCode = table.Column<string>(maxLength: 255, nullable: true),
                    StartCoverDate = table.Column<DateTime>(type: "date", nullable: false),
                    AppStatus = table.Column<string>(maxLength: 255, nullable: true),
                    CancelDate = table.Column<DateTime>(type: "date", nullable: false),
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
                name: "FollowupCustomer",
                schema: "ifo");

            migrationBuilder.DropTable(
                name: "HeaderCustomer",
                schema: "ifo");

            migrationBuilder.DropTable(
                name: "AdminApprove",
                schema: "sap");

            migrationBuilder.DropTable(
                name: "AdminApproveTransaction",
                schema: "sap");

            migrationBuilder.DropTable(
                name: "CustomerHotline",
                schema: "scp");

            migrationBuilder.DropTable(
                name: "CustomerHotlineDetail",
                schema: "scp");

            migrationBuilder.DropTable(
                name: "CustomerProfile",
                schema: "scp");

            migrationBuilder.DropTable(
                name: "CustomerTransaction",
                schema: "scp");

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
