using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TempPacking.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Salary { get; set; }
        public string Duration { get; set; }
        public JobCategory Category { get; set; }
    }

    public enum JobCategory
    {
        Building = 1,
        Hospitality = 2,
        IT = 3
    }
}