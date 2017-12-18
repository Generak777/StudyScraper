(function () {
    "use strict";

    angular.module("app")
        .controller("registerController", RegisterController);

    RegisterController.$inject = ["$scope", "$location", "registerService"];

    function RegisterController($scope, $location, RegisterService) {
        var vm = this;
        vm.$scope = $scope;
        vm.userItem = {};
        vm.$onInit = _onInit;
        vm.registerService = RegisterService;
        vm.register = _register;
        vm.registerSuccess = _registerSuccess;
        vm.registerError = _registerError;

        function _onInit() {
            console.log("init register controller");
        }

        function _register() {
            vm.registerService.register(vm.userItem)
                .then(vm.registerSuccess)
                .catch(vm.registerError);
        }
        function _registerSuccess(res) {
            console.log(res);
            alert("Registration success!");
            $location.path('login');
        }
        function _registerError(res) {
            console.log(res);
            alert("Failed to register!");
        }
    }
})();