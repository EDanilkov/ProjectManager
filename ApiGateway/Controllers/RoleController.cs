using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApiGateway.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiGateway.Controllers
{
    [Route("roleapi/")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [HttpGet("{userId}/{projectId}")]
        public async Task<Role> GetRoleInProjectAsync(int projectId, int userId)
        {
            var url = "http://project-container:80/userproject/" + projectId + "/" + userId;
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
