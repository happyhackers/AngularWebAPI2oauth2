(function () {
    'use strict';

    angular
        .module('magnaCuraWebbApp')
        .factory('Authentication', authentication);

    authentication.$inject = ['$localStorage'];

    function authentication($localStorage) {
        //public interface
        var service = {
            authenticate: authenticate,
            isAuthenticated: isAuthenticated,
            revokeAuthentication: revokeAuthentication
        }

        return service;

        //Private implementation

        function authenticate() {
            $localStorage.user = {
                'userid': 'Simon',
                'token': 'aoimidfoaismdfä',
                'isAuthenticated': true
            };
        }

        function isAuthenticated() {
            return $localStorage.user.IsAuthenticated;
        }

        function revokeAuthentication() {
            $localStorage.user = null;
        }
    }
})();