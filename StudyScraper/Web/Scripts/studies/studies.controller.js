(function () {
    "use strict";

    angular.module("app")
        .controller("studiesController", StudiesController);

    StudiesController.$inject = ["$scope", "studiesService"];

    function StudiesController($scope, StudiesService) {
        var vm = this;
        vm.$scope = $scope;
        vm.studiesService = StudiesService;
        vm.studies = [];
        vm.searchTerm = "";
        vm.$onInit = _onInit;
        vm.getStudies = _getStudies;
        vm.getStudiesSuccess = _getStudiesSuccess;
        vm.getStudiesError = _getStudiesError;

        function _onInit() {
        }

        function _getStudies() {
            vm.studiesService.get(vm.searchTerm)
                .then(vm.getStudiesSuccess)
                .catch(vm.getStudiesError);
        }

        function _getStudiesSuccess(res) {
            console.log(res);
            vm.studies = res.data.result.publications;
        }

        function _getStudiesError(err) {
            console.log(err);
        }
    }
})();