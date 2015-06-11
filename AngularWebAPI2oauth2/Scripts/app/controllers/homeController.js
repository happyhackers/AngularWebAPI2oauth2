(function () {
    'use strict';

    angular.module('app')
        .controller('homeController', homeController);

    homeController.$inject = [];

    function homeController() {
        var vm = this;

        vm.welcomeMessage = "Welcome to your new SPA";

        activate();

        function activate() {
        }
    };
})();