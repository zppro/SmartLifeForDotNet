using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartLife.Client.PensionAgency.Models;

namespace SmartLife.Client.PensionAgency.SelfService
{
    public static class Data
    {
        public static List<OldManInfo> OldMans { get; set; }
        public static List<BookMealInfo> BookMeals { get; set; }
        public static List<CourseInfo> Courses { get; set; }
    }
}
