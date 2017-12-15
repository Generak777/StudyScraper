(function () {
    "use strict";
    angular.module("app")
        .factory("scraperService", ScraperService);

    ScraperService.$inject = ["$http", "$q"];

    function ScraperService($http, $q) {
        return {
            getAll: _getAll,
            savePost: _savePost
        }

        function _getAll() {
            return $http.get('/api/scraper')
                .then(success)
                .catch(error);
        }

        function _savePost(data) {
            return $http.post('/api/scraper',
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