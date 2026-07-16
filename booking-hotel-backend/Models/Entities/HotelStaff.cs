using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities
{
    [Table("hotel_staffs")]
    public class HotelStaff
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Column("hotel_id")]
        public long HotelId { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("position_id")]
        public long PositionId { get; set; }

        public bool Status { get; set; } = true;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(HotelId))]
        public Hotel Hotel { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(PositionId))]
        public Position Position { get; set; } = null!;
    }
}
