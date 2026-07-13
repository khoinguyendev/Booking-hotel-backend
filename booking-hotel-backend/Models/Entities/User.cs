using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using booking_hotel_backend.Models.Enums;

namespace booking_hotel_backend.Models.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string OtpCode { get; set; } = string.Empty;
        public string ExpiredAt { get; set; } = string.Empty;
        public bool Verified { get; set; } = true;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? Avatar { get; set; }
        [Column("role")]
        public Role Role { get; set; } = Role.Customer;
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
