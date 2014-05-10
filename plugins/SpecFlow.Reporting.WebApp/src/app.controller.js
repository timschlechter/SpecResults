(function() {
    'use strict';

    angular.module('app').controller('AppController', [
        '$scope',
        function($scope) {
            $scope.report = report;

            $scope.selectFeature = function(feature) {
                $scope.selectedFeature = feature;
            };

            $scope.selectScenario = function(scenario) {
                $scope.selectedScenario = scenario;
            };

            $scope.getSteps = function(scenario) {
                return _.union(scenario.given.steps, scenario.when.steps, scenario.then.steps);
            };
        }
    ]);
})();
