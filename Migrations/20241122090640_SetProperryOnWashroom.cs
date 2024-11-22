using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SetProperryOnWashroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "Washrooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Washrooms_PropertyId",
                table: "Washrooms",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Washrooms_Properties_PropertyId",
                table: "Washrooms",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Washrooms_Properties_PropertyId",
                table: "Washrooms");

            migrationBuilder.DropIndex(
                name: "IX_Washrooms_PropertyId",
                table: "Washrooms");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Washrooms");
        }
    }
}
