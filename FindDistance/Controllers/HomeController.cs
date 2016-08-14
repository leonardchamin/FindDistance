using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindDistance.Core;
using FindDistance.ViewModels;

namespace FindDistance.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistanceCalculator _distanceCalculator;
        public HomeController(IDistanceCalculator distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetDistanceForRoutePlan(string routePlan)
        {
            try
            {
                int distance = _distanceCalculator.GetDistance(routePlan);
                return Json(distance, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
           
        }


        public JsonResult GetJourneyPlans(string startPoint, string endPoint, string numberOfStops)
        {
            var routePlan = _distanceCalculator.JourneyPlanner(startPoint, endPoint, Convert.ToInt32(numberOfStops));
            var j = Json(routePlan, JsonRequestBehavior.AllowGet);
            return j;
        }

        public JsonResult GetShortestDistanceForRoutePlan(string routePlan)
        {
            string[] stops = routePlan.Split('-');
            var distance = _distanceCalculator.GetShortestPath(stops[0], stops[1]).Single();
            return Json(distance.Distance, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetDistances(RoutePlannerViewModel model)
        {
            try
            {
                //_distanceCalculator.GetShortestPaths()
                return View();
            }
            catch (Exception ex)
            {
                //write error log ex
                return View(@"~\Views\Shared\_ErrorPage.cshtml");
            }
        }
    }
}