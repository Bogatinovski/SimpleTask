using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Account")]
        public int AccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [MinLength(2), MaxLength(2)]
        public string State { get; set; }

        [StringLength(75)]
        public string Province { get; set; }

        [MinLength(2), MaxLength(2)]
        public string CountryCode { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public Account Account { get; set; }
        public ICollection<Classroom> Classrooms { get; set; }
    }
}
