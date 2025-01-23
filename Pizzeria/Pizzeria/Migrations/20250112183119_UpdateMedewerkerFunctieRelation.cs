using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedewerkerFunctieRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medewerkers_Functies_FunctieId1",
                table: "Medewerkers");

            migrationBuilder.DropIndex(
                name: "IX_Medewerkers_FunctieId1",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "FunctieId1",
                table: "Medewerkers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FunctieId1",
                table: "Medewerkers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Medewerkers",
                keyColumn: "Id",
                keyValue: 1,
                column: "FunctieId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Medewerkers",
                keyColumn: "Id",
                keyValue: 2,
                column: "FunctieId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Medewerkers",
                keyColumn: "Id",
                keyValue: 3,
                column: "FunctieId1",
                value: null);

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
    }
}
