using ApiGateway.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
        [HttpPost]
        public async Task<Project> CreateAsync([FromBody] Project project)
        {
            var jsonProject = JsonConvert.SerializeObject(project);

            var data = new StringContent(jsonProject, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var url = "http://user-container:80/role";
            var response = await client.GetAsync(url);
            var readAsStringAsync = response.Content.ReadAsStringAsync();
            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(readAsStringAsync.Result);

            url = "http://project-container:80/project?roleId=" + roles.First(r => r.Name == "admin").Id;

            response = await client.PostAsync(url, data);
            readAsStringAsync = response.Content.ReadAsStringAsync();
            project = JsonConvert.DeserializeObject<Project>(readAsStringAsync.Result);

            return project;
        }
        
        [HttpPut]
        public async Task<Project> UpdateAsync([FromBody] Project project)
        {
            var jsonProject = JsonConvert.SerializeObject(project);

            var data = new StringContent(jsonProject, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var url = "http://project-container:80/project";
            var response = await client.PutAsync(url, data);
            var readAsStringAsync = response.Content.ReadAsStringAsync();
            project = JsonConvert.DeserializeObject<Project>(readAsStringAsync.Result);
           
            return project;
        }

        [HttpGet("{projectId}/users")]
        public async Task<List<User>> GetUsersFromProjectAsync(Guid projectId)
        {
            var url = "http://project-container:80/project/" + projectId + "/users";
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var readAsStringAsync = response.Content.ReadAsStringAsync();
            List<Guid> usersId = JsonConvert.DeserializeObject<List<Guid>>(readAsStringAsync.Result);

            List<User> users = new List<User>();
            foreach (Guid userId in usersId)
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
