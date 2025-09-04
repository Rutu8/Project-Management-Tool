using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office.PowerPoint.Y2023.M02.Main;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectmanagementtoolProject.Context;
using projectmanagementtoolProject.Models;
using System.Collections.Specialized;
using System.Data;
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

        [NonAction]
        public DataTable Listprojects()
        {
            DataTable dt = new DataTable();
            dt.TableName = "projects";
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("OwnerId", typeof(int));
            var _list = dbContext.Projects.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.Id, item.Name, item.Description, item.OwnerId);
                });
            }
            return dt;
        }


        [HttpGet("excel")]
        public IActionResult ExportData()
        {
            var projects = Listprojects();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(projects, "Project data");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheet.sheet", "projects.xlsx");
                }
            }

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
            //Project project = dbContext.Projects.Find(id);
            var project = dbContext.Projects.Where(p => p.Id == id).Select(p => new { p.Id, p.Name, p.Description, p.OwnerId, OwnerName = p.Owner.Name }).FirstOrDefault();
            return Ok(project);
        }
        

        [HttpGet("owner/{ownerId}")]
        public IActionResult GetProjectsbyowner(int ownerId)
        {
            var projects = dbContext.Projects.Where(p => p.OwnerId == ownerId).Select(p=>new {Id = p.Id, name= p.Name, Description = p.Description, ownerId = p.OwnerId }).ToList();
            return Ok(projects);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var projects = dbContext.Projects.Where(p => dbContext.Jobs.Any(j => j.ProjectId == id)).ToList();
            if(projects.Count > 0)
            {
                message = "Given Project Cannot be deleted";
                return Ok(new {message =  message});
            }
            else
            {
                Project project = dbContext.Projects.Find(id);
               dbContext.Remove(project);
                dbContext.SaveChanges();
                return Ok(new { success = "success", message ="Project is successfully deleted"});

            }
               
        }

        [HttpGet("tasks")]
        public IActionResult Listtask()
        {
            //List<Job> list = dbContext.Jobs.ToList();
            var list = from t in dbContext.Jobs join p in dbContext.Projects on t.ProjectId equals p.Id into projGroup from Project in projGroup.DefaultIfEmpty() select new { t.Id, t.Title, t.Status, t.ProjectId, t.Priority, Project.Name };
            return Ok(list);
        }

        [NonAction]
        public DataTable Listjobs()
        {
            DataTable dt = new DataTable();
            dt.TableName = "jobs";
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("ProjectId", typeof(int));
            dt.Columns.Add("Priority", typeof(string));
            var _list = dbContext.Jobs.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.Id, item.Title, item.Status, item.ProjectId, item.Priority);
                });
            }
            return dt;
        }


        [HttpGet("tasks/excel")]
        public IActionResult ExportJobs()
        {
            var jobs = Listjobs();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(jobs, "Tasks data");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheet.sheet", "jobs.xlsx");
                }
            }

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
        public IActionResult Deletetask(int id)
        {
            var jobs = dbContext.Jobs.Where(j => dbContext.UserJobs.Any(uj => uj.JobId == id)).ToList();
            if (jobs.Count > 0)
            {
                message = "Job cannot be deleted because job assigned to user";
                return Ok(new {message =  message});               
            }
            else
            {
                Job job = dbContext.Jobs.Find(id);
                dbContext.Remove(job);
                dbContext.SaveChanges();
                return Ok(new {success = "success", message = "Task successfully deleted"});

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
            var job = dbContext.Jobs.Where(p => p.ProjectId == projectId).Select(p => new { p.Project.Name, Id = p.Id, title = p.Title, Status = p.Status,ProjectId = p.ProjectId, ownerId = p.Project.OwnerId, Priority=p.Priority, UserId=p.UserId}).ToList();
           // var job = dbContext.Jobs.Where(j => j.ProjectId == projectId).Select(j => new{j.Id, j.Title,j.ProjectId, j.Status, j.Priority, ProjectName = j.Project.Name}).ToList();

            return Ok(job);
        }

        [HttpGet("tasks/{id}/{projectId}")]
        public IActionResult Gettaskby(int id)
        {
            List<Job> job = dbContext.Jobs.Where(j=>j.Id == id).ToList();
            return Ok(job);
        }

      

       




    }
}
