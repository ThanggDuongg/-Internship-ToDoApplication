namespace TodoApplication.Models.DTOs.Responses
{
    public class RefreshTokenResponse
    {
        public string? Username { get; set; }

        public string? accessToken { get; set; }

        public string? refreshToken { get; set; }
    }
}
