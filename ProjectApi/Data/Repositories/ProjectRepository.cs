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


        public async Task DeleteAsync(int projectId)
        {
            Project project = await GetAsync(projectId);
            db.Project.Remove(project);
            await db.SaveChangesAsync();
        }
    }
}
