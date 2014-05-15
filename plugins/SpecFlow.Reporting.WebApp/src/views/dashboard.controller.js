(function () {
	'use strict';

	angular.module('app').controller('DashboardController', [
        '$scope',
        '$routeParams',
        'report',
        function ($scope, $routeParams, report) {
        	$scope.report = report;

        	$scope.isActiveFeature = function (feature) {
        		return feature.id === $routeParams.featureId;
        	};
        }
	]);
})();