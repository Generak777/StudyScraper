(function () {
    "use strict";

    angular.module("app")
        .controller("scraperController", ScraperController);

    ScraperController.$inject = ["$scope"];

    function ScraperController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;

        function _onInit() {
            console.log("init scraper controller");
        }
    }
})();