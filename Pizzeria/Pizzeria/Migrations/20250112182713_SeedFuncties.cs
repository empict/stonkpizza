using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class SeedFuncties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Medewerkers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "FunctieId1",
                table: "Medewerkers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wachtwoord",
                table: "Medewerkers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Functies",
                columns: new[] { "Id", "Naam" },
                values: new object[,]
                {
                    { 1, "Manager" },
                    { 2, "Bakker" },
                    { 3, "Bezorger" }
                });

            migrationBuilder.InsertData(
                table: "Medewerkers",
                columns: new[] { "Id", "Email", "FunctieId", "FunctieId1", "Naam", "Wachtwoord" },
                values: new object[,]
                {
                    { 1, "trump123@gmail.com", 1, null, "Trump123", "EBc1oatbEj8XsCqLny1mLO46DW0IFgeB3fE0atZ2QjM=" },
                    { 2, "bakker123@gmail.com", 2, null, "Bakker123", "SJFbA3wyTJQwuGoUudRvOPsle5IGSdTtUsGFvQ/pxV8=" },
                    { 3, "bezorger123@gmail.com", 3, null, "Bezorger123", "Dqhkdz4yNIfSocJchTc46F4jrcYHyZz4iPpcZknsGGg=" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medewerkers_FunctieId1",
                table: "Medewerkers",
                column: "FunctieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerkers_Functies_FunctieId1",
                table: "Medewerkers",
                column: "FunctieId1",
                principalTable: "Functies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medewerkers_Functies_FunctieId1",
                table: "Medewerkers");

            migrationBuilder.DropIndex(
                name: "IX_Medewerkers_FunctieId1",
                table: "Medewerkers");

            migrationBuilder.DeleteData(
                table: "Medewerkers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medewerkers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medewerkers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Functies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Functies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Functies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "FunctieId1",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "Wachtwoord",
                table: "Medewerkers");
        }
    }
}
