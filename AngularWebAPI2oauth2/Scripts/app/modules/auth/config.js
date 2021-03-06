﻿(function () {
    'use strict';

    angular
        .module('auth')
        .config(config)
        .run(run)
        .constant('authSettings', {
            apiServiceBaseUri: '/',
            clientId: 'AngularApp',
            loginState: 'login',
            accessDeniedState: 'accessdenied',
            tokenEndpoint: '/token'
        });

    config.$inject = ['$httpProvider'];

    function config($httpProvider) {
        $httpProvider.interceptors.push('httpInterceptorService');
    }

    run.$inject = ['$rootScope', '$state', '$stateParams', 'authService'];

    function run($rootScope, $state, $stateParams, authService) {
        authService.fillAuthData();
        $rootScope.$on('$stateChangeStart', function (event, toState, toStateParams) {
            $rootScope.toState = toState;
            $rootScope.toStateParams = toStateParams;

            if (!authService.authorize())
                event.preventDefault(); //If authorize fails then we reject the transitionpromise
        });
    };
})();
