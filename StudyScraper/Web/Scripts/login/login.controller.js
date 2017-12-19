(function () {
    "use strict";

    angular.module("app")
        .controller("loginController", LoginController);

    LoginController.$inject = ["$scope", "$location", "$cookies", "loginService"];

    function LoginController($scope, $location, $cookies, LoginService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$cookies = $cookies;
        vm.loginService = LoginService;
        vm.$onInit = _onInit;
        vm.userItem = {};
        vm.login = _login;
        vm.loginSuccess = _loginSuccess;
        vm.loginError = _loginError;

        function _onInit() {
        }

        function _login() {
            vm.loginService.login(vm.userItem)
                .then(vm.loginSuccess)
                .catch(vm.loginError);
        }

        function _loginSuccess(res) {
            var obj = res.data;
            vm.$cookies.putObject("user", obj);
            window.location ='/home';
        }

        function _loginError(err) {
            alert("Failed to login! Username or password incorrect.");
            console.log(err);
        }
    }
})();