using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AdminId { get; set; }
    }
}
