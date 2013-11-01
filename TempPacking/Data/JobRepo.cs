using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TempPacking.Models;

namespace TempPacking.Data
{
    public class JobRepo
    {
        private List<Job> jobs { get; set; }

        public JobRepo()
        {
            jobs = new List<Job>();
            jobs.Add(new Job()
                         {
                             Category = JobCategory.Building,
                             Description = "",
                             Duration = "15 days",
                             Id = 1,
                             Salary = "£10/hr",
                             Title = "Electrician needed"
                         });

            jobs.Add(new Job()
            {
                Category = JobCategory.Hospitality,
                Description = "",
                Duration = "13 days",
                Id = 2,
                Salary = "£9/hr",
                Title = "Waiter urgently needed"
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.IT,
                Description = "",
                Duration = "2 weeks",
                Id = 3,
                Salary = "£15/hr",
                Title = "SQL Server developer needed"
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.Building,
                Description = "",
                Duration = "1 week",
                Id = 4,
                Salary = "£6/hr",
                Title = "Bricklayer required"
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.IT,
                Description = "",
                Duration = "3 weeks",
                Id = 1,
                Salary = "£50/hr",
                Title = "Developer for Reed"
            });
        }

        //public IEnumerable<Job> GetJobs(float longitude, float latitude)
        //{
        //    var jobs = new List<Job>();

        //    var rnd = new Random();

        //    var jobsCount = rnd.Next(5, 11);

        //    for (var i = 0; i < jobsCount; i++)
        //    {
        //        var job = jobs[rnd.Next(0, jobs.Count)];



        //    }
        //}
    }
}