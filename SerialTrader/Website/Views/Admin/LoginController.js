"use strict";

define(['application-configuration', 'adminService', 'alertsService'], function (app) {

  app.register.controller('loginController', ['$scope', '$rootScope', 'adminService', 'alertsService',
    function ($scope, $rootScope, adminService, alertsService) {

            $rootScope.closeAlert = alertsService.closeAlert;
            $rootScope.alerts = [];

            $scope.initializeController = function () {
               
                $scope.UserName = "";               
                $scope.Password = "";
            }

            $scope.login = function () {
                $rootScope.IsloggedIn = false;
                var user = $scope.createLoginCredentials();
                adminService.login(user, $scope.loginCompleted, $scope.loginError);
            }

            $scope.loginCompleted = function (response) {
                $rootScope.MenuItems = response.MenuItems;        
                window.location = "Application.html#/Admin/Dashboard";
            }

            $scope.loginError = function (response) {

                alertsService.RenderErrorMessage(response.ReturnMessage);
        
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);              

            }

            $scope.clearValidationErrors = function () {
              
                $scope.UserNameInputError = false;               
                $scope.PasswordInputError = false;               

            }

            $scope.createLoginCredentials = function () {

                var user = new Object();
               
                user.UserName = $scope.UserName;              
                user.Password = $scope.Password;
             
                return user;

            }

        }]);
});
