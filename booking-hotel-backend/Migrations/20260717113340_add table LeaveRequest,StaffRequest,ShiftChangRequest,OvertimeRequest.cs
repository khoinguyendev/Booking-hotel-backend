using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace booking_hotel_backend.Migrations
{
    /// <inheritdoc />
    public partial class addtableLeaveRequestStaffRequestShiftChangRequestOvertimeRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leave_requests_hotel_staffs_hotel_staff_id",
                table: "leave_requests");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "leave_requests");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "leave_requests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "leave_requests");

            migrationBuilder.RenameColumn(
                name: "hotel_staff_id",
                table: "leave_requests",
                newName: "HotelStaffId");

            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "leave_requests",
                newName: "to_date");

            migrationBuilder.RenameColumn(
                name: "FromDate",
                table: "leave_requests",
                newName: "from_date");

            migrationBuilder.RenameIndex(
                name: "IX_leave_requests_hotel_staff_id",
                table: "leave_requests",
                newName: "IX_leave_requests_HotelStaffId");

            migrationBuilder.AlterColumn<long>(
                name: "HotelStaffId",
                table: "leave_requests",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "staff_request_id",
                table: "leave_requests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "staff_requests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hotel_staff_id = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    approved_by = table.Column<long>(type: "bigint", nullable: true),
                    approved_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    reject_reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_staff_requests_hotel_staffs_hotel_staff_id",
                        column: x => x.hotel_staff_id,
                        principalTable: "hotel_staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "overtime_requests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    staff_request_id = table.Column<long>(type: "bigint", nullable: false),
                    work_date = table.Column<DateOnly>(type: "date", nullable: false),
                    from_time = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    to_time = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    hours = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_overtime_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_overtime_requests_staff_requests_staff_request_id",
                        column: x => x.staff_request_id,
                        principalTable: "staff_requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shift_change_requests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    staff_request_id = table.Column<long>(type: "bigint", nullable: false),
                    work_schedule_id = table.Column<long>(type: "bigint", nullable: false),
                    new_shift_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shift_change_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shift_change_requests_shifts_new_shift_id",
                        column: x => x.new_shift_id,
                        principalTable: "shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shift_change_requests_staff_requests_staff_request_id",
                        column: x => x.staff_request_id,
                        principalTable: "staff_requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shift_change_requests_work_schedules_work_schedule_id",
                        column: x => x.work_schedule_id,
                        principalTable: "work_schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_leave_requests_staff_request_id",
                table: "leave_requests",
                column: "staff_request_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_overtime_requests_staff_request_id",
                table: "overtime_requests",
                column: "staff_request_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shift_change_requests_new_shift_id",
                table: "shift_change_requests",
                column: "new_shift_id");

            migrationBuilder.CreateIndex(
                name: "IX_shift_change_requests_staff_request_id",
                table: "shift_change_requests",
                column: "staff_request_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shift_change_requests_work_schedule_id",
                table: "shift_change_requests",
                column: "work_schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_requests_hotel_staff_id",
                table: "staff_requests",
                column: "hotel_staff_id");

            migrationBuilder.AddForeignKey(
                name: "FK_leave_requests_hotel_staffs_HotelStaffId",
                table: "leave_requests",
                column: "HotelStaffId",
                principalTable: "hotel_staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_leave_requests_staff_requests_staff_request_id",
                table: "leave_requests",
                column: "staff_request_id",
                principalTable: "staff_requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leave_requests_hotel_staffs_HotelStaffId",
                table: "leave_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_leave_requests_staff_requests_staff_request_id",
                table: "leave_requests");

            migrationBuilder.DropTable(
                name: "overtime_requests");

            migrationBuilder.DropTable(
                name: "shift_change_requests");

            migrationBuilder.DropTable(
                name: "staff_requests");

            migrationBuilder.DropIndex(
                name: "IX_leave_requests_staff_request_id",
                table: "leave_requests");

            migrationBuilder.DropColumn(
                name: "staff_request_id",
                table: "leave_requests");

            migrationBuilder.RenameColumn(
                name: "to_date",
                table: "leave_requests",
                newName: "ToDate");

            migrationBuilder.RenameColumn(
                name: "from_date",
                table: "leave_requests",
                newName: "FromDate");

            migrationBuilder.RenameColumn(
                name: "HotelStaffId",
                table: "leave_requests",
                newName: "hotel_staff_id");

            migrationBuilder.RenameIndex(
                name: "IX_leave_requests_HotelStaffId",
                table: "leave_requests",
                newName: "IX_leave_requests_hotel_staff_id");

            migrationBuilder.AlterColumn<long>(
                name: "hotel_staff_id",
                table: "leave_requests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "leave_requests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "leave_requests",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "leave_requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_leave_requests_hotel_staffs_hotel_staff_id",
                table: "leave_requests",
                column: "hotel_staff_id",
                principalTable: "hotel_staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
