using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindDistance.Core.Models;


namespace FindDistance.Core
{
    public class DistanceCalculator : IDistanceCalculator
    {
        private int _matrixSize = 0;
        private int[,] _distanceMatrix;
        private int[] _distances;
        private int[] _shortestPaths;

        private void InitializeCalculator(int matrixSize, int[,] paramArray)
        {
            _distanceMatrix = new int[matrixSize, matrixSize];
            _distances = new int[matrixSize];
            _shortestPaths = new int[matrixSize];
            _matrixSize = matrixSize;

            for (int i = 0; i < _matrixSize; i++)
            {
                for (int j = 0; j < _matrixSize; j++)
                {
                    _distanceMatrix[i, j] = paramArray[i, j];
                }
            }

            for (int i = 0; i < _matrixSize; i++)
            {
                _distances[i] = i;
            }

            _distances[0] = -1;

            for (int i = 1; i < _matrixSize; i++)
            {
                _shortestPaths[i] = _distanceMatrix[0, i];
            }
        }
        private void ProcessShortestPaths()
        {
            int minValue = Int32.MaxValue;
            int minNode = 0;
            for (int i = 0; i < _matrixSize; i++)
            {
                if (_distances[i] == -1)
                    continue;
                if (_shortestPaths[i] > 0 && _shortestPaths[i] < minValue)
                {
                    minValue = _shortestPaths[i];
                    minNode = i;
                }
            }
            _distances[minNode] = -1;
            for (int i = 0; i < _matrixSize; i++)
            {
                if (_distanceMatrix[minNode, i] < 0)
                    continue;
                if (_shortestPaths[i] < 0)
                {
                    _shortestPaths[i] = minValue + _distanceMatrix[minNode, i];
                    continue;
                }
                if ((_shortestPaths[minNode] + _distanceMatrix[minNode, i]) < _shortestPaths[i])
                    _shortestPaths[i] = minValue + _distanceMatrix[minNode, i];
            }
        }

        public int[] GetShortestPaths(int[,] distanceMatrix)
        {
            try
            {
                InitializeCalculator((int)Math.Sqrt(distanceMatrix.Length), distanceMatrix);

                for (int matrixIndex = 1; matrixIndex < _matrixSize; matrixIndex++)
                {
                    ProcessShortestPaths();
                }
                return _shortestPaths;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RouteInfo> GetShortestPath(string startPoint, string endPoint)
        {
            try
            {
                List<RouteInfo> possiblePaths = JourneyPlanner(startPoint, endPoint, 0);
                var shortestDistance = possiblePaths.Select(x => x.Distance).Min();
                return possiblePaths.Where(x => x.Distance == shortestDistance).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetDistance(string routeStops)
        {
            try
            {
                string[] routes = new string[] { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" };
                List<RouteInfo> routeList = new List<RouteInfo>();
                int distance = 0;

                foreach (var route in routes)
                {
                    routeList.Add(new RouteInfo
                    {
                        Route = route.Substring(0, 2),
                        Distance =  Convert.ToInt32(route.Substring(2, route.Length - 2))
                    });
                }

                string[] stops = routeStops.Split('-');
                string[] plannedRoutes = new string[stops.Count() - 1];
                int j = 0;
                for (int i = 0; i < stops.Count() - 1; i++)
                {
                    j = i;
                    plannedRoutes[i] = stops[i] + stops[++j];
                }

                foreach (var plannedRoute in plannedRoutes)
                {
                    var d = routeList.Where(x => x.Route == plannedRoute).Select(x => x.Distance).SingleOrDefault();

                    if (d == 0)
                    {
                        throw new Exception($"{plannedRoute} is not a valid route");
                    }

                    distance += Convert.ToInt32(d);
                }
                return distance;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<RouteInfo> JourneyPlanner(string startPoint, string endPoint, int maximumStop)
        {
            try
            {
                string[] routes = new string[] { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" };
                //string[] routes = new string[] { "BC4", "CD8", "DE6", "EC2", "EB3" };
                bool validateuserRquestedStops = false;

                List<RouteInfo> routeList = new List<RouteInfo>();
                List<string> possibleRoutes = new List<string>();
                List<RouteInfo> possibleRouteInfo = new List<RouteInfo>();
                string routeTmp = string.Empty;


                foreach (var route in routes)
                {
                    routeList.Add(new RouteInfo
                    {
                        Route = route.Substring(0, 2),
                        Distance =  Convert.ToInt32(route.Substring(2, route.Length - 2))
                    });
                }
                possibleRoutes = MakeTheJourneyPlan(routeList, maximumStop, startPoint: startPoint, endPoint: endPoint);

                foreach (var possibleRoute in possibleRoutes)
                {
                    char[] routeArr = possibleRoute.ToCharArray();
                    char[] validRoutesArr = new char[possibleRoute.Length];
                    //validate maximum stops.If maximumStop==0 , then maximumStop condition doesnt apply.
                    // maximumStop set to 0 when we look for shortest path
                    if (maximumStop != 0)
                    {
                        validateuserRquestedStops = true;
                        if (routeArr.Length <= maximumStop)
                        {
                            validRoutesArr = routeArr;
                        }
                    }
                    else
                    {
                        validRoutesArr = routeArr;
                    }



                    for (int i = 0; i < validRoutesArr.Count(); i++)
                    {
                        //Process the format
                        if (i == 0)
                        {
                            routeTmp = routeArr[i].ToString();
                        }
                        else
                        {
                            routeTmp = $"{routeTmp}-{routeArr[i]}";
                        }
                    }


                    possibleRouteInfo.Add(new RouteInfo
                    {
                        Route = routeTmp,
                        Distance = GetDistance(routeTmp)
                    });
                }

                return possibleRouteInfo;

            }
            catch (Exception)
            {
                throw;
            }
        }


        private List<string> MakeTheJourneyPlan(List<RouteInfo> allRoutes, int maximumRoutes, int numberOfStops = 1, string startPoint = "", string endPoint = "", List<string> paths = null)
        {
            string tmpPath = string.Empty;
            List<string> tmpRouteList = new List<string>();
            List<string> rs = new List<string>();

            foreach (var s in allRoutes.Select(x => x.Route))
            {
                tmpRouteList = GeneratePath(s, allRoutes, endPoint, rs);
            }


            return tmpRouteList;
        }

        private List<string> GeneratePath(string paths, List<RouteInfo> allRoutes, string endPoint, List<string> possibleRoutes, int searchCount = 0)
        {
            string lastStopofPath = paths.Substring(paths.Length - 1, 1);

            if (searchCount != allRoutes.Count)
            {
                if (lastStopofPath != endPoint)
                {
                    for (int i = 0; i < allRoutes.Count; i++)
                    {
                        string y = allRoutes[i].Route.Substring(0, 1);
                        if (lastStopofPath == y)
                        {
                            searchCount++;
                            paths = paths + allRoutes[i].Route.Substring(1, 1);
                            return GeneratePath(paths, allRoutes, endPoint, possibleRoutes, searchCount: searchCount);
                        }
                    }
                }
                else
                {
                    possibleRoutes.Add(paths);
                }
            }
            return possibleRoutes;
        }
    }
}
