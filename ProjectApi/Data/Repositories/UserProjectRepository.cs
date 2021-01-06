using Microsoft.EntityFrameworkCore;
using ProjectApi.Data.Repositories.Contracts;
using ProjectApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Data.Repositories
{
    public class UserProjectRepository : IUserProjectRepository
    {
        ProjectManagerContext db;

        public UserProjectRepository(ProjectManagerContext _db)
        {
            db = _db;
        }
        public async Task<UserProject> CreateAsync(UserProject userProject)
        {
            db.UserProject.Add(userProject);
            await db.SaveChangesAsync();
            return userProject;
        }

        public async Task<UserProject> GetAsync(int projectId, int userId)
            => await db.UserProject.FirstAsync(c => c.ProjectId == projectId && c.UserId == userId);

        public async Task<UserProject> UpdateAsync(UserProject userProject)
        {
            db.UserProject.Update(userProject);
            await db.SaveChangesAsync();
            return userProject;
        }

        public async Task DeleteAsync(int projectId, int userId)
        {
            UserProject userProject = await db.UserProject.FirstAsync(c => c.ProjectId == projectId && c.UserId == userId);
            db.UserProject.Remove(userProject);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userProjectId)
        {
            UserProject userProject = db.UserProject.First(u => u.Id == userProjectId);
            db.UserProject.Remove(userProject);
            await db.SaveChangesAsync();
        }

    }
}
