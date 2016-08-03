using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.PensionAgency.Models
{
    public class BookMealInfo
    {
        public string OldManId { get; set; }
        public string OldManName { get; set; }
        public string BookDate { get; set; }
        public string MealType { get; set; }
        public string MealTypeName { get; set; }
        public string SetMealId { get; set; }
        public string SetMealName { get; set; }
        public string SetMealContent { get; set; }
        public string FetchFlag { get; set; } 
    }

}
