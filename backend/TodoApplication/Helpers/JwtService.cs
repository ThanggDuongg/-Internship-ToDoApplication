using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApplication.Constants;
using TodoApplication.Entities;

namespace TodoApplication.Helpers
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string GenerateAccessToken(UserEntity userEntity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // hash key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Token:SceretKey"]));

            // payload
            var claims = new[]
            {
                new Claim("userid", userEntity.Id.ToString()),
                new Claim(ClaimTypes.Name, userEntity.Username),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["Token:ValidAudience"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Token:ValidIssuer"])
            };

            // create token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(JwtExpireConstants.EXPIRE_ACCESS_TOKEN),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // hash key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Token:SceretKey"]));

            // payload
            var claims = new[]
            {
                new Claim("userid", userId),
            };

            // create token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(JwtExpireConstants.EXPIRE_REFRESH_TOKEN),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ValidateJwtToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Token:SceretKey"]));

            try
            {
                tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string user_id = jwtToken.Claims.First(c => c.Type == "userid").Value;

                // Check token expired
                string exp = jwtToken.Claims.First(c => c.Type == "exp").Value;
                long exp_long = long.Parse(exp);

                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(exp_long);
                DateTime dateTimeExpires = dateTimeOffset.LocalDateTime;

                if (dateTimeExpires < DateTime.UtcNow)
                {
                    return "";
                }

                // return username from JWT token if validation successful
                return user_id;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
