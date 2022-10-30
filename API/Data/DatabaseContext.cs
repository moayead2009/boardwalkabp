using ApplicationBuildingPlatformAPI.Models.Domain;
using ApplicationBuildingPlatformAPI.Models.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationBuildingPlatformAPI.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<TokenInfo> TokenInfo { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
    }
}
