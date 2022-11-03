using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class UserLoginRequest
    {
        //[Required, EmailAddress]
        //public string Email { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
