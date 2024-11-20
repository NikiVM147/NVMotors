using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NVMotors.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixAdModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Motors_VechicleId",
                table: "Ads");

            migrationBuilder.RenameColumn(
                name: "VechicleId",
                table: "Ads",
                newName: "MotorId");

            migrationBuilder.RenameIndex(
                name: "IX_Ads_VechicleId",
                table: "Ads",
                newName: "IX_Ads_MotorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Motors_MotorId",
                table: "Ads",
                column: "MotorId",
                principalTable: "Motors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Motors_MotorId",
                table: "Ads");

            migrationBuilder.RenameColumn(
                name: "MotorId",
                table: "Ads",
                newName: "VechicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Ads_MotorId",
                table: "Ads",
                newName: "IX_Ads_VechicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Motors_VechicleId",
                table: "Ads",
                column: "VechicleId",
                principalTable: "Motors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
