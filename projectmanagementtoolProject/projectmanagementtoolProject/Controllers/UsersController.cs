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
        string message = "";

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
        public bool Delete(int id)
        {
            var users = Dbcontext.Users.Where(u => Dbcontext.Projects.Any(p => p.OwnerId == id) || Dbcontext.Jobs.Any(p => p.UserId == p.Id)).ToList();
            if (users.Count > 0)
            { 
                message = "User Cannot be Deleted";
                return false;
            }
            else
            {
                User user = Dbcontext.Users.Find(id);
                Dbcontext.Remove(user);
                message = "User Deleted Succcessfully";
                Dbcontext.SaveChanges();
                return true;

            }
               



        }

        //[HttpGet("user/{email}")]
        //public IActionResult getbyEmail(string email)
        //{
        //    List<User> users = Dbcontext.Users.Where(u => u.email == Email).ToList();
        //    return Ok(users);
        //}
        [HttpGet("jobs/{userid}")]
        public IActionResult Listtasks(int userid)
        {
            
            var userJobs = Dbcontext.UserJobs.Where(uj => uj.UserId == userid).Select(uj => uj.Job).ToList();

            return Ok(userJobs);
        }

        [HttpPost("assigntask")]
        public IActionResult AssignTask([FromBody] UserJob userjob)
        {
            userjob.DateAssigned = DateTime.Now;
            Dbcontext.UserJobs.Add(userjob);
            Dbcontext.SaveChanges();
            return Ok(userjob);
        }

        [HttpDelete("unassigned/{userId}/{jobId}")]
        public IActionResult UnassignJob(int userId, int jobId)
        {
            List<UserJob> userJobs = Dbcontext.UserJobs.Where(ut => ut.UserId == userId & ut.JobId == jobId).ToList();
            foreach (UserJob userjob in userJobs)
            {
                Dbcontext.Remove(userjob);
            }
            Dbcontext.SaveChanges();
            return Ok();
        }


    }
}
