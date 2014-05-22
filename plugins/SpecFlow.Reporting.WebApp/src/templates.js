angular.module('app').run(['$templateCache', function($templateCache) {
  'use strict';

  $templateCache.put('directives/result-badge.tpl.html',
    "<span class=\"label pull-right\" ng-class=\"{ 'label-success': result == 'OK', 'label-danger': result == 'Error', 'label-warning': result == 'Pending', 'label-default': result == 'NotRun' }\">{{result}}</span>"
  );


  $templateCache.put('views/dashboard.tpl.html',
    "<div ng-controller=\"DashboardController\"><nav class=\"navbar navbar-default navbar-fixed-top\" role=\"navigation\"><div class=\"container\"><!-- Brand and toggle get grouped for better mobile display --><div class=\"navbar-header\"><a class=\"navbar-brand\" href=\"#\">__TITLE__</a></div></div></nav><div class=\"container\">__DASHBOARD_TEXT__<h1>Features</h1><accordion close-others=\"oneAtATime\"><accordion-group ng-repeat=\"feature in report.features\" is-open=\"feature.$$isOpen\"><accordion-heading>{{feature.title}} <small class=\"text-muted\"><em>completed in {{feature.duration}} ms</em></small><result-badge result=\"feature.result\"></result-badge></accordion-heading><div ng-bind-html=\"feature.description_html | trusted\"></div><h3>Scenarios</h3><div class=\"list-group\"><a ng-repeat=\"scenario in feature.scenarios\" class=\"list-group-item\" href=\"#/features/{{feature.id}}/scenarios/{{scenario.id}}\">{{scenario.title}}<small class=\"text-muted\"><em>completed in {{scenario.duration}} ms</em></small><result-badge result=\"feature.result\"></result-badge></a></div></accordion-group></accordion></div></div>"
  );


  $templateCache.put('views/scenario.tpl.html',
    "<div ng-controller=\"ScenarioController\"><nav class=\"navbar navbar-default navbar-fixed-top\" role=\"navigation\"><div class=\"container-fluid\"><!-- Collect the nav links, forms, and other content for toggling --><div class=\"navbar-left\"><div class=\"navbar-left\"><a href=\"#/dashboard/feature/{{feature.id}}\" role=\"button\" class=\"btn btn-default navbar-btn\"><span class=\"glyphicon glyphicon-chevron-left\"></span>Back</a></div></div><div class=\"collapse navbar-collapse text-center\"><p class=\"navbar-text\">{{feature.title}}</p></div></div></nav><div class=\"container\"><h4>Scenario: {{scenario.title}}</h4><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">Given</h3></div><div class=\"panel-body\"><ul class=\"list-group\"><li class=\"list-group-item step-details\" ng-include=\"'step-details'\" ng-repeat=\"step in scenario.given.steps\"></li></ul></div></div><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">When</h3></div><div class=\"panel-body\"><ul class=\"list-group\"><li class=\"list-group-item step-details\" ng-include=\"'step-details'\" ng-repeat=\"step in scenario.when.steps\"></li></ul></div></div><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">Then</h3></div><div class=\"panel-body\"><ul class=\"list-group\"><li class=\"list-group-item step-details\" ng-include=\"'step-details'\" ng-repeat=\"step in scenario.then.steps\"></li></ul></div></div></div><script id=\"step-details\" type=\"text/ng-template\"><!-- step-details-marker: begin -->\r" +
    "\n" +
    "\t\t{{step.title}} <small class=\"text-muted\"><em>completed in {{step.duration}} ms</em></small>\r" +
    "\n" +
    "        <result-badge result=\"step.result\"></result-badge>\r" +
    "\n" +
    "\r" +
    "\n" +
    "\t\t<!-- step-details-marker: end --></script></div>"
  );

}]);
