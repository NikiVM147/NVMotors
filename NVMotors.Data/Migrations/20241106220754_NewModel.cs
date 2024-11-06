using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NVMotors.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MotorCategoryId",
                table: "Vechicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "Vechicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "TransmissionType",
                table: "Specifications",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FuelType",
                table: "Specifications",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Condition",
                table: "Specifications",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Specifications",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EngineDisplacement",
                table: "Specifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdImage",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdImage", x => new { x.ImageId, x.AdId });
                    table.ForeignKey(
                        name: "FK_AdImage_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdImage_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vechicles_MotorCategoryId",
                table: "Vechicles",
                column: "MotorCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vechicles_SellerId",
                table: "Vechicles",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_AdImage_AdId",
                table: "AdImage",
                column: "AdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vechicles_AspNetUsers_SellerId",
                table: "Vechicles",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vechicles_MotorCategory_MotorCategoryId",
                table: "Vechicles",
                column: "MotorCategoryId",
                principalTable: "MotorCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vechicles_AspNetUsers_SellerId",
                table: "Vechicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vechicles_MotorCategory_MotorCategoryId",
                table: "Vechicles");

            migrationBuilder.DropTable(
                name: "AdImage");

            migrationBuilder.DropTable(
                name: "MotorCategory");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Vechicles_MotorCategoryId",
                table: "Vechicles");

            migrationBuilder.DropIndex(
                name: "IX_Vechicles_SellerId",
                table: "Vechicles");

            migrationBuilder.DropColumn(
                name: "MotorCategoryId",
                table: "Vechicles");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Vechicles");

            migrationBuilder.DropColumn(
                name: "EngineDisplacement",
                table: "Specifications");

            migrationBuilder.AlterColumn<string>(
                name: "TransmissionType",
                table: "Specifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "FuelType",
                table: "Specifications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "Condition",
                table: "Specifications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<int>(
                name: "Color",
                table: "Specifications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
