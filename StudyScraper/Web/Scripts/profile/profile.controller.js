(function () {
    "use strict";

    angular
        .module('app')
        .controller('profileController', ProfileController);

    ProfileController.$inject = ['$scope', '$location', '$cookies', 'profileService'];

    function ProfileController($scope, $location, $cookies, ProfileService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$cookies = $cookies;
        vm.profileService = ProfileService;
        vm.userCookie = null;
        vm.profileInfo = {};
        vm.updateData = {};
        vm.$onInit = _onInit;
        vm.showUpdateModal = _showUpdateModal;
        vm.updateProfile = _updateProfile;
        vm.updateProfileSuccess = _updateProfileSuccess;
        vm.updateProfileError = _updateProfileError;
        vm.getProfile = _getProfile;
        vm.getProfileSuccess = _getProfileSuccess;
        vm.getProfileError = _getProfileError;

        function _onInit() {
            //check for user cookie
            vm.userCookie = vm.$cookies.getObject('user');
            //if cookie exists, get profile info
            if (vm.userCookie) {
                vm.getProfile(vm.userCookie.id);
            //otherwise, redirect to login page
            } else {
                window.location = '/login';
            }
        }

        function _showUpdateModal() {
            vm.updateData.userId = vm.userCookie.id;
            vm.updateData.firstName = vm.profileInfo.firstName;
            vm.updateData.middleInitial = vm.profileInfo.middleInitial;
            vm.updateData.lastName = vm.profileInfo.lastName;
            $('#updateModal').modal('show');
        }

        function _updateProfile() {
            vm.profileService.updateProfile(vm.updateData)
                .then(vm.updateProfileSuccess)
                .catch(vm.updateProfileError);
        }

        function _updateProfileSuccess(res) {
            $('#updateModal').modal('hide');
            vm.updateData = {};
            vm.getProfile(vm.userCookie.id);
        }

        function _updateProfileError(err) {
            alert("Failed to update profile!");
            console.log(err);
        }

        function _getProfile(id) {
            vm.profileService.selectById(id)
                .then(vm.getProfileSuccess)
                .catch(vm.GetProfileError);
        }

        function _getProfileSuccess(res) {
            vm.profileInfo = res.data.item;
            if (vm.profileInfo.middleInitial === (' ' || '')) {
                vm.profileInfo.middleInitial = null;
            }
        }

        function _getProfileError(err) {
            console.log(err);
        }
    }
})();