using Microsoft.EntityFrameworkCore;
using ProjectApi.Models;

namespace ProjectApi.Data.Repositories
{
    public class ProjectManagerContext : DbContext
    {
        public DbSet<Project> Project { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<UserProject> UserProject { get; set; }

        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options)
            : base(options)
        {
        }
    }
}
