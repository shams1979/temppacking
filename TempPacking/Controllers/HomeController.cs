using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;
using TempPacking.Data;
using TempPacking.Models;

namespace TempPacking.Controllers
{
    public class HomeController : Controller
    {
        private readonly JobRepo jobRepo;

        public HomeController()
        {
            jobRepo = new JobRepo();
        }

        public IList<ItineraryStep> Itinerary
        {
            get
            {
                if (Session["Itinerary"] == null)
                {
                    Session["Itinerary"] = new List<ItineraryStep>();
                }

                return Session["Itinerary"] as List<ItineraryStep>;
            }
            
        }

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult FindSkill(string query)
        {
            if (query.Length < 3)
                return Json(false, JsonRequestBehavior.AllowGet);

            var data = SkilRepo.GetSkills().Where(x => x.ToLower().StartsWith(query.ToLower())).Select(x => x).ToList();
            
           return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetJobs(string city, double longitude, double latitude, string startDate, string endDate, string skills)
        {
            DateTime parsedStartDate;
            var parsedStart = DateTime.TryParse(startDate, out parsedStartDate);
            DateTime parsedEndDate;
            var parsedEnd = DateTime.TryParse(endDate, out parsedEndDate);

            //if(!parsedStart || !parsedEnd)
            //    return Json(new { success = false, error = "Invalid start or end date." });

            //if (parsedEndDate <= parsedStartDate)
            //    return Json(new { success = false, error = "End date cannot be greater than start date" });

            if(string.IsNullOrEmpty(skills))
                return Json(new { success = false, error = "Please enter at least one skill" });

            var splitSkills = skills.Split(',');
            
            if(splitSkills.Length <= 0)
                return Json(new { success = false, error = "Please enter at least one skill" });

            var skillObjs = new List<Skill>();

            foreach(var s in splitSkills)
                skillObjs.Add(new Skill{name = s});

            //if (Itinerary.Any())
            //{
            //    var maxDate = Itinerary.Max(x => x.EndDate);

            //    if (parsedEndDate < maxDate || parsedStartDate < maxDate)
            //        return Json(new { success = false, error = "Your dates can't be used with the current itinerary." });
            //}

            var jobs = jobRepo.GetJobs(longitude, latitude, skillObjs);

            var itineraryStep = new ItineraryStep
                                    {
                                        City = city,
                                        Skills = skillObjs,
                                        Latitude = latitude,
                                        Longitude = longitude,
                                        StartDate = parsedStartDate,
                                        EndDate = parsedEndDate,
                                        Jobs = jobs
                                    };

            Itinerary.Add(itineraryStep);

            return Json(new {success = true, itinerary = JsonConvert.SerializeObject(Itinerary)});
        }
    }

    public class ItineraryStep
    {
        public string City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Job> Jobs { get; set; }
        public IList<Skill> Skills { get; set; } 
    }

    public class Skill
    {
        public string name { get; set; }
    }
}
