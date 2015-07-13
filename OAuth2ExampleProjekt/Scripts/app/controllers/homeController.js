(function () {
    'use strict';

    angular.module('app')
        .controller('homeController', homeController);

    homeController.$inject = ['authService'];

    function homeController(authService) {
        var vm = this;

        vm.welcomeMessage = "Welcome to your new SPA";
        vm.user = authService.loginData;

        activate();

        function activate() { }
    };
})();