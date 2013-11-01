using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TempPacking.Controllers;
using TempPacking.Data;

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
        public Location Location { get; set; }
        public IList<Skill> Skills { get; set; }

        public Job()
        {
            Skills = new List<Skill>();
        }
    }

    public enum JobCategory
    {
        Building = 1,
        Hospitality = 2,
        IT = 3
    }

    public class Location
    {
        public double Long { get; set; }
        public double Lat { get; set; }
    }
}