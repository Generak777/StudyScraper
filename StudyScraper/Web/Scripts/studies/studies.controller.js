(function () {
    "use strict";

    angular.module("app")
        .controller("studiesController", StudiesController);

    StudiesController.$inject = ["$scope", "$location", "$cookies", "studiesService"];

    function StudiesController($scope, $location, $cookies, StudiesService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$cookies = $cookies;
        vm.studiesService = StudiesService;
        vm.userCookie = null;
        vm.studies = [];
        vm.searchTerm = "";
        vm.$onInit = _onInit;
        vm.getStudies = _getStudies;
        vm.getStudiesSuccess = _getStudiesSuccess;
        vm.getStudiesError = _getStudiesError;

        function _onInit() {
            //check for user cookie
            vm.userCookie = vm.$cookies.getObject('user');
            //if cookie exists, allow user to hit NCBI API
            if (vm.userCookie) {
                //otherwise, redirect to login page
            } else {
                window.location = '/login';
            }
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