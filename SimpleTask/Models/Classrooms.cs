using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTask.Models
{
    public partial class Classrooms
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("LocationID")]
        public int LocationId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int Capacity { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("Classrooms")]
        public Locations Location { get; set; }
    }
}
