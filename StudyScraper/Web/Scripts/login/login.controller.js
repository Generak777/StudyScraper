(function () {
    "use strict";

    angular.module("app")
        .controller("loginController", LoginController);

    LoginController.$inject = ["$scope", "$location"];

    function LoginController($scope, $location) {
        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$onInit = _onInit;
        vm.userItem = {};
        vm.login = _login;

        function _onInit() {
        }

        function _login() {
            vm.$location.path("home");
        }
    }
})();