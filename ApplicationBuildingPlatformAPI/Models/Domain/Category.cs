using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBuildingPlatformAPI.Models.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        [ForeignKey("Appliations")]
        public int ApplicationId { get; set; }
        public virtual Application? Applications { get; set; }

        [Required]
        [ForeignKey("Questions")]
        public int QuestionId { get; set; }
        public virtual Question? Questions { get; set; }

    }
}
