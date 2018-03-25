using System;
using System.Collections.Generic;

namespace SimpleTask.Models
{
    /// <summary>
    /// The Classroom entity holds the data for each individual classroom
    /// </summary>
    public class Classroom
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public Location Location { get; set; }
    }
}
