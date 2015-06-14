(function () {
    'use strict';

    angular
        .module('auth')
        .factory('httpInterceptorService', httpInterceptorService);

    httpInterceptorService.$inject = ['$q', 'localStorageService', '$injector', 'httpBufferService'];

    function httpInterceptorService($q, localStorageService, $injector, httpBuffer) {
        var service = {
            request: request,
            //requestError: requestError, //Not implemented
            //response: response, //Not implemented
            responseError: responseError
        }

        return service;

        /***** public *****/

        /**
        * Adding the Authorization Bearer token to the config header on all requests.
        */
        function request(config) {
            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');

            if (authData)
                config.headers.Authorization = 'Bearer ' + authData.token;

            return config;
        }

        /**
        * Handles 401(Authorization) and tries to reauthinticate if user uses refreshtoken.
        */
        function responseError(rejection) {
            switch (rejection.status) {
                case 401:
                    return handleReAuthorization(rejection);
                default:
                    return $q.reject(rejection);
            }
        }

        
        /***** private *****/

        /**
        * Adds failed request to http buffer and tries to reauthinticate. If success then resends the failed requests, otherwise force user to reauth.
        */
        function handleReAuthorization(rejection) {
            var authService = $injector.get('authService');
            var authData = localStorageService.get('authorizationData');
            var deferred = $q.defer();

            httpBuffer.append(rejection.config, deferred);
            if (!authService.isRefreshing) {
                if (authData && authData.useRefreshTokens)
                    authService.refreshToken().then(refreshSuccessDelegate, refreshErrorDelegate);
                else {
                    $q.reject(rejection);
                    refreshErrorDelegate(rejection);
                }
            } else
                $q.reject(rejection);

            return deferred.promise;
        }

        /***** http delegates *****/

        /**
        * Delegatefunction to handle error from refreshToken(). Forces uses to logout.
        */
        function refreshErrorDelegate(rejection) {
            var $state = $injector.get('$state');
            var authService = $injector.get('authService');
            httpBuffer.rejectAll(rejection);
            authService.logOut();
            $state.go('login');
        }

        /**
        * Delegatefunction to handle success from refreshToken(), retries all request in httpbuffer.
        */
        function refreshSuccessDelegate() {
            httpBuffer.retryAll(configModifierDelegate);
        }

        /**
        * Delegatefunction to handle the modification needed on the request in httpbuffer.
        */
        function configModifierDelegate(config) {
            var authData = localStorageService.get('authorizationData');

            if (authData)
                config.headers.Authorization = 'Bearer ' + authData.token;
            return config;
        }
    }
})();