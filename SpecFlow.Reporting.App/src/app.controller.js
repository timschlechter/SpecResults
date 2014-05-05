(function() {
    'use strict';

    angular.module('app').controller('AppController', [
        '$scope',
        '$http',
        function($scope, $http) {
            $http.get('TestResults/testreport.json').then(
                function(response) {
                    $scope.report = response.data;

                    $scope.selectFeature($scope.report.features[2]);
                }
            );

            $scope.selectFeature = function(feature) {
                $scope.selectedFeature = feature;
                $scope.selectScenario(feature.scenarios[0]);
            };

            $scope.selectScenario = function(scenario) {
                $scope.selectedScenario = scenario;
            };

            $scope.$watch('selectedScenario', function() {
                // Refresh scollspy
                $('[data-spy="scroll"]').each(function() {
                    var $spy = $(this).scrollspy('refresh');
                });
            });
        }
    ]);
})(this.angular);
