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
            vm.message = "Password was successfully set";
        }

        function error(rejection) {
            try {
                for (var i = 0; i < rejection.data.modelState["model.NewPassword"].length; i++) {
                    vm.passwordMessages.push(rejection.data.modelState["model.NewPassword"][i]);
                }
            } catch (e) {

            }

            try {
                
                for (var j = 0; j < rejection.data.modelState["model.ConfirmPassword"].length; j++) {
                    vm.passwordMessages.push(rejection.data.modelState["model.ConfirmPassword"][j]);
                }
            } catch (e) {

            }

            try {

                for (var j = 0; j < rejection.data.modelState["model.Password"].length; j++) {
                    vm.passwordMessages.push(rejection.data.modelState["model.Password"][j]);
                }
            } catch (e) {
                
            }
            
        }
    };
})();