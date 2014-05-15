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
                    feature.duration = getDuration(feature);

                    _.forEach(feature.scenarios, function(scenario, index) {
                        scenario.id = feature.id + 's' + index;
                        scenario.duration = getDuration(scenario);

                        scenario.given.duration = getDuration(scenario.given);
                        _.forEach(scenario.given.steps, function(step, index) {
                            step.id = scenario.id + 'g' + index;
                            step.duration = getDuration(step);
                        });

                        scenario.when.duration = getDuration(scenario.when);
                        _.forEach(scenario.when.steps, function(step, index) {
                            step.id = scenario.id + 'w' + index;
                            step.duration = getDuration(step);
                        });

                        scenario.then.duration = getDuration(scenario.then);
                        _.forEach(scenario.then.steps, function(step, index) {
                            step.id = scenario.id + 't' + index;
                            step.duration = getDuration(step);
                        });
                    });
                });
            }

            function getDuration(item) {
                return new Date(item.end_time).getTime() - new Date(item.start_time).getTime();
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