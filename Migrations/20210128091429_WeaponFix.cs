using Microsoft.EntityFrameworkCore.Migrations;

namespace TEMPLETEAPI.Migrations
{
    public partial class WeaponFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Weapon_WeaponId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_WeaponId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "Character");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Weapon",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Damage",
                table: "Weapon",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Weapon_CharacterId",
                table: "Weapon",
                column: "CharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapon_Character_CharacterId",
                table: "Weapon",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weapon_Character_CharacterId",
                table: "Weapon");

            migrationBuilder.DropIndex(
                name: "IX_Weapon_CharacterId",
                table: "Weapon");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Weapon");

            migrationBuilder.DropColumn(
                name: "Damage",
                table: "Weapon");

            migrationBuilder.AddColumn<int>(
                name: "WeaponId",
                table: "Character",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_WeaponId",
                table: "Character",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Weapon_WeaponId",
                table: "Character",
                column: "WeaponId",
                principalTable: "Weapon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
