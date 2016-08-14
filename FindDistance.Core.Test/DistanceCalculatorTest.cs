using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindDistance.Core;
using FindDistance.Core.Models;

namespace FindDistance.Core.Test
{
    [TestClass]
    public class DistanceCalculatorTest
    {
        [TestMethod]
        public void GetShortestPathsAtoETest()
        {
            //AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7

            int[,] distanceMatrix ={
                {-1,  5, -1, 5, 7},
                {5,  -1, 4, -1, 3},
                {-1,  4, -1, 8, 2},
                {5,  -1, 8, -1, 6},
                {7,  3, 2, 6, -1}
            };

            IDistanceCalculator distanceCalculator = new DistanceCalculator();
            var result = distanceCalculator.GetShortestPaths(distanceMatrix);
            //AA = , AB = 5 , AC = 9 , AD = 5 , AE = 7
            Assert.AreEqual("0,5,9,5,7", string.Join(",", result));
        }

        [TestMethod]
        public void GetShortestPathsAtoEFailTest()
        {
            //AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7

            int[,] distanceMatrix ={
                {-1,  5, -1, 5, 7},
                {5,  -1, 4, -1, 3},
                {-1,  4, -1, 8, 2},
                {5,  -1, 8, -1, 6},
                {7,  3, 2, 6, -1}
            };

            IDistanceCalculator distanceCalculator = new DistanceCalculator();
            var result = distanceCalculator.GetShortestPaths(distanceMatrix);
            //AA = , AB = 5 , AC = 9 , AD = 5 , AE = 7
            Assert.AreNotEqual("5,9,5,7", string.Join(",", result));
        }


        [TestMethod]
        public void GetShortestPathsAtoCTest()
        {
            //AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7

            int[,] distanceMatrix ={
                {-1,  5, -1},
                {5,  -1, 4},
                {-1,  4, -1}
            };

            IDistanceCalculator distanceCalculator = new DistanceCalculator();
            var result = distanceCalculator.GetShortestPaths(distanceMatrix);
            //AA = , AB = 5 , AC = 9 
            Assert.AreEqual("0,5,9", string.Join(",", result));
        }

        [TestMethod]
        public void GetDistance()
        {
            IDistanceCalculator distanceCalculator = new DistanceCalculator();
            var d = distanceCalculator.GetDistance("A-D-C");
            Assert.AreEqual(13, d);
        }

        
        [TestMethod]
        public void GetPossiblePaths()
        {
            bool isResultsMatch = false;
            IDistanceCalculator distanceCalculator = new DistanceCalculator();
            List<RouteInfo> resutList = distanceCalculator.JourneyPlanner("C", "C", 3);
            
            List<RouteInfo> expectedList = new List<RouteInfo>();
            expectedList.Add(new RouteInfo()
            {
                Route = "C-D-C",
                Distance = 16
            });


            foreach (var result in resutList)
            {
                if (!expectedList.Exists(x => x.Route == result.Route && x.Distance == result.Distance))
                {
                    isResultsMatch = false;
                    break;
                }
                isResultsMatch = true;
            }
            Assert.IsTrue(isResultsMatch);
        }


        [TestMethod]
        public void GetShortestPath()
        {
            bool isResultsMatch = false;
            IDistanceCalculator distanceCalculator = new DistanceCalculator();
            var resuList = distanceCalculator.GetShortestPath("C", "C");

            List<RouteInfo> expectedList = new List<RouteInfo>();

            expectedList.Add(new RouteInfo()
            {
                Route = "B-C-E-B",
                Distance = 7
            });

            foreach (var result in resuList)
            {
                if (!expectedList.Exists(x => x.Route == result.Route && x.Distance == result.Distance))
                {
                    isResultsMatch = false;
                    break;
                }
                isResultsMatch = true;
            }
            Assert.IsTrue(isResultsMatch);
        }

    }
}
