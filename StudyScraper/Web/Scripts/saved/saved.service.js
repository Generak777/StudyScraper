(function () {
    "use strict";

    angular.module("app")
        .factory("savedService", SavedService);

    SavedService.$inject = ["$http", "$q"];

    function SavedService($http, $q) {
        return {
            getAll: _getAll,
            selectById: _selectById,
            update: _update,
            delete: _delete
        }

        function _getAll(id) {
            return $http.get('/api/saved/user/' + id)
                .then(success)
                .catch(error);
        }

        function _selectById(id) {
            return $http.get('api/saved/' + id)
                .then(success)
                .catch(error);
        }

        function _update(data) {
            return $http.put('api/saved',
                data)
                .then(success)
                .catch(error);
        }

        function _delete(id) {
            return $http.delete('api/saved/' + id)
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