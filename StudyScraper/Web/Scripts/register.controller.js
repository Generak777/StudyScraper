(function () {
    angular.module("app")
        .controller("registerController", RegisterController);

    RegisterController.$inject = ["$scope"];

    function RegisterController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;

        function _onInit() {
            console.log("init register controller");
        }
    }
})();