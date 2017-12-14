(function () {
    angular.module("app")
        .controller("studyController", StudyController);

    StudyController.$inject = ["$scope"];

    function StudyController($scope) {
        var vm = this;
        vm.$scope = $scope;
        vm.$onInit = _onInit;

        function _onInit() {
            console.log("init study API controller");
        }
    }
})();