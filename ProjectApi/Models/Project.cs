﻿using System;

namespace ProjectApi.Models
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid AdminId { get; set; }
    }
}
