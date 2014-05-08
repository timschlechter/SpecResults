(function(angular) {
	'use strict';
	angular.module('app').config([
		'$routeProvider',
		function($routeProvider) {
			$routeProvider.
				when('/features', {
					templateUrl: 'views/features.tpl.html'
				}).
				otherwise({
					redirectTo: '/features'
				});
		}
	]);
})(this.angular);