using System.ComponentModel.DataAnnotations;

namespace TodoApplication.Models.DTOs.Requests
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "Refresh token invalid")]
        public string? refreshToken { get; set; }
    }
}
