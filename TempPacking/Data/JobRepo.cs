using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TempPacking.Controllers;
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
                             Title = "Electrician needed",
                             Skills = new List<Skill>{  new Skill{name = "Bricklayer"},
                                               new Skill{name = "Plumber"},
                                               new Skill{name = "Electrician"},
                                               new Skill{name = "Builder"}}
                         });

            jobs.Add(new Job()
            {
                Category = JobCategory.Hospitality,
                Description = "",
                Duration = "13 days",
                Id = 2,
                Salary = "£9/hr",
                Title = "Waiter urgently needed",
                Skills = new List<Skill> {  new Skill{name = "Waiter"},
                                               new Skill{name = "Barman"},
                                               new Skill{name = "Receptionist" }}
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.IT,
                Description = "",
                Duration = "2 weeks",
                Id = 3,
                Salary = "£15/hr",
                Title = "SQL Server developer needed",
                Skills = new List<Skill> {  new Skill{name = "Developer"},
                                               new Skill{name = "SQL Developer"},
                                               new Skill{name = "Frontend"},
                                                new Skill{name = "C#"},
                                                new Skill{name = "Java"},
                                                new Skill{name = "PHP"} }
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.Building,
                Description = "",
                Duration = "1 week",
                Id = 4,
                Salary = "£6/hr",
                Title = "Bricklayer required",
                Skills = new List<Skill>{  new Skill{name = "Bricklayer"},
                                               new Skill{name = "Plumber"},
                                               new Skill{name = "Electrician"},
                                               new Skill{name = "Builder"}}
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.IT,
                Description = "",
                Duration = "3 weeks",
                Id = 1,
                Salary = "£50/hr",
                Title = "Developer for Reed",
                Skills = new List<Skill> {  new Skill{name = "Developer"},
                                               new Skill{name = "SQL Developer"},
                                               new Skill{name = "Frontend"},
                                                new Skill{name = "C#"},
                                                new Skill{name = "Java"},
                                                new Skill{name = "PHP"} }
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.Hospitality,
                Description = "",
                Duration = "2 weeks",
                Id = 1,
                Salary = "£62/hr",
                Title = "Hotel manager",
                Skills = new List<Skill> {  new Skill{name = "Waiter"},
                                               new Skill{name = "Barman"},
                                               new Skill{name = "Receptionist" }}
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.IT,
                Description = "",
                Duration = "4 weeks",
                Id = 1,
                Salary = "£20/hr",
                Title = "System administrator",
                Skills = new List<Skill> {  new Skill{name = "Developer"},
                                               new Skill{name = "SQL Developer"},
                                               new Skill{name = "Frontend"},
                                                new Skill{name = "C#"},
                                                new Skill{name = "Java"},
                                                new Skill{name = "PHP"} }
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.Building,
                Description = "",
                Duration = "6 weeks",
                Id = 1,
                Salary = "£100/hr",
                Title = "Architect",
                Skills = new List<Skill>{  new Skill{name = "Bricklayer"},
                                               new Skill{name = "Plumber"},
                                               new Skill{name = "Electrician"},
                                               new Skill{name = "Builder"}}
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.IT,
                Description = "",
                Duration = "3 weeks",
                Id = 1,
                Salary = "£15/hr",
                Title = "Tester",
                Skills = new List<Skill> {  new Skill{name = "Developer"},
                                               new Skill{name = "SQL Developer"},
                                               new Skill{name = "Frontend"},
                                                new Skill{name = "C#"},
                                                new Skill{name = "Java"},
                                                new Skill{name = "PHP"} }
            });

            jobs.Add(new Job()
            {
                Category = JobCategory.Hospitality,
                Description = "",
                Duration = "2 weeks",
                Id = 1,
                Salary = "£8/hr",
                Title = "Cleaner",
                Skills = new List<Skill> {  new Skill{name = "Waiter"},
                                               new Skill{name = "Barman"},
                                               new Skill{name = "Receptionist" }}
            });
        }

        public IEnumerable<Job> GetJobs(double longitude, double latitude, IList<Skill> skills)
        {
            var rnd = new Random();
            var results = jobs.Where(job => job.Skills.Select(x => x.name.ToLower()).Intersect(skills.Select(x => x.name.ToLower())).Any()).ToList();
            foreach (var j in results)
                j.Location = GetLocation(rnd, longitude, latitude, 16000);
            return results;
        }

        public Location GetLocation(Random random, double x0, double y0, int radius)
        {
            // Convert radius from meters to degrees
            double radiusInDegrees = radius / 111000f;

            double u = random.NextDouble();
            double v = random.NextDouble();
            double w = radiusInDegrees * Math.Sqrt(u);
            double t = 2 * Math.PI * v;
            double x = w * Math.Cos(t);
            double y = w * Math.Sin(t);

            // Adjust the x-coordinate for the shrinking of the east-west distances
            double new_x = x / Math.Cos(y0);

            double foundLongitude = new_x + x0;
            double foundLatitude = y + y0;
            return new Location {Lat = foundLatitude, Long = foundLongitude};
        }
    }
}