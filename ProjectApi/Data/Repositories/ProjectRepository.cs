using Microsoft.EntityFrameworkCore;
using ProjectApi.Data.Repositories.Contracts;
using ProjectApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        ProjectManagerContext db;

        public ProjectRepository(ProjectManagerContext _db)
        {
            db = _db;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            db.Project.Add(project);
            await db.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            db.Project.Update(project);
            await db.SaveChangesAsync();
            return project;
        }

        public IEnumerable<Project> Get()
            => db.Project;

        public IEnumerable<Project> Get(string projectName)
            => db.Project.Where(c => c.Name.Contains(projectName));

        public async Task<Project> GetAsync(int projectId)
            => await db.Project.Where(c => c.Id == projectId).FirstAsync();

        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(int userId)
        {
            var projects = new List<Project>();
            var userProjects = db.UserProject.Where(u => u.UserId == userId).ToList();
            foreach (UserProject userProject in userProjects)
            {
                var project = await db.Project.FirstOrDefaultAsync(p => p.Id == userProject.ProjectId);
                if(project != null)
                {
                    projects.Add(new Project() { Id = project.Id, Name = project.Name, AdminId = project.AdminId});
                }
            }
            return projects;
        }

        public IEnumerable<int> GetUsersIdFromProject(int projectId)
        {
            var usersId = new List<int>();
            var userProjects = db.UserProject.Where(u => u.ProjectId == projectId).ToList();
            foreach (UserProject userProject in userProjects)
            {
                usersId.Add(userProject.UserId);
            }
            return usersId;
        }

        public async Task DeleteAsync(int projectId)
        {
            Project project = await GetAsync(projectId);
            var userProjects = db.UserProject.Where(u => u.ProjectId == projectId).ToList();
            foreach (UserProject userProject in userProjects)
            {
                db.UserProject.Remove(userProject);
            }
            await db.SaveChangesAsync();
            db.Project.Remove(project);
            await db.SaveChangesAsync();
        }
    }
}
