(function () {
    "use strict";

    angular.module("app")
        .controller("registerController", RegisterController);

    RegisterController.$inject = ["$scope", "$location", "$cookies", "registerService"];

    function RegisterController($scope, $location, $cookies, RegisterService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$cookies = $cookies;
        vm.userCookie = null;
        vm.userItem = {};
        vm.$onInit = _onInit;
        vm.registerService = RegisterService;
        vm.register = _register;
        vm.registerSuccess = _registerSuccess;
        vm.registerError = _registerError;

        function _onInit() {
            //check for user cookie
            vm.userCookie = vm.$cookies.getObject('user');
            //if cookie exists, redirect user to home page
            if (vm.userCookie) {
                window.location = '/home';
                //otherwise, do nothing
            }
        }

        function _register() {
            vm.registerService.register(vm.userItem)
                .then(vm.registerSuccess)
                .catch(vm.registerError);
        }
        function _registerSuccess(res) {
            alert("Registration success!");
            vm.$location.path('login');
        }
        function _registerError(res) {
            console.log(res);
            alert("Failed to register!");
        }
    }
})();