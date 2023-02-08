using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Text;
using TodoApplication.Entities;
using TodoApplication.Filters;
using TodoApplication.Helpers;
using TodoApplication.Models;
using TodoApplication.Models.DTOs.Requests;
using TodoApplication.Models.DTOs.Responses;
using TodoApplication.Services.Interfaces;

namespace TodoApplication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly JwtService jwtService;

        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            this._userService = userService;
            this._mapper = mapper;
            this._configuration = configuration;
            this.jwtService = new JwtService(this._configuration);
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshNewToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            string accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            
            try
            {
                string val_refreshToken = this.jwtService.ValidateJwtToken(refreshTokenRequest.refreshToken);
                // return user_id
                if (String.IsNullOrEmpty(val_refreshToken))
                {
                    return BadRequest(new Response<UserEntity>
                    {
                        Success = false,
                        Message = "Something wrong ...",
                    });
                }
                else
                {
                    UserEntity userEntity = await this._userService.getByIdAsync(Guid.Parse(val_refreshToken));
                    string newAccessToken = this.jwtService.GenerateAccessToken(userEntity);
                    RefreshTokenResponse refreshTokenResponse = new RefreshTokenResponse
                    {
                        Username = userEntity.Username,
                        accessToken = newAccessToken,
                        refreshToken = refreshTokenRequest.refreshToken,
                    };
                    return Ok(new Response<RefreshTokenResponse>
                    {
                        Success = true,
                        Data = refreshTokenResponse,
                        Message = "Refresh new access token successfully!"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }
        

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserAuthRequest userAuthRequest)
        {
            //if (ModelState.IsValid == false)
            if (String.IsNullOrEmpty(userAuthRequest.Username) || String.IsNullOrEmpty(userAuthRequest.Password))
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(new Response<UserEntity>
                {
                    Success = false,
                    Message = "Something wrong ...",
                    Errors = allErrors
                });
            }

            try
            {
                UserEntity user = await this._userService.login(userAuthRequest);
                UserAuthResponse userAuthResponse = this._mapper.Map<UserAuthResponse>(user);
                userAuthResponse.accessToken = this.jwtService.GenerateAccessToken(user);
                userAuthResponse.refreshToken = this.jwtService.GenerateRefreshToken(user.Id.ToString());
                return Ok(new Response<UserAuthResponse>
                {
                    Success = true,
                    Data = userAuthResponse,
                    Message = "login successfully !"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserAuthRequest userAuthenRequest)
        {
            //if (ModelState.IsValid == false)
            if (String.IsNullOrEmpty(userAuthenRequest.Username) || String.IsNullOrEmpty(userAuthenRequest.Password))
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(new Response<UserEntity>
                {
                    Success = false,
                    Message = "Something wrong ...",
                    Errors = allErrors
                });
            }

            try
            {
                UserEntity user = await this._userService.register(userAuthenRequest);
                UserAuthResponse userAuthResponse = this._mapper.Map<UserAuthResponse>(user);
                userAuthResponse.accessToken = this.jwtService.GenerateAccessToken(user);
                userAuthResponse.refreshToken = this.jwtService.GenerateRefreshToken(user.Id.ToString());
                return Ok(new Response<UserAuthResponse>
                {
                    Success = true,
                    Data = userAuthResponse,
                    Message = "register successfully !"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Exception>
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }
    }
}
