using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTask.Models
{
    public partial class Locations
    {
        public Locations()
        {
            Classrooms = new HashSet<Classrooms>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Column("AccountID")]
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
        [Column(TypeName = "char(2)")]
        public string State { get; set; }
        [StringLength(75)]
        public string Province { get; set; }
        [Column(TypeName = "char(2)")]
        public string CountryCode { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }

        [ForeignKey("AccountId")]
        [InverseProperty("Locations")]
        public Accounts Account { get; set; }
        [InverseProperty("Location")]
        public ICollection<Classrooms> Classrooms { get; set; }
    }
}
