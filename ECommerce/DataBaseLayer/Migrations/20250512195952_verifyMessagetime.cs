using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class verifyMessagetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "VerifyTime",
                table: "Messages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 5, 12, 22, 59, 52, 330, DateTimeKind.Local).AddTicks(6931));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifyTime",
                table: "Messages");
        }
    }
}
