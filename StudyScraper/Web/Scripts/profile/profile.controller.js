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
        vm.$onInit = _onInit;
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

        function _getProfile(id) {
            vm.profileService.selectById(id)
                .then(vm.getProfileSuccess)
                .catch(vm.GetProfileError);
        }

        function _getProfileSuccess(res) {
            console.log(res);
        }

        function _getProfileError(err) {
            console.log(err);
        }
    }
})();