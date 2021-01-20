using ApiGateway.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [Route("roleapi/")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [HttpGet("{userId}/{projectId}")]
        public async Task<Role> GetRoleInProjectAsync(Guid projectId, Guid userId)
        {
            var url = "http://project-container:80/project/" + projectId + "/user/" + userId;
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            var readAsStringAsync = response.Content.ReadAsStringAsync();
            UserProject userProject = JsonConvert.DeserializeObject<UserProject>(readAsStringAsync.Result);

            url = "http://user-container:80/role/" + userProject.RoleId;
            response = await client.GetAsync(url);
            Role role = JsonConvert.DeserializeObject<Role>(response.Content.ReadAsStringAsync().Result);

            return role;
        }
    }
}
