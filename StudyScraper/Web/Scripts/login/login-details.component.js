(function () {
    "use strict";

    angular
        .module("app")
        .component("loginDetails", {
            templateUrl: '/app/modules/login/login-details.html',
            controller: 'loginDetailsController'
        });
})();

(function () {
    "use strict";

    angular
        .module('app')
        .controller('loginDetailsController', LoginDetailsController);

    LoginDetailsController.$inject = ['$scope', '$location', '$cookies'];

    function LoginDetailsController($scope, $location, $cookies) {
        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$cookies = $cookies;
        vm.loggedIn = false;
        vm.userInfo = null;
        vm.$onInit = _onInit;
        vm.logout = _logout;

        function _onInit() {
            vm.userInfo = vm.$cookies.getObject('user');
            if (vm.userInfo) {
                vm.loggedIn = vm.userInfo.isLoggedIn;
            };
        }

        function _logout() {
            vm.$cookies.remove('user');
            vm.loggedIn = false;
            vm.userInfo = null;
            vm.$location.path('login');
        }
    }
})();