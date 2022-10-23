using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBuildingPlatformAPI.Models.Domain
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string? Title { get; set; }
        [Required]
        [StringLength(255)]
        public string? Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime RespondedAt { get; set; }

        [ForeignKey("Questions")]
        public int QuestionId { get; set; }
        public virtual Question? Questions { get; set; }

        [ForeignKey("Conditions")]
        public int ConditionId { get; set; }
        public virtual Condition? Conditions { get; set; }
    }
}
