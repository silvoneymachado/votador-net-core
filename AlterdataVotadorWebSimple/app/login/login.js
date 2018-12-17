'use strict';
 
angular.module('myApp.login', ['ngRoute'])
 
// Declared route 
.config(['$routeProvider', function($routeProvider) {
    $routeProvider.when('/login', {
        templateUrl: 'login/login.html',
        controller: 'LoginCtrl'
    });
}])
 
// Login controller
.controller('LoginCtrl',[ '$scope', '$http', '$window',  function($scope, $http, $window) {
    $scope.endpointLogin = "https://localhost:44371/api/usuario/token";
    $scope.user = {
        email: 'adminvotacao@alterdata.com.br',
        senha: '123456'
    }

    $scope.SignIn = function(event) {
        event.preventDefault();  // To prevent form refresh
        
        console.log($scope.user);
        
        $http.post($scope.endpointLogin, $scope.user)
        .then(function(res) {
                // Success callback
                console.log('Authentication successful');
                console.log(res);
                localStorage.setItem('usuario', JSON.stringify(res.data));
                $window.location.href = "../home/home.html"
            }, function(error) {
                // Failure callback
                console.log('Authentication failure');
                console.log(error);
            });
    }

    $scope.oninit = function(){
        if($scope.IsLogged()){
            $window.location.href = "../login/login.html";
        }
    }

    $scope.IsLogged = function() {
        let userData = localStorage.getItem("usuario");
        if(!userData){
            return false;
        } else{
            $scope.loggedUser = JSON.parse(userData);
            return true;
        }
    };
}]);