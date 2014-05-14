angular.module('app').run(['$templateCache', function($templateCache) {
  'use strict';

  $templateCache.put('views/dashboard.tpl.html',
    "<div ng-controller=\"DashboardController\"><nav class=\"navbar navbar-default navbar-fixed-top\" role=\"navigation\"><div class=\"container\"><!-- Brand and toggle get grouped for better mobile display --><div class=\"navbar-header\"><a class=\"navbar-brand\" href=\"#\">__TITLE__</a></div></div></nav><div class=\"container\"><accordion close-others=\"oneAtATime\"><accordion-group ng-repeat=\"feature in report.features\" is-open=\"feature.$$isOpen\"><accordion-heading>Feature: {{feature.title}}</accordion-heading><pre>{{feature.description}}</pre><ul class=\"nav nav-pills\"><li ng-repeat=\"scenario in feature.scenarios\"><a href=\"#/features/{{feature.id}}/scenarios/{{scenario.id}}\">{{scenario.title}}</a></li></ul></accordion-group></accordion></div></div>"
  );


  $templateCache.put('views/scenario.tpl.html',
    "<div ng-controller=\"ScenarioController\"><nav class=\"navbar navbar-default navbar-fixed-top\" role=\"navigation\"><div class=\"container-fluid\"><!-- Collect the nav links, forms, and other content for toggling --><div class=\"navbar-left\"><div class=\"navbar-left\"><a href=\"#/dashboard/feature/{{feature.id}}\" role=\"button\" class=\"btn btn-default navbar-btn\"><span class=\"glyphicon glyphicon-chevron-left\"></span> Back</a></div></div><div class=\"collapse navbar-collapse text-center\"><p class=\"navbar-text\">{{feature.title}}</p></div></div></nav><div class=\"container\"><h4>Scenario: {{scenario.title}}</h4><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">Given</h3></div><div class=\"panel-body\"><div class=\"step-details\" ng-include=\"'step-details'\" ng-repeat=\"step in scenario.given.steps\"></div></div></div><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">When</h3></div><div class=\"panel-body\"><div class=\"step-details\" ng-include=\"'step-details'\" ng-repeat=\"step in scenario.when.steps\"></div></div></div><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">Then</h3></div><div class=\"panel-body\"><div class=\"step-details\" ng-include=\"'step-details'\" ng-repeat=\"step in scenario.then.steps\"></div></div></div></div><script id=\"step-details\" type=\"text/ng-template\"><figure ng-if=\"step.user_data.screenshot\">\r" +
    "\n" +
    "            <div class=\"screenshot-container\">\r" +
    "\n" +
    "                <img class=\"screenshot\" alt=\"{{step.title}}\" title=\"{{step.title}}\" ng-src=\"{{step.user_data.screenshot}}\"></img>\r" +
    "\n" +
    "            </div>\r" +
    "\n" +
    "            <figcaption class=\"text-muted\">{{step.title}}</figcaption>\r" +
    "\n" +
    "        </figure></script></div>"
  );

}]);
