using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NVMotors.Data.Migrations
{
    /// <inheritdoc />
    public partial class queryChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Queries");

            migrationBuilder.AddColumn<Guid>(
                name: "RequesterId",
                table: "Queries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Queries_RequesterId",
                table: "Queries",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Queries_AspNetUsers_RequesterId",
                table: "Queries",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Queries_AspNetUsers_RequesterId",
                table: "Queries");

            migrationBuilder.DropIndex(
                name: "IX_Queries_RequesterId",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                table: "Queries");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Queries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
