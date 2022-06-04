﻿using System;
using System.Collections.Generic;

namespace TheLionsDen.Services.Database
{
    public partial class Facility
    {
        public Facility()
        {
            Employees = new HashSet<Employee>();
            ReservationFacilites = new HashSet<ReservationFacilite>();
        }

        public int FacilityId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public float Price { get; set; }
        public string Status { get; set; } = null!;
        public byte[]? Image { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ReservationFacilite> ReservationFacilites { get; set; }
    }
}
