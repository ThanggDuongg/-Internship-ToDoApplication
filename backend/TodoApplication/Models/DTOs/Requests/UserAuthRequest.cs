using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApplication.Models.DTOs.Requests
{
    public record UserAuthRequest
    {
        //[Required(ErrorMessage = "Username is required")]
        [Required]
        public string? Username { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[JsonIgnore]
        [Required]
        public string? Password { get; set; }

        //public string accessToken   { get; set; }

        //public string refreshToken { get; set; }
    }
}
