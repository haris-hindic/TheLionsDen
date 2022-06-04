using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class JobType
    {
        public JobType()
        {
            Employees = new HashSet<Employee>();
        }

        public int JobTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
