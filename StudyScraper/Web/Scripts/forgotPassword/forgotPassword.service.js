(function () {
    "use strict";
    angular.module("app")
        .factory("forgotPasswordService", ForgotPasswordService);

    ForgotPasswordService.$inject = ["$http", "$q"];

    function ForgotPasswordService($http, $q) {
        return {
            getByEmail: _getByEmail,
            check: _check,
            resetPassword: _resetPassword
        }

        function _getByEmail(email) {
            return $http.get("/api/confirmation/" + email + "/g",
                { withCredentials: true }
            )
                .then(getByEmailSuccess).catch(getByEmailError);

            function getByEmailSuccess(res) {
                return res;
            }

            function getByEmailError(err) {
                return $q.reject(err);
            }
        }

        function _check(token) {
            return $http.get("/api/confirmation/" + token,
                { withCredentials: true }
            )
                .then(checkSuccess).catch(checkError);

            function checkSuccess(res) {
                return res;
            }

            function checkError(err) {
                return $q.reject(err);
            }
        }

        function _resetPassword(data) {
            return $http.post("/api/confirmation",
                data,
                { withCredentials: true }
            ).then(resetPasswordSuccess).catch(resetPasswordError);

            function resetPasswordSuccess(res) {
                return res;
            }

            function resetPasswordError(err) {
                return $q.reject(err);
            }
        }
    }
})();