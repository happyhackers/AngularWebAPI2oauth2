(function () {
    'use strict';

    angular.module('app')
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        console.log("App Config");
        $urlRouterProvider.otherwise('/home');

        $stateProvider
            .state('login', {
                url: '/login',
                templateUrl: '/Scripts/app/templates/login.html',
                controller: 'loginController',
                controllerAs: 'vm',
                data: {}
            })
            .state("logout", {
                url: '/logout',
                templateUrl: '/Scripts/app/templates/logout.html',
                controller: 'logoutController',
                controllerAs: 'vm',
                data: {}
            })
            .state('authed', {
                url: '',
                'abstract': true,
                data: { authed: true },
                template: '<div data-ui-view></div>',
                controllerAs: 'vm'
            })
            .state("accessdenied", {
                parent: 'authed',
                url: '/denied',
                data: { authed: true },
                templateUrl: 'Scripts/app/templates/accessdenied.html'
            })
            .state('home', {
                parent: 'authed',
                url: '/home',
                data: { authed: true },
                templateUrl: '/Scripts/app/templates/home.html',
                controller: 'homeController',
                controllerAs: 'vm'
            })
            .state('register', {
                parent: 'authed',
                url: '/register',
                data: { authed: true },
                templateUrl: '/Scripts/app/templates/register.html',
                controller: 'registerController',
                controllerAs: 'vm'
            })
            .state('account', {
                parent: 'authed',
                url: '/account',
                data: { authed: true },
                templateUrl: '/Scripts/app/templates/account.html',
                controller: 'accountController',
                controllerAs: 'vm'
            });
    };
})();