using IdentityApi.Data.Models;
using IdentityApi.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data.Repositories
{
    public class ProjectManagerContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options)
            : base(options)
        {
        }
    }
}
