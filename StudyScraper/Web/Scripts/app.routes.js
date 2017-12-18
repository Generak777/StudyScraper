(function () {
    'use strict';
    var app = angular.module('app.routes', []);

    app.config(_configureStates);

    _configureStates.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider'];

    function _configureStates($stateProvider, $locationProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
        });
        $urlRouterProvider.otherwise('/home');
        $stateProvider
            .state({
                name: 'home',
                url: '/home',
                templateUrl: '/app/modules/home/home.html',
                title: 'Home',
                controller: 'homeController as homeCtrl'
            })
            .state({
                name: 'register',
                url: '/register',
                templateUrl: '/app/modules/register/register.html',
                title: 'Register',
                controller: 'registerController as registerCtrl'
            })
            .state({
                name: 'login',
                url: '/login',
                templateUrl: '/app/modules/login/login.html',
                title: 'Login',
                controller: 'loginController as loginCtrl'
            })
            .state({
                name: 'scraper',
                url: '/scraper',
                templateUrl: '/app/modules/redditScraper/redditScraper.html',
                title: 'Reddit Scraper',
                controller: 'scraperController as scraperCtrl'
            })
            .state({
                name: 'studies',
                url: '/studies',
                templateUrl: '/app/modules/studies/studies.html',
                title: 'Study APIs',
                controller: 'studiesController as studiesCtrl'
            })
            .state({
                name: 'saved',
                url: '/saved',
                templateUrl: '/app/modules/saved/saved.html',
                title: 'Saved Studies',
                controller: 'savedController as savedCtrl'
            });
    }
})();