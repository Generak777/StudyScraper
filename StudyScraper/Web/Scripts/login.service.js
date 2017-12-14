(function () {
    "use strict";

    angular.module("app")
        .factory("loginService", LoginService);

    LoginService.$inject = ["$http", "$q"];

    function LoginService($http, $q) {
        return {

        }
    }
})();