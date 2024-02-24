using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Users
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The Limit of password should be from {2} to {1}", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
