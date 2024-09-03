using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using DevFreela.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        public ProjectsController(DevFreelaDbContext context)
        {
            _context = context;
        }
        //GET api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .Skip(page * size)
                .Take(size)
                .ToList();

            var model = projects
                .Where(p => p != null)  // Garante que a entidade Project não é nula
                .Select(ProjectViewModel.FromEntity)
                .ToList();

            return Ok(model);
        }

        // GET api/projects/1234
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            var model = ProjectViewModel.FromEntity(projects);
            return Ok(model);
        }

        // POST api/projects
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var projects = model.ToEntity();
            _context.Projects.Add(projects);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = 1}, model);
        }

        // PUT api/projects/1234

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

            if(projects == null)
            {
                return NotFound();
            }

            projects.Update(model.Title, model.Description, model.TotalCost);   
            
            _context.Projects.Update(projects);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/project/1234
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (projects == null)
            {
                return NotFound();
            }

            projects.SetAsDeleted();
            _context.Projects.Update(projects);
            _context.SaveChanges();
            return NoContent();
        }

        // PUT api/projects/1234/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (projects == null)
            {
                return NotFound();
            }

            projects.Start();
            _context.Projects.Update(projects);
            _context.SaveChanges();
            return NoContent();
        }

        // PUT api/projects/1234/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (projects == null)
            {
                return NotFound();
            }

            projects.Complete();
            _context.Projects.Update(projects);
            _context.SaveChanges();

            return NoContent();
        }
        // POST api/projects/1234/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComments(int id, CreateProjectCommentInputModel model)
        {
            var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (projects == null)
            {
                return NotFound();
            }

            var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);
            _context.ProjectComments.Add(comment);
            _context.SaveChanges();
            return Ok();
        }
    }
}
