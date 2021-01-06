using ApiGateway.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [Route("projectapi/")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpPost()]
        public async Task<Project> CreateAsync([FromBody] Project project)
        {
            var jsonProject = JsonConvert.SerializeObject(project);
            var data = new StringContent(jsonProject, Encoding.UTF8, "application/json");

            var url = "http://project-container:80/project";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            var readAsStringAsync = response.Content.ReadAsStringAsync();
            project = JsonConvert.DeserializeObject<Project>(readAsStringAsync.Result);

            url = "http://user-container:80/role";
            response = await client.GetAsync(url);
            readAsStringAsync = response.Content.ReadAsStringAsync();
            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(readAsStringAsync.Result);

            var jsonUserProject = JsonConvert.SerializeObject(new UserProject() {ProjectId = project.Id, RoleId = roles.First(r => r.Name == "admin").Id, UserId = project.AdminId});
            data = new StringContent(jsonUserProject, Encoding.UTF8, "application/json");

            url = "http://project-container:80/userproject";

            response = await client.PostAsync(url, data);
            return project;
        }

        [HttpGet("{projectId}/user")]
        public async Task<List<User>> GetUsersFromProjectAsync(int projectId)
        {
            var url = "http://project-container:80/project/" + projectId + "/users";
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var readAsStringAsync = response.Content.ReadAsStringAsync();
            List<int> usersId = JsonConvert.DeserializeObject<List<int>>(readAsStringAsync.Result);

            List<User> users = new List<User>();
            foreach (int userId in usersId)
            {
                url = "http://user-container:80/user/" + userId;
                response = await client.GetAsync(url);
                User user = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
                users.Add(user);
            }
            return users;
        }
    }
}
