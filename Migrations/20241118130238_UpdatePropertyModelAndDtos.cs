using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyModelAndDtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Properties_PropertyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PropertyId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "RealEstateCompanyName",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealEstateCompanyName",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Properties");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PropertyId",
                table: "Users",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Properties_PropertyId",
                table: "Users",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
