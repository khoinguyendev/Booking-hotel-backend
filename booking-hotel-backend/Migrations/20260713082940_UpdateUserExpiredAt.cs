using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace booking_hotel_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserExpiredAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredAt",
                table: "users",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "ExpiredAt",
                keyValue: null,
                column: "ExpiredAt",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ExpiredAt",
                table: "users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
