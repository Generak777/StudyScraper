(function () {
    "use strict";

    angular
        .module('app')
        .factory('profileService', ProfileService);

    ProfileService.$inject = ['$http', '$q'];

    function ProfileService($http, $q) {
        return {
            selectById: _selectById
        }

        function _selectById(id) {
            return $http.get('api/profile/' + id)
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