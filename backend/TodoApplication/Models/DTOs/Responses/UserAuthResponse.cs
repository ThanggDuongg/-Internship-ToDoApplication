using System.Text.Json.Serialization;

namespace TodoApplication.Models.DTOs.Responses
{
    public record UserAuthResponse
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Username { get; set; }

        public string? accessToken { get; set; }
        
        public string? refreshToken { get; set; }
    }
}
