using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using projectmanagementtoolProject.Context;
using projectmanagementtoolProject.Models;
using System.Data;
using System.Data.Common;
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

        [NonAction]
        public DataTable Listusers()
        {
            DataTable dt = new DataTable();
            dt.TableName = "users";
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("MobileNo", typeof(string));
            dt.Columns.Add("Password", typeof(string));
            dt.Columns.Add("Usertype", typeof(string));
            var _list =Dbcontext.Users.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.Id, item.Name, item.Email, item.MobileNo, item.Password, item.Usertype);
                });
            }
            return dt;
        }


        [HttpGet("excel")]
        public IActionResult ExportData()
        {
            var users = Listusers();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(users, "Users data");
                using(MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheet.sheet", "users.xlsx");
                }
            }
              
        }

        [HttpPost]
        public IActionResult Post([FromBody] User User)
        {
            //if (User.Userfile != null)
            //{
            //    string filename = Guid.NewGuid() + ".xsl";

            //}
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
        public IActionResult listtasks(int userid)
        {
            //projects = dbContext.Projects.Where(p => p. == ownerId).Select(p => new { p.Owner.Name, Id = p.Id, title = p.Name, Description = p.Description, ownerId = p.OwnerId }).ToList();
            //var userJobs = Dbcontext.UserJobs.Where(uj => uj.UserId == userid).Select(uj => uj.Job).ToList();
            //var userjob = Dbcontext.UserJobs.Where(uj => uj.UserId == userid).Select
            //var userjob = Dbcontext.UserJobs.Where(uj => uj.UserId == userid & uj.Project.Id == uj.ProjectId).Select(uj => new {UserId = uj.User.Id, uj.Job, uj.DateAssigned, uj.Job.ProjectId, uj.Project.Name});
            var userjob = from uj in Dbcontext.UserJobs join p in Dbcontext.Projects on uj.Job.ProjectId equals p.Id into projGroup from Project in projGroup.DefaultIfEmpty() select new { uj.Id, uj.Job.ProjectId, ProjectName = Project != null ? Project.Name : "Unknown", uj.Job, uj.DateAssigned, uj.UserId };
            var result = userjob.ToList();

            return Ok(result);
        }

        [NonAction]
        public DataTable Listusersjobs()
        {
            DataTable dt = new DataTable();
            dt.TableName = "userjobs";
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("UserId", typeof(int));
            dt.Columns.Add("JobId", typeof(int));
            dt.Columns.Add("DateAssigned", typeof(System.DateTime));
            dt.Columns.Add("ProjectId", typeof(int));
            var _list = Dbcontext.UserJobs.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.Id, item.UserId, item.JobId, (item.DateAssigned).ToString(), item.ProjectId);
                });
            }
            return dt;
        }


        [HttpGet("userjobs/excel")]
        public IActionResult ExportUserjobs()
        {
            var userjobs = Listusersjobs();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(userjobs, "Users job data");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheet.sheet", "userjobs.xlsx");
                }
            }

        }


        [HttpPost("assigntask")]
        public IActionResult AssignTask([FromBody] UserJob userjob)
        {
            userjob.DateAssigned = DateTime.Now;
            //userjob.ProjectId = userjob.Project.Id;
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
