(function() {
    'use strict';

    angular
        .module('app')
        .controller('registerController', registerController);

    registerController.$inject = ['$state', 'authService', '$rootScope', '$http'];

    function registerController($state, authService, $rootScope, $http) {
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
            vm.message = response;
        }

        function error(rejection) {
            for (var i = 0; i < rejection.data.modelState["userModel.Username"].length; i++) {
                vm.usernameMessages.push(rejection.data.modelState["userModel.Username"][i]);
            }

            for (var i = 0; i < rejection.data.modelState["userModel.Password"].length; i++) {
                vm.passwordMessages.push(rejection.data.modelState["userModel.Password"][i]);
            }

            for (var i = 0; i < rejection.data.modelState["userModel.ConfirmPassword"].length; i++) {
                vm.passwordMessages.push(rejection.data.modelState["userModel.ConfirmPassword"][i]);
            }
            //vm.usernameMessages = rejection.data.modelState["userModel.Username"];
            //vm.passwordMessages = rejection.data.modelState["userModel.Password"];
        }
    };
})();