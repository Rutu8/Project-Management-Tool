using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectmanagementtoolProject.Context;
using projectmanagementtoolProject.Models;
namespace projectmanagementtoolProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ProjectDBContext Dbcontext;
        public UsersController(ProjectDBContext dbcontext)
        {
            this.Dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult List()
        {
            List<User> users = this.Dbcontext.Users.ToList();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User User)
        {
            Dbcontext.Users.Add(User);
            Dbcontext.SaveChanges();
            return Ok(User);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User user= Dbcontext.Users.Find(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            User users = Dbcontext.Users.Find(id);
            users.Name = user.Name;
            users.Email = user.Email;
            users.MobileNo = user.MobileNo;
            users.Password = user.Password;
            users.Usertype = user.Usertype;
            Dbcontext.Update(users);
            Dbcontext.SaveChanges();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Project project = new Project();
            //List<Project> list = Dbcontext.Projects.ToList();
            
            User user= Dbcontext.Users.Find(id);
            Dbcontext.Remove(user);
            Dbcontext.SaveChanges();
            return Ok(user);
        }

        //[HttpGet("user/{email}")]
        //public IActionResult getbyEmail(string email)
        //{
        //    List<User> users = Dbcontext.Users.Where(u=>u.email==Email).ToList();
        //    return Ok(users);
        //}


    }
}
