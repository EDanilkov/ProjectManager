using System;

namespace ApiGateway.Models
{
    public class UserProject
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
