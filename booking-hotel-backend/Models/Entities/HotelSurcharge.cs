using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("hotel_surcharges")]
public class HotelSurcharge
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("hotel_id")]
    public long HotelId { get; set; }

    [Required]
    [Column("surcharge_type_id")]
    public long SurchargeTypeId { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;


    // Khoảng thời gian áp dụng phụ thu
    [Column("start_time")]
    public TimeOnly? StartTime { get; set; }

    [Column("end_time")]
    public TimeOnly? EndTime { get; set; }


    // FIXED hoặc PERCENT
    [Required]
    [Column("amount_type")]
    public AmountType AmountType { get; set; } = AmountType.Fixed;


    // Giá trị phụ thu
    // FIXED: 500000 VNĐ
    // PERCENT: 10 (%)
    [Required]
    [Column("amount", TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }


    // Có cần khách sạn duyệt trước không
    [Column("is_request")]
    public bool IsRequest { get; set; } = false;


    [Required]
    [Column("status")]
    public SurchargeStatus Status { get; set; } = SurchargeStatus.Active;


    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    // Navigation Properties

    [ForeignKey(nameof(HotelId))]
    public virtual Hotel Hotel { get; set; } = null!;


    [ForeignKey(nameof(SurchargeTypeId))]
    public virtual SurchargeType SurchargeType { get; set; } = null!;
    public virtual ICollection<BookingSurcharge> BookingSurcharges { get; set; } = [];
}