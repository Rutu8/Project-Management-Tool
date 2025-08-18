using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using projectmanagementtoolProject.Context;
using projectmanagementtoolProject.DTOs;
using projectmanagementtoolProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace projectmanagementtoolProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        ProjectDBContext dBContext;
        IConfiguration _configuration;
        public AuthenticationController(ProjectDBContext dBContext, IConfiguration configuration)
        {
            this.dBContext = dBContext;
            _configuration = configuration;
        }


        [HttpPost("Login")]
        public IActionResult LoginUser(Login login)
        {
            LoginStatus status = new LoginStatus();
            status.message = "failed";
            status.usertype = " ";
           var users = dBContext.Users.FirstOrDefault(u=>u.Email.Equals(login.Email) && u.Password.Equals(login.Password));
            if(users!= null)
            {
                status.status = "success";
                status.message = "Login Successfully";
            
               var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, users.Email),
                new Claim("usertype", users.Usertype),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims:claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirMinutes"])),
                    signingCredentials:creds
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    usertype = users.Usertype?.Trim(),
                    userId = users.Id,
                    status = status.status,
                    message = status.message

                });
            }
            else 
            {
                status.status = "error";
                status.message = "Login Failed";
            }
            return Ok(status);
        }

    }
}
