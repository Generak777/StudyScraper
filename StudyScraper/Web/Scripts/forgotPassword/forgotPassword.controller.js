(function () {
	"use strict";

	angular
		.module('app')
	.controller('forgotPasswordController', ForgotPasswordController);

	ForgotPasswordController.$inject = ['$scope', '$location', '$cookies', 'forgotPasswordService'];

	function ForgotPasswordController($scope, $location, $cookies, ForgotPasswordService) {
		var vm = this;
		vm.$scope = $scope;
		vm.$location = $location;
		vm.$cookies = $cookies;
		vm.forgotPasswordService = ForgotPasswordService;
        vm.userCookie = null;
        vm.email = null;
        vm.$onInit = _onInit;
        vm.sendEmail = _sendEmail;
        vm.sendEmailSuccess = _sendEmailSuccess;
        vm.sendEmailError = _sendEmailError;

		function _onInit() {
			//check for user cookie
			vm.userCookie = vm.$cookies.getObject('user');
			//if cookie exists, redirect user to home page
			if (vm.userCookie) {
				window.location = '/home';
				//otherwise, do nothing and allow the anon user to be on this page
			}
        }

        function _sendEmail() {
            var data = {
                "email": vm.email
            }
            vm.forgotPasswordService.sendEmail(data)
                .then(vm.sendEmailSuccess)
                .catch(vm.sendEmailError);
        }

        function _sendEmailSuccess(res) {
            alert('Reset password email sent');
        }

        function _sendEmailError(err) {
            alert('Failed to send password reset email!');
            console.log(err);
        }


	}
})();