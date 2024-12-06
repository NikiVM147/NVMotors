using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NVMotors.Data.Migrations
{
    /// <inheritdoc />
    public partial class addModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motors_MotorCategories_MotorCategoryId",
                table: "Motors");

            migrationBuilder.AlterColumn<Guid>(
                name: "MotorCategoryId",
                table: "Motors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Ads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Motors_MotorCategories_MotorCategoryId",
                table: "Motors",
                column: "MotorCategoryId",
                principalTable: "MotorCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motors_MotorCategories_MotorCategoryId",
                table: "Motors");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Ads");

            migrationBuilder.AlterColumn<Guid>(
                name: "MotorCategoryId",
                table: "Motors",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Motors_MotorCategories_MotorCategoryId",
                table: "Motors",
                column: "MotorCategoryId",
                principalTable: "MotorCategories",
                principalColumn: "Id");
        }
    }
}
