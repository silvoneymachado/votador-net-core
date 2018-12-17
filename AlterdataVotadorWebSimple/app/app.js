'use strict';
 
angular.module('myApp', [
  'ngRoute',
  'myApp.login'           // Newly added home module
]).
config(['$routeProvider', function($routeProvider) {
  // Set defualt view of our app to login
  
  $routeProvider.when('/home', {
    templateUrl: 'home/home.html',
    controller: 'HomeCtrl'
  });
  $routeProvider.otherwise({
      redirectTo: '/login'
  });
}]);