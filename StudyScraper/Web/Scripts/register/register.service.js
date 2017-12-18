(function () {
    "use strict";

    angular.module('app')
        .factory('registerService', RegisterService);

    RegisterService.$inject = ['$http', '$q'];

    function RegisterService($http, $q) {
        return {
            register: _register
        }

        function _register(data) {
            return $http.post('/api/register',
                data)
                .then(success)
                .catch(error);
        }

        function success(res) {
            return res;
        }

        function error(err) {
            return $q.reject(err);
        }
    }
})();