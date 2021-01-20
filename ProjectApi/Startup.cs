using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectApi.Data.Repositories;
using ProjectApi.Data.Repositories.Contracts;
using ProjectApi.Data.Services;
using ProjectApi.Data.Services.Contracts;

namespace ProjectApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ProjectManagerContext>(options =>
                options.UseSqlServer(Configuration.GetValue<string>("db:connectionString")));

            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IUserProjectService, UserProjectService>();
            services.AddTransient<IUserProjectRepository, UserProjectRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
