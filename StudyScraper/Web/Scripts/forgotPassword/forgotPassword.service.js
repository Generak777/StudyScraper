(function () {
    "use strict";

    angular
        .module('app')
        .factory('forgotPasswordService', ForgotPasswordService);

    ForgotPasswordService.$inject = ['$http', '$q'];

    function ForgotPasswordService($http, $q) {
        return {
            sendEmail: _sendEmail
        }

        function _sendEmail(data) {
            return $http.post('/api/forgotPassword',
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