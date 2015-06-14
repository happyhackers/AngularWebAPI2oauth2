(function () {
    'use strict';

    angular
        .module('auth')
        .factory('authService', authService);

    authService.$inject = ['$rootScope', '$state', '$http', '$q', 'localStorageService', 'authSettings'];

    function authService($rootScope, $state, $http, $q, localStorageService, authSettings) {
        var service = {
            authentication: { isAuth: false, userName: "", useRefreshTokens: false, roles: [], companyName: 'Easy Storage' },
            authorize: authorize,
            fillAuthData: fillAuthData,
            isInAnyRole: isInAnyRole,
            isInRole: isInRole,
            isRefreshing: false,
            login: login,
            logOut: logOut,
            refreshToken: refreshToken
        };

        var tokenUri = getTokenUri();

        return service;

        /***** public *****/

        /**
        * Handles statechanges authorization control, checks if user is allowed to the designated state.
        */
        function authorize() {
            var isAuthenticated = service.authentication.isAuth;

            // if state doesnt require any authorization just pass them
            if (!$rootScope.toState.data.authed)
                return true;
            else {
                // if user is authenticated but not the required role they get the accessdenied state
                if (isAuthenticated) {
                    if ($rootScope.toState.data.roles && $rootScope.toState.data.roles.length > 0 && !isInAnyRole($rootScope.toState.data.roles)) {
                        $state.go(authSettings.accessDeniedState);
                        return false;
                    }
                } else {
                    // user is not authenticated. stow the state they wanted before you
                    // send them to the login state, so you can return them when you're done
                    $rootScope.returnToState = $rootScope.toState;
                    $rootScope.returnToStateParams = $rootScope.toStateParams;
                    $rootScope.errorMessage = 'Du måste vara inloggad för att nå denna sida';
                    // now, send them to the login state so they can log in
                    $state.go(authSettings.loginState);
                    return false;
                }
            }

            return true;
        }

        /**
        * Controls if user is in any of the roles asked for.
        */
        function isInAnyRole(roles) {
            if (!service.authentication.isAuth || !service.authentication.roles) return false;

            for (var i = 0; i < roles.length; i++) {
                if (isInRole(roles[i])) return true;
            }
            return false;
        }

        /**
        * Controls if user is in the role asked for.
        */
        function isInRole(role) {
            if (!service.authentication.isAuth || !service.authentication.roles) return false;

            return service.authentication.roles.indexOf(role) != -1;
        }

        /**
        * Handles user login and creates the authentication object on success.
        */
        function login(loginData) {

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
            data = data + "&client_id=" + authSettings.clientId;
            var deferred = $q.defer();

            $http.post(tokenUri, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                if (loginData.useRefreshTokens) {
                    localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: response.refresh_token, useRefreshTokens: true, roles: JSON.parse(response.roles) });
                }
                else {
                    localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false, roles: JSON.parse(response.roles) });
                }
                service.authentication.isAuth = true;
                service.authentication.userName = loginData.userName;
                service.authentication.useRefreshTokens = loginData.useRefreshTokens;
                service.authentication.roles = JSON.parse(response.roles);
                deferred.resolve(response);

            }).error(function (err) {
                logOut();
                deferred.reject(err);
            });

            return deferred.promise;
        }

        /**
        * Handles logout removing authentication object and any data in localstorage connected to user.
        */
        function logOut() {
            localStorageService.remove('authorizationData');

            service.authentication.isAuth = false;
            service.authentication.userName = "";
            service.authentication.useRefreshTokens = false;
            service.authentication.roles = [];
            service.authentication.companyName = 'Easy Storage';
        }

        /**
        * Updates user authentication object from localstorage object.
        */
        function fillAuthData() {

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                service.authentication.isAuth = true;
                service.authentication.userName = authData.userName;
                service.authentication.useRefreshTokens = authData.useRefreshTokens;
                service.authentication.roles = authData.roles;
                service.authentication.companyName = authData.companyName;
            }
        }

        /**
        * Handles the renewing of accesstoken if user uses refreshtoken.
        */
        function refreshToken() {
            var deferred = $q.defer();

            var authData = localStorageService.get('authorizationData');

            if (authData && authData.useRefreshTokens && !service.isRefreshing) {

                var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + authSettings.clientId;

                localStorageService.remove('authorizationData');
                service.isRefreshing = true;
                $http.post(tokenUri, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                    localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true, roles: JSON.parse(response.roles) });

                    deferred.resolve(response);
                    service.isRefreshing = false;
                }).error(function (err) {
                    logOut();
                    deferred.reject(err);
                    service.isRefreshing = false;
                });
            }

            return deferred.promise;
        }

        /***** private *****/

        function getTokenUri() {
            if (endsWith(authSettings.apiServiceBaseUri, '/')) {
                if (startsWith(authSettings.tokenEndpoint, '/'))
                    return authSettings.apiServiceBaseUri.slice(0, -1) + authSettings.tokenEndpoint;
                else
                    return authSettings.apiServiceBaseUri + authSettings.tokenEndpoint;

            } else if (startsWith(authSettings.tokenEndpoint, '/')) 
                return authSettings.apiServiceBaseUri + authSettings.tokenEndpoint;
             else 
                return authSettings.apiServiceBaseUri + '/' + authSettings.tokenEndpoint;
        }

        function startsWith(str, prefix) {
            return str.indexOf(prefix, 0) !== -1;
        }

        function endsWith(str, suffix) {
            return str.indexOf(suffix, str.length - suffix.length) !== -1;
        }
    }
})();