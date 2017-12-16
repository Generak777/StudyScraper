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
        vm.loaded = false;
        vm.savePost = _savePost;
        vm.savePostSuccess = _savePostSuccess;
        vm.savePostError = _savePostError;
        vm.getAll = _getAll;
        vm.getAllSuccess = _getAllSuccess;
        vm.getAllError = _getAllError;

        function _onInit() {
            vm.getAll();
        }

        function _savePost(title, url) {
            var data = {
                "title": title,
                "url": url
            }
            vm.scraperService.savePost(data)
                .then(vm.savePostSuccess).catch(vm.savePostError);
        }

        function _savePostSuccess(res) {
            alert("Post saved!");
            console.log(res);
        }

        function _savePostError(err) {
            alert("Failed to save post!");
            console.log(err);
        }

        function _getAll() {
            vm.scraperService.getAll()
                .then(vm.getAllSuccess)
                .catch(vm.getAllError);
        }

        function _getAllSuccess(res) {
            vm.posts = res.data.item.posts;
            vm.loaded = true;
        }

        function _getAllError(err) {
            console.log(err);
        }
    }
})();