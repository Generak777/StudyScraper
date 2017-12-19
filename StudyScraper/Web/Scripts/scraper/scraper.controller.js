(function () {
    "use strict";

    angular.module("app")
        .controller("scraperController", ScraperController);

    ScraperController.$inject = ["$scope", "$location", "$cookies", "scraperService"];

    function ScraperController($scope, $location, $cookies, ScraperService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$cookies = $cookies;
        vm.scraperService = ScraperService;
        vm.userCookie = null;
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
            //check for user cookie
            vm.userCookie = vm.$cookies.getObject('user');
            //if cookie exists, init scraper
            if (vm.userCookie) {
                vm.getAll();
            //otherwise, redirect to login page
            } else {
                window.location = '/login';
            }
        }

        function _savePost(title, url) {
            var data = {
                "userId": vm.userCookie.id,
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