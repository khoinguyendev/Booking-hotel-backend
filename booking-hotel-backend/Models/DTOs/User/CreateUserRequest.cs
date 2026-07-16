using booking_hotel_backend.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.User
{
    public class CreateUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public Role Role { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string CodeId { get; set; }= string.Empty;
    }
}
