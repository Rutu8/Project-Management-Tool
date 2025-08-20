using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectmanagementtoolProject.Context;
using projectmanagementtoolProject.Models;
using System.Threading.Tasks;

namespace projectmanagementtoolProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        ProjectDBContext dbContext;
        string message = "";
        public ProjectsController(ProjectDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Project> list = dbContext.Projects.ToList();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            dbContext.Projects.Add(project);
            dbContext.SaveChanges();
            return Ok(project);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Project project)
        {
            Project plan =dbContext.Projects.Find(id);
            plan.Name = project.Name;
            plan.Description = project.Description;
            plan.OwnerId = project.OwnerId; 
            dbContext.Update(plan);
            dbContext.SaveChanges();
            return Ok(plan);
        }

        [HttpGet("project/{id}")]
        public IActionResult Get(int id)
        {
            Project project = dbContext.Projects.Find(id);
            return Ok(project);
        }

        [HttpGet("owner/{ownerId}")]
        public IActionResult GetProjectsbyowner(int ownerId)
        {
            List<Project> projects = dbContext.Projects.Where(p=>p.OwnerId==ownerId).ToList();
            return Ok(projects);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var projects = dbContext.Projects.Where(p => dbContext.Jobs.Any(j => j.ProjectId == id)).ToList();
            if(projects.Count > 0)
            {
                message = "Given Project Cannot be deleted";
                return false;
            }
            else
            {
                Project project = dbContext.Projects.Find(id);
                dbContext.Remove(project);
                dbContext.SaveChanges();
                return true;

            }
               
        }

        [HttpGet("tasks")]
        public IActionResult Listtask()
        {
            List<Job> list = dbContext.Jobs.ToList();
            return Ok(list);
        }

        [HttpPost("tasks")]
        public IActionResult Posttask([FromBody] Job job)
        {
            dbContext.Jobs.Add(job);
            dbContext.SaveChanges();
            return Ok(job);
        }

        [HttpPut("tasks/{id}")]
        public IActionResult Puttask(int id, [FromBody] Job Job)
        {
            Job job = dbContext.Jobs.Find(id);
            job.Title = Job.Title;
            job.Status = Job.Status;
            job.ProjectId = job.ProjectId;
            job.Priority = job.Priority;
            dbContext.Update(job);
            dbContext.SaveChanges();
            return Ok(job);
        }

        [HttpDelete("tasks/{id}")]
        public bool Deletetask(int id)
        {
            var jobs = dbContext.Jobs.Where(j=>dbContext.UserJobs.Any(uj=>uj.JobId==id)).ToList();
            if (jobs.Count > 0)
            {
                message = "Job Cannot be deleted";
                return false;
            }
            else
            {
                Job job = dbContext.Jobs.Find(id);
                dbContext.Remove(job);
                dbContext.SaveChanges();
                return true;

            }
               
        }

        [HttpGet("tasks/byid/{id}")]
        public IActionResult Gettasks(int id)
        {
            Job job = dbContext.Jobs.Find(id);
            return Ok(job);
        }

        [HttpGet("tasks/{projectId}")]
        public IActionResult GettaskbyProjectid(int projectId)
        {
            List<Job> job = dbContext.Jobs.Where(j=>j.ProjectId==projectId).ToList();
            return Ok(job);
        }

        [HttpGet("tasks/{id}/{projectId}")]
        public IActionResult Gettaskby(int id)
        {
            List<Job> job = dbContext.Jobs.Where(j=>j.Id == id).ToList();
            return Ok(job);
        }

        //[HttpGet("tasks/usertask/{userid}")]
        //public IActionResult GettaskUsers(int userid)
        //{
        //    var jobs= dbContext.Jobs.Select(t => t.User).Where(t => t.Id == userid).ToList();
        //    return Ok(jobs);
        //}

       




    }
}
