(function () {
    "use strict";

    angular.module("app")
        .controller("scraperController", ScraperController);

    ScraperController.$inject = ["$scope", "scraperService"];

    function ScraperController($scope, ScraperService) {
        var vm = this;
        vm.$scope = $scope;
        vm.scraperService = ScraperService;
        vm.posts = [];
        vm.$onInit = _onInit;
        vm.getAll = _getAll;
        vm.getAllSuccess = _getAllSuccess;
        vm.getAllError = _getAllError;

        function _onInit() {
            console.log("init scraper controller");
            vm.getAll();
        }

        function _getAll() {
            vm.scraperService.getAll()
                .then(vm.getAllSuccess)
                .catch(vm.getAllError);
        }

        function _getAllSuccess(res) {
            console.log(res);
        }

        function _getAllError(err) {
            console.log(err);
        }
    }
})();