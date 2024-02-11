using AutoMapper;
using ju.Dto;
using ju.entity;
using ju.model;
using ju.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ju.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IuserService userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        public UserController(IuserService userService, IMapper mapper, IConfiguration configuration )
        {
            this.userService = userService;
            _mapper = mapper;
            this.configuration = configuration;
           
        }
        [HttpPost, Route("Register")]
        [AllowAnonymous]
        public IActionResult AddUser([FromBody] UserDTO usersDTO)
        {
            try
            {
                User user = _mapper.Map<User>(usersDTO);
                userService.AddUser(user);
                return StatusCode(200, user);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, ex.InnerException.Message);
            }
        }
        [HttpPut, Route("EditUser")]
        [Authorize(Roles = "User")]

        public IActionResult EditUser([FromBody] UserDTO userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                userService.UpdateUser(user);
                return StatusCode(200, user);


            }
            catch (Exception ex)
            {
               

                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete, Route("DeleteUser/{userId}")]
        [Authorize(Roles = "User")]

        public IActionResult DeleteUser(int userId)
        {
            try
            {
                userService.DeleteUser(userId);
                return StatusCode(200, new JsonResult($"User with Id {userId} is Deleted"));
            }

            catch (Exception ex)
            {
               
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("Validate")]
        [AllowAnonymous]
        public IActionResult Validate([FromBody] Login login)
        {
            try
            {
                User user = userService.ValidteUser(login.Email, login.Password);
                AuthResponse authResponse = new AuthResponse();
                if (user != null)
                {
                    authResponse.UserName = user.UserName;
                    authResponse.role = user.role;
                    authResponse.UserId = user.UserId;
                    authResponse.Email = user.Email;
                    authResponse.Password = user.Password;
                   
                    authResponse.Token = GetToken(user);
                }
                return StatusCode(200, authResponse);
            }
            catch (Exception ex)
            {
             

                return StatusCode(500, ex.Message);
            }
        }
        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            //payload part
            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Role, user.role),
                        new Claim(ClaimTypes.Email,user.Email)
                    });

            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
