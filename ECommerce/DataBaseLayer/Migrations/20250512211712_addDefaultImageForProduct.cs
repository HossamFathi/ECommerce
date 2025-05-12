using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class addDefaultImageForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "VerifyTime",
                table: "Messages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 5, 13, 0, 17, 11, 912, DateTimeKind.Local).AddTicks(3855),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 5, 12, 22, 59, 52, 330, DateTimeKind.Local).AddTicks(6931));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VerifyTime",
                table: "Messages",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 5, 12, 22, 59, 52, 330, DateTimeKind.Local).AddTicks(6931),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 5, 13, 0, 17, 11, 912, DateTimeKind.Local).AddTicks(3855));
        }
    }
}
