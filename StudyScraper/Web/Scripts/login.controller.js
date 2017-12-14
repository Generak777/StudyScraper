(function () {
    angular.module("app")
        .controller("loginController", LoginController);

    LoginController.$inject = ["$scope"];

    function LoginController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;

        function _onInit() {
            console.log("init login controller");
        }
    }
})();