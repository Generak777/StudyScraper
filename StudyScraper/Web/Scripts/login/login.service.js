(function () {
    "use strict";

    angular.module("app")
        .factory("loginService", LoginService);

    LoginService.$inject = ["$http", "$q"];

    function LoginService($http, $q) {
        return {
            login: _login
        }

        function _login(data) {
            return $http.post('/api/login',
                data)
                .then(success)
                .catch(error);
        }

        function success(res) {
            return res;
        }

        function error(err) {
            return err;
        }
    }
})();