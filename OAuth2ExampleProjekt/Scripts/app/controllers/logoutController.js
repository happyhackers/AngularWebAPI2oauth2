(function () {
    'use strict';

    angular
        .module('app')
        .controller('logoutController', logoutController);

    logoutController.$inject = ['$scope', '$timeout', '$state', '$interval', 'authService'];

    function logoutController($scope, $timeout, $state, $interval, authService) {
        /* jshint validthis:true */
        var vm = this;
        var startTimer;
        vm.redirectTimer = 5;

        activate();

        function activate() {
            authService.logOut();
            startTimer = $interval(function () {
                vm.redirectTimer--;
                if (vm.redirectTimer == 0)
                    $state.go('login');
            }, 1000);
        }
        

        $scope.$on('$destroy', function () {
            $interval.cancel(startTimer);
        });
    }
})();
