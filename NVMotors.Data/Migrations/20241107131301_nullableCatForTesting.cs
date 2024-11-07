using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NVMotors.Data.Migrations
{
    /// <inheritdoc />
    public partial class nullableCatForTesting : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Motors_MotorCategories_MotorCategoryId",
                table: "Motors",
                column: "MotorCategoryId",
                principalTable: "MotorCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
