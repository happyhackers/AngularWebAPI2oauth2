(function () {
    'use strict';

    angular
        .module('auth')
        .factory('httpBufferService', httpBufferService);

    httpBufferService.$inject = ['$injector'];

    function httpBufferService($injector) {
        var service = {
            append: append,
            rejectAll: rejectAll,
            retryAll: retryAll
        };

        var buffer = [];
        var $http; /** Service initialized later because of circular dependency problem. */

        return service;

        /**
        * Appends HTTP request configuration object with deferred response attached to buffer.
        */
        function append(config, deferred) {
            buffer.push({
                config: config,
                deferred: deferred
            });
        }

        /**
        * Abandon or reject (if reason provided) all the buffered requests.
        */
        function rejectAll(reason) {
            if (reason) {
                for (var i = 0; i < buffer.length; ++i) {
                    buffer[i].deferred.reject(reason);
                }
            }
            buffer = [];
        }

        /**
        * Retries all the buffered requests clears the buffer.
        */
        function retryAll(updater) {
            for (var i = 0; i < buffer.length; ++i) {
                retryHttpRequest(updater(buffer[i].config), buffer[i].deferred);
            }
            buffer = [];
        }

        /***** private *****/

        function retryHttpRequest(config, deferred) {
            function successCallback(response) {
                deferred.resolve(response);
            }
            function errorCallback(response) {
                deferred.reject(response);
            }
            $http = $http || $injector.get('$http');
            $http(config).then(successCallback, errorCallback);
        }
    }
})();