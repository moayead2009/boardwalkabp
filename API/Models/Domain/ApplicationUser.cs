using Microsoft.AspNetCore.Identity;

namespace ApplicationBuildingPlatformAPI.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

    }
}
