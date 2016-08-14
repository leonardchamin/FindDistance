using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace FindDistance.ViewModels
{
    public class RoutePlannerViewModel
    {
        [Display(Name = "Enter your journey plan")]
        public string RoutePlan { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string NumberOfMaxStops { get; set; }
    }
}
