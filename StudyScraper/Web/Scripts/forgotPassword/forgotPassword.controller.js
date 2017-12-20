(function () {
    "use strict";
    angular.module("app")
        .controller("forgotPasswordController", ForgotPasswordController)

    ForgotPasswordController.$inject = ["$scope", "$location", "$cookies", "forgotPasswordService"];

    function ForgotPasswordController($scope, $location, $cookies, ForgotPasswordService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$location = $location;
        vm.$cookies = $cookies;
        vm.forgotPasswordService = ForgotPasswordService;
        vm.$onInit = _onInit;
        vm.path = null;
        vm.item = null;
        vm.resetItem = {};
        vm.newPass = null;
        vm.token = null;
        vm.showForm = true;
        vm.formSubmitted = false;
        vm.resetting = false;
        vm.showNoResetDiv = false;
        vm.successfulReset = false;
        vm.checkEmail = _checkEmail;
        vm.checkEmailSuccess = _checkEmailSuccess;
        vm.checkEmailError = _checkEmailError;
        vm.validate = _validate;
        vm.validateSuccess = _validateSuccess;
        vm.validateError = _validateError;
        vm.allowReset = _allowReset;
        vm.resetPassword = _resetPassword;
        vm.resetPasswordSuccess = _resetPasswordSuccess;
        vm.resetPasswordError = _resetPasswordError;
        vm.noReset = _noReset;

        function _onInit() {
            //check for user cookie
            vm.userCookie = vm.$cookies.getObject('user');
            //if cookie exists, redirect user to home page
            if (vm.userCookie) {
                window.location = '/home';
                //otherwise, do nothing and allow the anon user to be on this page
            }
            var absUrl = vm.$location.absUrl();
            vm.token = vm.$location.path().slice(16);
            console.log(vm.token);
            if (absUrl != "http://localhost:52505/forgotPassword/") {
                vm.validate(vm.token);
            }
        }

        function _checkEmail() {
            vm.forgotPasswordService.getByEmail(vm.item.email)
                .then(vm.checkEmailSuccess).catch(vm.checkEmailError);
        }

        function _checkEmailSuccess(res) {
            vm.showForm = false;
            vm.formSubmitted = true;
        }

        function _checkEmailError(err) {
        }

        function _validate(token) {
            vm.showForm = false;
            vm.formSubmitted = false;
            vm.forgotPasswordService.check(vm.token)
                .then(vm.validateSuccess).catch(vm.validateError);
        }

        function _validateSuccess(res) {
            if (res.data.item) {
                vm.allowReset();
            } else {
                vm.noReset();
            }
        }

        function _validateError(err) {
        }

        function _allowReset() {
            vm.resetting = true;
        }

        function _noReset() {
            vm.showNoResetDiv = true;
        }

        function _resetPassword() {
            vm.resetItem.Token = vm.token;
            vm.resetItem.NewBasicPassword = vm.newPass;
            vm.forgotPasswordService.resetPassword(vm.resetItem)
                .then(vm.resetPasswordSuccess).catch(vm.resetPasswordError);
        }

        function _resetPasswordSuccess(res) {
            vm.resetting = false;
            vm.successfulReset = true;
        }

        function _resetPasswordError(err) {
        }
    }
})();