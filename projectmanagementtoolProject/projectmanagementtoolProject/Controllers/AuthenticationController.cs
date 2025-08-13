using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using projectmanagementtoolProject.Context;
using projectmanagementtoolProject.DTOs;
using projectmanagementtoolProject.Models;

namespace projectmanagementtoolProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        ProjectDBContext dBContext;
        public AuthenticationController(ProjectDBContext dBContext)
        {
            this.dBContext = dBContext;
        }


        [HttpPost("Login")]
        public IActionResult LoginUser(Login login)
        {
            LoginStatus status = new LoginStatus();
            status.message = "failed";
            status.usertype = " ";
            List<User> users = dBContext.Users.Where(u=>u.Email.Equals(login.Email) && u.Password.Equals(login.Password)).ToList();
            if(users.Count > 0)
            {
                //var UserType = dBContext.Users.Where(u => u.Usertype.Equals("admin")).ToString();
                if (status.users[0].Usertype == "admin")
                {
                    status.status = "Success";
                    status.message = "Login Successfully";
                    status.usertype = "admin";
                  
                }
                else
                {
                    status.status = "Success";
                    status.message = "Login Successfully";
                    status.usertype = "user";
                    status.user = users[0];
                  
                }
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
