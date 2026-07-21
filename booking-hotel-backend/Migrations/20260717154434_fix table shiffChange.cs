using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace booking_hotel_backend.Migrations
{
    /// <inheritdoc />
    public partial class fixtableshiffChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shift_change_requests_shifts_new_shift_id",
                table: "shift_change_requests");

            migrationBuilder.AlterColumn<long>(
                name: "new_shift_id",
                table: "shift_change_requests",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateOnly>(
                name: "new_work_date",
                table: "shift_change_requests",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "target_work_schedule_id",
                table: "shift_change_requests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_shift_change_requests_target_work_schedule_id",
                table: "shift_change_requests",
                column: "target_work_schedule_id");

            migrationBuilder.AddForeignKey(
                name: "FK_shift_change_requests_shifts_new_shift_id",
                table: "shift_change_requests",
                column: "new_shift_id",
                principalTable: "shifts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_shift_change_requests_work_schedules_target_work_schedule_id",
                table: "shift_change_requests",
                column: "target_work_schedule_id",
                principalTable: "work_schedules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shift_change_requests_shifts_new_shift_id",
                table: "shift_change_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_shift_change_requests_work_schedules_target_work_schedule_id",
                table: "shift_change_requests");

            migrationBuilder.DropIndex(
                name: "IX_shift_change_requests_target_work_schedule_id",
                table: "shift_change_requests");

            migrationBuilder.DropColumn(
                name: "new_work_date",
                table: "shift_change_requests");

            migrationBuilder.DropColumn(
                name: "target_work_schedule_id",
                table: "shift_change_requests");

            migrationBuilder.AlterColumn<long>(
                name: "new_shift_id",
                table: "shift_change_requests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_shift_change_requests_shifts_new_shift_id",
                table: "shift_change_requests",
                column: "new_shift_id",
                principalTable: "shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
