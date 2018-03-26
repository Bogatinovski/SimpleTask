using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTask.Models
{
    /// <summary>
    /// The Classroom entity holds the data for each individual classroom
    /// </summary>
    public class Classroom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Location")]
        public int LocationId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }

        public Location Location { get; set; }
    }
}
