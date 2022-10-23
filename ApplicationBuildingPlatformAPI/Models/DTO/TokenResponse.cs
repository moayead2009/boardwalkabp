namespace ApplicationBuildingPlatformAPI.Models.DTO
{
    public class TokenResponse
    {
        public string? TokenString { get; set; }
        public DateTime ValidTo { get; set; }
        //public bool? Success { get; set; }
        //public string Error { get; set; }
        //public DateTime? Created { get; set; }
        //public int? StatusCode { get; set; }
    }
}
