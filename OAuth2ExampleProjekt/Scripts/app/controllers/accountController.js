(function () {
    'use strict';

    angular
        .module('app')
        .controller('accountController', accountController);

    accountController.$inject = ['$state', '$rootScope', '$http'];

    function accountController($state, $rootScope, $http) {
        var vm = this;
        vm.changePassword = changePassword;
        vm.message = "";
        vm.passwordMessages = [];

        vm.newPassword = { oldPassword: "", newPassword: "", confirmPassword: "" };

        activate();

        function activate() { }

        function changePassword() {
            $http.put('http://localhost:38268/api/Account/ChangePassword', vm.newPassword).then(success, error);
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