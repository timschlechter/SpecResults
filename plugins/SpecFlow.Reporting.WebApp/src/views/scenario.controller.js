(function () {
	'use strict';

	angular.module('app').controller('ScenarioController', [
        '$scope',
        '$routeParams',
        'report',
        function ($scope, $routeParams, report) {
        	$scope.feature = report.findFeatureById($routeParams.featureId);
        	$scope.scenario = report.findScenarioById($routeParams.scenarioId);
        }
	]);
})();