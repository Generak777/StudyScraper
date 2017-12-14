(function () {
    "use strict";
    angular.module("app")
        .factory("scraperService", ScraperService);

    ScraperService.$inject = ["$http", "$q"];

    function ScraperService($http, $q) {
        return {
            getAll: _getAll
        }

        function _getAll() {
            return $http.get('/api/scraper')
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