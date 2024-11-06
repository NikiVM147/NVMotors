using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NVMotors.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdImage_Ads_AdId",
                table: "AdImage");

            migrationBuilder.DropForeignKey(
                name: "FK_AdImage_Image_ImageId",
                table: "AdImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Vechicles_VechicleId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Vechicles_AspNetUsers_SellerId",
                table: "Vechicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vechicles_MotorCategory_MotorCategoryId",
                table: "Vechicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vechicles_Specifications_SpecificationId",
                table: "Vechicles");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vechicles",
                table: "Vechicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotorCategory",
                table: "MotorCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdImage",
                table: "AdImage");

            migrationBuilder.RenameTable(
                name: "Vechicles",
                newName: "Motors");

            migrationBuilder.RenameTable(
                name: "MotorCategory",
                newName: "MotorCategories");

            migrationBuilder.RenameTable(
                name: "AdImage",
                newName: "AdsImages");

            migrationBuilder.RenameIndex(
                name: "IX_Vechicles_SpecificationId",
                table: "Motors",
                newName: "IX_Motors_SpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Vechicles_SellerId",
                table: "Motors",
                newName: "IX_Motors_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Vechicles_MotorCategoryId",
                table: "Motors",
                newName: "IX_Motors_MotorCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AdImage_AdId",
                table: "AdsImages",
                newName: "IX_AdsImages_AdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motors",
                table: "Motors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotorCategories",
                table: "MotorCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdsImages",
                table: "AdsImages",
                columns: new[] { "ImageId", "AdId" });

            migrationBuilder.CreateTable(
                name: "MotorImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorImages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Motors_VechicleId",
                table: "Ads",
                column: "VechicleId",
                principalTable: "Motors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdsImages_Ads_AdId",
                table: "AdsImages",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdsImages_MotorImages_ImageId",
                table: "AdsImages",
                column: "ImageId",
                principalTable: "MotorImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motors_AspNetUsers_SellerId",
                table: "Motors",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motors_MotorCategories_MotorCategoryId",
                table: "Motors",
                column: "MotorCategoryId",
                principalTable: "MotorCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Motors_Specifications_SpecificationId",
                table: "Motors",
                column: "SpecificationId",
                principalTable: "Specifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Motors_VechicleId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_AdsImages_Ads_AdId",
                table: "AdsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_AdsImages_MotorImages_ImageId",
                table: "AdsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Motors_AspNetUsers_SellerId",
                table: "Motors");

            migrationBuilder.DropForeignKey(
                name: "FK_Motors_MotorCategories_MotorCategoryId",
                table: "Motors");

            migrationBuilder.DropForeignKey(
                name: "FK_Motors_Specifications_SpecificationId",
                table: "Motors");

            migrationBuilder.DropTable(
                name: "MotorImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Motors",
                table: "Motors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotorCategories",
                table: "MotorCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdsImages",
                table: "AdsImages");

            migrationBuilder.RenameTable(
                name: "Motors",
                newName: "Vechicles");

            migrationBuilder.RenameTable(
                name: "MotorCategories",
                newName: "MotorCategory");

            migrationBuilder.RenameTable(
                name: "AdsImages",
                newName: "AdImage");

            migrationBuilder.RenameIndex(
                name: "IX_Motors_SpecificationId",
                table: "Vechicles",
                newName: "IX_Vechicles_SpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Motors_SellerId",
                table: "Vechicles",
                newName: "IX_Vechicles_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Motors_MotorCategoryId",
                table: "Vechicles",
                newName: "IX_Vechicles_MotorCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AdsImages_AdId",
                table: "AdImage",
                newName: "IX_AdImage_AdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vechicles",
                table: "Vechicles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotorCategory",
                table: "MotorCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdImage",
                table: "AdImage",
                columns: new[] { "ImageId", "AdId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_AdImage_Ads_AdId",
                table: "AdImage",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdImage_Image_ImageId",
                table: "AdImage",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Vechicles_VechicleId",
                table: "Ads",
                column: "VechicleId",
                principalTable: "Vechicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Vechicles_Specifications_SpecificationId",
                table: "Vechicles",
                column: "SpecificationId",
                principalTable: "Specifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
