(function (angular) {
    'use strict';

    angular.module('app').directive('resultBadge', [function () {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: 'directives/result-badge.tpl.html',
            scope: {
                'result': '='
            },
            link: function ($scope, element, attrs) {
               
            }
        };
    }]);
})(this.angular);


