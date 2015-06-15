(function () {
    'use strict';

    angular
        .module('app')
        .controller('loginController', loginController);

    loginController.$inject = ['$state', 'authService', '$rootScope'];

    function loginController($state, authService, $rootScope) {
        /* jshint validthis:true */
        var vm = this;
        vm.login = login;
        vm.loginData = { userName: "", password: "", useRefreshTokens: false };
        vm.message = "";

        activate();

        function activate() {
        }

        function login() {

            authService.login(vm.loginData).then(function (response) {
                if ($rootScope.returnToState)
                    $state.go($rootScope.returnToState, $rootScope.returnToStateParams);
                else
                    $state.go('home');
            },
             function (err) {
                 vm.message = err.error_description;
             });
        };
    }
})();
