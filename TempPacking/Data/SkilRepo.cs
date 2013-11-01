using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TempPacking.Models;

namespace TempPacking.Data
{
    public static class SkilRepo
    {
        private static List<string> skills;

        public static List<string> GetSkills()
        {
            return skills ?? (skills = new List<string>
                                           {
                                                "Developer",
                                               "SQL Developer",
                                               "Frontend",
                                                "C#",
                                                "Java",
                                                "PHP",
                                               "Bricklayer",
                                               "Plumber",
                                               "Electrician",
                                               "Builder",
                                               "Waiter",
                                               "Barman",
                                               "Receptionist"
                                           });
        }
    }
}