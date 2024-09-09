using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _context;
        public ProjectService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel Complet(int id)
        {
            var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (projects == null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
            }

            projects.Complete();
            _context.Projects.Update(projects);
            _context.SaveChanges();
            return ResultViewModel.Sucess();
        }

        public ResultViewModel Delete(int id)
        {
            var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (projects == null)
            {
                return ResultViewModel.Error("Projeto não existe");
            }

            projects.SetAsDeleted();
            _context.Projects.Update(projects);
            _context.SaveChanges();
            return ResultViewModel.Sucess();
        }

        public ResultViewModel<List<ProjectItemViwModel>> GetAll(string search = "")
        {

            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .ToList();

            var model = projects
                .Where(p => p != null)  // Garante que a entidade Project não é nula
                .Select(ProjectItemViwModel.FromEntity)
                .ToList();

            return ResultViewModel<List<ProjectItemViwModel>>.Sucess(model);
        }

        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            var model = ProjectViewModel.FromEntity(projects);

            if (projects is null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
            }

            return ResultViewModel<ProjectViewModel>.Sucess(model);
        }

        public ResultViewModel<int> Insert(CreateProjectInputModel model)
        {
            var projects = model.ToEntity();
            _context.Projects.Add(projects);
            _context.SaveChanges();

            return ResultViewModel<int>.Sucess(projects.Id);
        }

        public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model)
        {
            var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (projects == null)
            {
                return ResultViewModel.Error("Projeto não existe");
            }

            var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);
            _context.ProjectComments.Add(comment);
            _context.SaveChanges();
            return ResultViewModel.Sucess();
        }

        public ResultViewModel Start(int id)
        {
            var projects = _context.Projects.SingleOrDefault(p => p.Id == id);

            if (projects == null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
            }

            projects.Start();
            _context.Projects.Update(projects);
            _context.SaveChanges();
            return ResultViewModel.Sucess();
        }

        public ResultViewModel Update(UpdateProjectInputModel model)
        {
            var projects = _context.Projects.SingleOrDefault(p => p.Id == model.IdProject);

            if (projects == null)
            {
                return ResultViewModel.Error("Projeto não existe");
            }

            projects.Update(model.Title, model.Description, model.TotalCost);

            _context.Projects.Update(projects);
            _context.SaveChanges();

            return ResultViewModel.Sucess();
        }
    }
}
