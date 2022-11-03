using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Domain
{
    public class Client
    {
        public Guid Id { get; set; }
        [Required, MinLength(2), MaxLength(100), Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        [Phone, PersonalData]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        [Column(TypeName = "jsonb")]
        public List<Application>? Applications { get; set; }
        
    }
}
