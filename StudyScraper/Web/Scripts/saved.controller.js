(function () {
    "use strict";

    angular.module("app")
        .controller("savedController", SavedController);

    SavedController.$inject = ["$scope"];

    function SavedController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;

        function _onInit() {
            console.log("init saved study controller");
        }
    }
})();