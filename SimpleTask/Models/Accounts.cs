using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTask.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            Locations = new HashSet<Locations>();
        }

        [Column("ID")]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        [Required]
        [StringLength(10)]
        public string SubDomain { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "char(1)")]
        public string Status { get; set; }

        [InverseProperty("Account")]
        public ICollection<Locations> Locations { get; set; }
    }
}
