(function(angular) {
	'use strict';
	angular.module('app').config([
		'$routeProvider',
		function($routeProvider) {
			$routeProvider.
				when('/dashboard', {
					templateUrl: 'views/dashboard.tpl.html'
				}).
				when('/dashboard/feature/:featureId', {
					templateUrl: 'views/dashboard.tpl.html'
				}).
				when('/features/:featureId/scenarios/:scenarioId', {
					templateUrl: 'views/scenario.tpl.html'
				}).
				otherwise({
					redirectTo: '/dashboard'
				});
		}
	]);
})(this.angular);