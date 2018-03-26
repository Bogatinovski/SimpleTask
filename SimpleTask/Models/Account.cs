using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        [StringLength(10)]
        public string SubDomain { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(1), MaxLength(1)]
        public string Status { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
