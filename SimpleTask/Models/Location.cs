using System;
using System.Collections.Generic;

namespace SimpleTask.Models
{
    /// <summary>
    /// The Location entity represents a physical location that belongs to a single Account and can have multiple Classrooms
    /// </summary>
    public class Location
    {
        public Location()
        {
            Classrooms = new HashSet<Classroom>();
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public string CountryCode { get; set; }
        public bool? IsActive { get; set; }

        public Account Account { get; set; }
        public ICollection<Classroom> Classrooms { get; set; }
    }
}
