using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace booking_hotel_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "rooms",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            

            migrationBuilder.CreateTable(
                name: "salaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hotel_staff_id = table.Column<long>(type: "bigint", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Allowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_salaries_hotel_staffs_hotel_staff_id",
                        column: x => x.hotel_staff_id,
                        principalTable: "hotel_staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hotel_id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    BreakMinutes = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shifts_hotels_hotel_id",
                        column: x => x.hotel_id,
                        principalTable: "hotels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "attendances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hotel_staff_id = table.Column<long>(type: "bigint", nullable: false),
                    ShiftId = table.Column<long>(type: "bigint", nullable: false),
                    WorkDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckInTime = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    CheckOutTime = table.Column<TimeOnly>(type: "time(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attendances_hotel_staffs_hotel_staff_id",
                        column: x => x.hotel_staff_id,
                        principalTable: "hotel_staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_attendances_shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "work_schedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hotel_staff_id = table.Column<long>(type: "bigint", nullable: false),
                    shift_id = table.Column<long>(type: "bigint", nullable: false),
                    WorkDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsDayOff = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_work_schedules_hotel_staffs_hotel_staff_id",
                        column: x => x.hotel_staff_id,
                        principalTable: "hotel_staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_work_schedules_shifts_shift_id",
                        column: x => x.shift_id,
                        principalTable: "shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_attendances_hotel_staff_id",
                table: "attendances",
                column: "hotel_staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_attendances_ShiftId",
                table: "attendances",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_requests_hotel_staff_id",
                table: "leave_requests",
                column: "hotel_staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_salaries_hotel_staff_id",
                table: "salaries",
                column: "hotel_staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_shifts_hotel_id",
                table: "shifts",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_schedules_hotel_staff_id",
                table: "work_schedules",
                column: "hotel_staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_schedules_shift_id",
                table: "work_schedules",
                column: "shift_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendances");

            migrationBuilder.DropTable(
                name: "leave_requests");

            migrationBuilder.DropTable(
                name: "salaries");

            migrationBuilder.DropTable(
                name: "work_schedules");

            migrationBuilder.DropTable(
                name: "shifts");

            migrationBuilder.DropColumn(
                name: "image",
                table: "rooms");
        }
    }
}
