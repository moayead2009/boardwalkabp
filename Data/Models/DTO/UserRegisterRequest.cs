using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class UserRegisterRequest
    {
        [Required, MinLength(2), MaxLength(100), Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters.")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password", ErrorMessage = "Passwords did not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        //[Phone, PersonalData]
        //public string PhoneNumber { get; set; } = string.Empty;
        //[Required]
        //public string Address { get; set; } = string.Empty;
    }
}
