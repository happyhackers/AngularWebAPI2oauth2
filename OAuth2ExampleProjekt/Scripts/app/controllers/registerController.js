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
            vm.usernameMessages = [];
            vm.passwordMessages = [];

            $http.post('http://localhost:38268/api/Account/Register', vm.userData).then(success, error);
        }

        function success(response) {
            vm.message = response.data;
        }

        function error(rejection) {
            try {
                for (var i = 0; i < rejection.data.modelState["userModel.Password"].length; i++) {
                    vm.passwordMessages.push(rejection.data.modelState["userModel.Password"][i]);
                }
                for (var i = 0; i < rejection.data.modelState["userModel.Username"].length; i++) {
                    vm.usernameMessages.push(rejection.data.modelState["userModel.Username"][i]);
                }
                for (var i = 0; i < rejection.data.modelState["userModel.ConfirmPassword"].length; i++) {
                    vm.passwordMessages.push(rejection.data.modelState["userModel.ConfirmPassword"][i]);
                }
            } catch (e) {

            } 
        }
    };
})();