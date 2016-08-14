using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindDistance.Core.Models;

namespace FindDistance.Core
{
    public interface IDistanceCalculator
    {
        int[] GetShortestPaths(int[,] distanceMatrix);
        int GetDistance(string routeStops);
        List<RouteInfo> JourneyPlanner(string startPoint, string endPoint, int maximumStop);
        List<RouteInfo> GetShortestPath(string startPoint, string endPoint);
    }
}
