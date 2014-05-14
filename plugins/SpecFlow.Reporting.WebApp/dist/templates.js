angular.module('app').run(['$templateCache', function($templateCache) {
  'use strict';

  $templateCache.put('views/dashboard.tpl.html',
    "<div ng-if=\"selectedScenario\" class=\"row\" ng-controller=\"DashboardController\"><div class=\"col-md-3 sidebar\"><div class=\"alert alert-info feature\"><h4>Feature: {{selectedFeature.title}}</h4>{{selectedFeature.description}}</div></div><div class=\"col-md-9\"><div class=\"bs-docs-sidebar scenario-summary\"><h3>Scenario: {{selectedScenario.title}}</h3><ul class=\"nav bs-docs-sidenav\"><li ng-repeat=\"step in selectedScenario.given.steps\"><a href=\"#step_{{$index}}\" class=\"row\"><span class=\"blocktype col-md-1\"><span ng-if=\"$index==0\">Given</span></span> <span class=\"col-md-11\"><span ng-if=\"$index > 0\" class=\"blocktype\">and</span> {{step.title}}</span></a></li><li ng-repeat=\"step in selectedScenario.when.steps\"><a href=\"#step_{{$index}}\" class=\"row\"><span class=\"blocktype col-md-1\"><span ng-if=\"$index==0\">When</span></span> <span class=\"col-md-11\"><span ng-if=\"$index > 0\" class=\"blocktype\">and</span> {{step.title}}</span></a></li><li ng-repeat=\"step in selectedScenario.then.steps\"><a href=\"#step_{{$index}}\" class=\"row\"><span class=\"blocktype col-md-1\"><span ng-if=\"$index==0\">Then</span></span> <span class=\"col-md-11\"><span ng-if=\"$index > 0\" class=\"blocktype\">and</span> {{step.title}}</span></a></li></ul></div></div></div>"
  );

}]);
