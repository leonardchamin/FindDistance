var routes = angular.module('FrogParking', []);
routes.controller('routesPlanController', ['$scope', '$http', '$templateCache', 'routesPlanService', function ($scope, $http, $templateCache, routesPlanService) {
    var vm = $scope;

    vm.numbersOnly = /^\d+$/;
    vm.routeModel = {};
    vm.routeModel = { routePlan: "", startPoint: "", endPoint: "", numberOfMaxStops: "", routePlanShortest:"" };

    vm.init = function (profile) {
        vm.model = profile;
    }

    vm.routeList = [];
    vm.routesPlanService = {};
    vm.distanceForRoutes = {};
    vm.distanceForRoutes = "";
    vm.distanceForShortestRoute = {};
    vm.distanceForShortestRoute = "";

    vm.GetDistanceForRoutes = function (stopsForRoutePlan) {
        routesPlanService.GetDistanceForRoutes(vm.routeModel.routePlan)
            .then(function (response) {
                vm.distanceForRoutes = response.data;
            }, function (response) {
                alert("Error occured.System cannot retrieve data");
            });
    };


    vm.GetUserJourneyPlans = function () {
        routesPlanService.GetJourneyPlans(vm.routeModel.startPoint, vm.routeModel.endPoint, vm.routeModel.numberOfMaxStops)
            .then(function (response) {

                angular.forEach(response.data, function (route) {
                    vm.routeList.push(route);
                });
            }, function (response) {
                alert("Error occured.System cannot retrieve data");
            });
    };


    vm.GetShortestJourneyPlan = function () {
        routesPlanService.GetShortestJourneyPlan(vm.routeModel.routePlanShortest)
            .then(function (response) {
                vm.distanceForShortestRoute = response.data;
            }, function (response) {
                alert("Error occured.System cannot retrieve data");
            });
    };
}
]);

routes.directive("keyPress", function () {
    return {
        link: function (scop, ele, attr) {

            angular.element(ele).on("keypress", function () {
                alert("Numbers Only");
            });
        }
    };
});