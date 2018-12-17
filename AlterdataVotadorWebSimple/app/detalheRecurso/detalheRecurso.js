'use strict';
 
angular.module('myApp.home', ['ngRoute'])
 
// Declared route 
.config(['$routeProvider', function($routeProvider) {
    $routeProvider.when('/home', {
        templateUrl: 'home/home.html',
        controller: 'HomeCtrl'
    });
}])
 
// Home controller
.controller('HomeCtrl',[ '$scope', '$http', '$window',  function($scope, $http, $window) {
    $scope.endpointHome = "https://localhost:44371/api/recurso/ranking";
    $scope.endpointVoto = "https://localhost:44371/api/votacao";
    $scope.ranking = [];
    $scope.votos = [];
    $scope.searchRecursoText = '';
    $scope.loggedUser = '';

    $scope.Logout = function() {
        localStorage.clear();
        $window.location.href = "../login/login.html";
    };

    $scope.votar = function(recurso){
        var mensagem = prompt('Por favor digite um comentario com no mínimo 5 caracteres');

        if (mensagem != null && mensagem.length > 5) {
            if(recurso){
                $scope.voto = {
                    idRecurso: recurso.id,
                    idUsuario: $scope.loggedUser.id,
                    comentario: mensagem
                };
            }
            $http({
                method: 'POST',
                url: $scope.endpointHome,
                data: $scope.voto,
                headers: {
                    'Authorization': `Bearer ${$scope.loggedUser.token}`,
                    'Content-type': 'application/json'
                }
            })
            .then(function(res) {
                $scope.ranking = res.data;
            }, function(err){
                console.log(err);
            });
        } 
    }

    $scope.oninit = function(){
        if($scope.IsLogged()){
            $scope.LoadRanking();
        } else{
            $scope.Logout();
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

    $scope.LoadRanking = function() {
        $http({
            method: 'GET',
            url: $scope.endpointHome,
            headers: {
                'Authorization': `Bearer ${$scope.loggedUser.token}`,
                'Content-type': 'application/json'
            }
        })
        .then(function(res) {
            $scope.ranking = res.data;
        }, function(err){
            console.log(err);
        });
    };

    $scope.LoadVotos = function(){
        $http({
            method: 'GET',
            url: $scope.endpointVoto,
            headers: {
                'Authorization': `Bearer ${$scope.loggedUser.token}`,
                'Content-type': 'application/json'
            }
        })
        .then(function(res) {
            $scope.votos = res.data;
        }, function(err){
            console.log(err);
        });
    }
}]);