using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationBuildingPlatformAPI.Models.Domain
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Type { get; set; }
        [Required]
        [StringLength(255)]
        public string? Body { get; set; }
        public string? Answer { get; set; }
        public int Order { get; set; }

        [ForeignKey("Conditions")]
        public int ConditionId { get; set; }
        public virtual Condition? Conditions { get; set; }

    }
}
