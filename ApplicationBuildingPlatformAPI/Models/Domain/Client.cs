using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBuildingPlatformAPI.Models.Domain
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        [StringLength(100)]
        public string? UserName { get; set; }
        [Required]
        [StringLength(255)]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [StringLength(15)]
        public string? PhoneNumber { get; set; }
        [Required]
        [StringLength(100)]
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        [ForeignKey("Appliations")]
        public int ApplicationId { get; set; }
        public virtual Application Applications { get; set; }

    }
}
