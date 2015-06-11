(function () {
    'use strict';

    angular.module('app')
        .config(appConfig)
        .run(appRun);

    appConfig.$inject = ['$stateProvider', '$urlRouterProvider'];

    function appConfig($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/home');

        $stateProvider
            .state('home', {
                url: '/home',
                data: {},
                templateUrl: '/Scripts/app/views/home.html',
                controller: 'homeController',
                controllerAs: 'vm'
            });
    };

    appRun.$inject = ['$rootScope', '$state', '$stateParams'];

    function appRun($rootScope, $state, $stateParams) {
        $rootScope.$on('$stateChangeStart', function (event, toState, toStateParams) {
            $rootScope.toState = toState;
            $rootScope.toStateParams = toStateParams;
        });
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    };
})();