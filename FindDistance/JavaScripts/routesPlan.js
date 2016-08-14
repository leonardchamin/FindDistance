routes.factory('routesPlanService', function ($http) {
    var routesPlanInstance = {};

    routesPlanInstance.GetDistanceForRoutes = function (orderOfStops) {
        var params = { routePlan: orderOfStops };
        var method = 'GET';
        var url = '/Home/GetDistanceForRoutePlan';

        return $http({
            method: method,
            url: url,
            params: params
        });
    };

    routesPlanInstance.GetJourneyPlans = function (startPoint, endPoint, numberOfStops) {
        var params = { startPoint: startPoint, endPoint: endPoint, numberOfStops: numberOfStops };
        var method = 'GET';
        var url = '/Home/GetJourneyPlans';

        return $http({
            method: method,
            url: url,
            params: params
        });
    };

    routesPlanInstance.GetShortestJourneyPlan = function (orderOfStops) {
        var params = { routePlan: orderOfStops };
        var method = 'GET';
        var url = '/Home/GetShortestDistanceForRoutePlan';

        return $http({
            method: method,
            url: url,
            params: params
        });
    };

    return routesPlanInstance;
})