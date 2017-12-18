(function () {
    "use strict";

    angular.module('app')
        .factory('studiesService', StudiesService);

    StudiesService.$inject = ['$http', '$q'];

    function StudiesService($http, $q) {
        return {
            get: _get
        }

        function _get(searchTerm) {
            var url = 'http://nif-services.neuinfo.org/servicesv1/v1/literature/search?q=' + searchTerm;
            return $http.get(url)
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