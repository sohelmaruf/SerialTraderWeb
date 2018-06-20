"use strict";

define(['application-configuration', 'mainService', 'alertsService'], function (app) {

    app.register.controller('defaultController', ['$scope',  '$rootScope', 'mainService', 'alertsService', function ($scope,  $rootScope, mainService, alertsService) {

        $rootScope.closeAlert = alertsService.closeAlert;

        $scope.initializeController = function () {                     
            window.location = "index.html#/Main/Home";  
            //mainService.initializeIndex($scope.initializeIndexComplete, $scope.initializeIndexError);
        }

        $scope.initializeIndexComplete = function (response)
        {     
            window.location = "index.html#/Main/Home";  
        }

        $scope.initializeIndexError = function (response)
        {         
          alertsService.RenderErrorMessage(response.ReturnMessage);
        }
    }]);
});


