using System;
using System.Collections.Generic;

namespace SimpleTask.Models
{
    /// <summary>
    /// The Account entity represents holds data for the virtual Account on the platform
    /// </summary>
    public class Account
    {
        public Account()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string SubDomain { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
