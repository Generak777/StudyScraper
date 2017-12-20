(function () {
    "use strict";

    angular
        .module('app')
        .factory('profileService', ProfileService);

    ProfileService.$inject = ['$http', '$q'];

    function ProfileService($http, $q) {
        return {
            selectById: _selectById,
            updateProfile: _updateProfile,
            changePassword: _changePassword
        }

        function _selectById(id) {
            return $http.get('api/profile/' + id)
                .then(success)
                .catch(error);
        }

        function _updateProfile(data) {
            return $http.post('/api/profile/update',
                data)
                .then(success)
                .catch(error);
        }

        function _changePassword(data) {
            return $http.put('api/profile/changePassword',
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