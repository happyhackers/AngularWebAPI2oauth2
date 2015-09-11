(function() {
    'use strict';

    angular
        .module('app')
        .controller('registerController', registerController);

    registerController.$inject = ['$state', '$rootScope', '$http'];

    function registerController($state, $rootScope, $http) {
        var vm = this;
        vm.registerUser = register;
        vm.message = "";
        vm.usernameMessages = [];
        vm.passwordMessages = [];

        vm.userData = { userName: "", password: "", confirmPassword: "" };

        activate();

        function activate() {}

        function register() {
            $http.post('http://localhost:38268/api/Account/Register', vm.userData).then(success, error);
        }

        function success(response) {
            vm.message = response.data;
        }

        function error(rejection) {
            var modelState = rejection.data.modelState;
            for (var errorType in modelState) {
                for (var errorNumber in modelState[errorType]) {
                    vm.passwordMessages.push(modelState[errorType][errorNumber]);
                }
            }
        }
    };
})();