(function() {
    'use strict';

    angular.module('app').factory('report', [
        function($scope, $routeParams) {
            // Return the report object from the global scope

            function ReportService(report) {
                this.report = report;

                this.features = report.features;

                _.forEach(this.report.features, function(feature, index) {
                    feature.id = 'f' + index;

                    _.forEach(feature.scenarios, function(scenario, index) {
                        scenario.id = feature.id + 's' + index;

                        _.forEach(scenario.given.steps, function(step, index) {
                            step.id = scenario.id + 'g' + index;
                        });

                        _.forEach(scenario.when.steps, function(step, index) {
                            step.id = scenario.id + 'w' + index;
                        });

                        _.forEach(scenario.then.steps, function(step, index) {
                            step.id = scenario.id + 't' + index;
                        });
                    });
                });

            }

            ReportService.prototype = {
                constructor: ReportService,

                findFeatureById: function(id) {
                    id = id.substring(0, 2);
                    return _.find(this.report.features, function(feature) {
                        return feature.id === id;
                    });
                },

                findScenarioById: function(id) {
					id = id.substring(0, 4);
                    var feature = this.findFeatureById(id);
                    return _.find(feature.scenarios, function(scenario) {
                        return scenario.id === id;
                    });
                }
            };

            return new ReportService(report);
        }
    ]);
})();
