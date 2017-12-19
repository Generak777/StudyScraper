(function () {
    "use strict";

    angular.module("app")
        .controller("savedController", SavedController);

    SavedController.$inject = ["$scope", "$location", "$cookies", "savedService"];

    function SavedController($scope, $location, $cookies, SavedService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$cookies = $cookies;
        vm.savedService = SavedService;
        vm.userCookie = null;
        vm.loaded = false;
        vm.posts = [];
        vm.updateData = {};
        vm.$onInit = _onInit;
        vm.getAll = _getAll;
        vm.getAllSuccess = _getAllSuccess;
        vm.getAllError = _getAllError;
        vm.selectById = _selectById;
        vm.selectByIdSuccess = _selectByIdSuccess;
        vm.selectByIdError = _selectByIdError;
        vm.update = _update;
        vm.updateSuccess = _updateSuccess;
        vm.updateError = _updateError;
        vm.delete = _delete;
        vm.deleteSuccess = _deleteSuccess;
        vm.deleteError = _deleteError;

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

        function _getAll() {
            vm.savedService.getAll()
                .then(vm.getAllSuccess)
                .catch(vm.getAllError);
        }

        function _getAllSuccess(res) {
            vm.posts = res.data.items;
            vm.loaded = true;
        }

        function _getAllError(err) {
            alert("Failed to get all saved posts!");
            console.log(err);
        }

        function _selectById(id) {
            vm.savedService.selectById(id)
                .then(vm.selectByIdSuccess)
                .catch(vm.selectByIdError);
        }

        function _selectByIdSuccess(res) {
            vm.updateData.id = res.data.item.id;
            vm.updateData.title = res.data.item.title;
            vm.updateData.url = res.data.item.url;
            vm.updateData.notes = res.data.item.notes;
            $('#updateModal').modal('show');
        }

        function _selectByIdError(err) {
            alert("Failed to select post by ID!");
            console.log(err);
        }

        function _update() {
            vm.savedService.update(vm.updateData)
                .then(vm.updateSuccess)
                .catch(vm.updateError);
        }

        function _updateSuccess(res) {
            $('#updateModal').modal('hide');
            vm.getAll();
        }

        function _updateError(err) {
            alert("Failed to update desired post!");
            console.log(err);
        }

        function _delete(id) {
            vm.savedService.delete(id)
                .then(vm.deleteSuccess)
                .catch(vm.deleteError);
        }

        function _deleteSuccess(res) {
            vm.getAll();
        }

        function _deleteError(err) {
            alert("Failed to delete post!");
            console.log(err);
        }
    }
})();