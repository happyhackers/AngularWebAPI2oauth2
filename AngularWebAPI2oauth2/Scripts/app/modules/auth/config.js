(function () {
    'use strict';

    angular
        .module('auth')
        .run(run)
        .constant('authSettings', {
            apiServiceBaseUri: '/',
            clientId: '',
            loginState: 'login',
            accessDeniedState: 'accessdenied',
            tokenEndpoint: '/token'
        });

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
