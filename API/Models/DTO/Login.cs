using System.ComponentModel.DataAnnotations;

namespace ApplicationBuildingPlatformAPI.Models.DTO
{
    public class Login
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        //[Required]
        //public string? Email { get; set; }
    }
}
