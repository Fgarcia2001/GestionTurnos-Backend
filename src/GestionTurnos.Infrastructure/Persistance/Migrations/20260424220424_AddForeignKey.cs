using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTurnos.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessSubscriptions_Plans_PlanId",
                table: "BusinessSubscriptions");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "BusinessSubscriptions",
                newName: "PlanID");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessSubscriptions_PlanId",
                table: "BusinessSubscriptions",
                newName: "IX_BusinessSubscriptions_PlanID");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessSubscriptions_Plans_PlanID",
                table: "BusinessSubscriptions",
                column: "PlanID",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessSubscriptions_Plans_PlanID",
                table: "BusinessSubscriptions");

            migrationBuilder.RenameColumn(
                name: "PlanID",
                table: "BusinessSubscriptions",
                newName: "PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessSubscriptions_PlanID",
                table: "BusinessSubscriptions",
                newName: "IX_BusinessSubscriptions_PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessSubscriptions_Plans_PlanId",
                table: "BusinessSubscriptions",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
