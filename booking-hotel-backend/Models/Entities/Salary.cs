using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("salaries")]
public class Salary
{
    [Key]
    public long Id { get; set; }

    [Required]
    [Column("hotel_staff_id")]
    public long HotelStaffId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal BasicSalary { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Allowance { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Bonus { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Deduction { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSalary { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public bool IsPaid { get; set; }

    public DateTime? PaidAt { get; set; }

    [ForeignKey(nameof(HotelStaffId))]
    public HotelStaff HotelStaff { get; set; } = null!;
}