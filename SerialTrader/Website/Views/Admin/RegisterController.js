"use strict";

define(['application-configuration', 'adminService', 'alertsService'], function (app) {

    app.register.controller('registerController', ['$scope', '$rootScope','$routeParams', 'adminService', 'alertsService',
        function ($scope, $rootScope, $routeParams, adminService, alertsService) {
            
            $scope.initializeController = function () {

                var subscriptionType = ($routeParams.id || "");

                $rootScope.closeAlert = alertsService.closeAlert;
                $rootScope.applicationModule = "Admin";
                $rootScope.alerts = [];

                $scope.SubscriptionType = subscriptionType;
                $scope.Title = "";
                $scope.ContactPerson = "";
                $scope.ContactNumber = "";
                $scope.Email = "";
                $scope.Address = "";
                $scope.UserName = "";
                $scope.Password = "";
                $scope.PasswordConfirmation = "";
                $scope.TermsAccepted = "";
            }

            $scope.createClient = function () {
                var register = $scope.registerClient();
                adminService.registerClient(register, $scope.registerClientCompleted, $scope.registerClientError);
            }

            $scope.registerClientCompleted = function (response) {
                $rootScope.MenuItems = response.MenuItems;
                window.location = "Application.html#/Dashboard";
            }

            $scope.registerClientError = function (response) {

                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();              
                alertsService.SetValidationErrors($scope, response.ValidationErrors);

            }

            $scope.clearValidationErrors = function () {

                //$scope.FirstNameInputError = false;
                //$scope.LastNameInputError = false;
                $scope.UserNameInputError = false;
                $scope.EmailAddressInputError = false;
                $scope.PasswordInputError = false;
                $scope.PasswordConfirmationInputError = false;

            }

            $scope.registerClient = function () {

                var register = new Object();

                register.SubscriptionType = $scope.SubscriptionType;
                register.Title = $scope.Title;
                register.ContactPerson = $scope.ContactPerson;
                register.ContactNumber = $scope.ContactNumber;
                register.Email = $scope.Email;
                register.Address = $scope.Address;
                register.UserName = $scope.UserName;
                register.Password = $scope.Password;
                register.PasswordConfirmation = $scope.PasswordConfirmation;
                register.TermsAccepted = $scope.TermsAccepted;
                
                return register;
            }

        }]);
});
