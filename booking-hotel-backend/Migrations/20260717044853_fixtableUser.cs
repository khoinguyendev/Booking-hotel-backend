using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace booking_hotel_backend.Migrations
{
    /// <inheritdoc />
    public partial class fixtableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_CodeId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "CodeId",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "employee_code",
                table: "hotel_staffs",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employee_code",
                table: "hotel_staffs");

            migrationBuilder.AddColumn<string>(
                name: "CodeId",
                table: "users",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_users_CodeId",
                table: "users",
                column: "CodeId",
                unique: true);
        }
    }
}
