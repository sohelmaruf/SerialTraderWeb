"use strict";

define(['application-configuration', 'accountsService', 'alertsService'], function (app) {

    app.register.controller('createAccountController', ['$scope', '$rootScope', '$routeParams','accountsService', 'alertsService',
        function ($scope, $rootScope, $routeParams, accountsService, alertsService) {

            $scope.initializeController = function () {

                var accountID = ($routeParams.id || "");

                $rootScope.applicationModule = "Accountss";
                $rootScope.alerts = [];

                $scope.AccountID = accountID;

                if (accountID == "") {

                    $scope.FIRSTNAME = "";
                    $scope.LASTNAME = "";
                    $scope.EMAILADDRESS = "";
                    $scope.ADDRESS = "";
                    $scope.POSTTOSLACK = "";
                    $scope.SLACKBOTCHANNEL = "";
                    $scope.PASSWORD = "";
                   
                    $scope.setOriginalValues();

                    $scope.EditMode = true;
                    $scope.DisplayMode = false;

                    $scope.ShowCreateButton = true;
                    $scope.ShowEditButton = false;
                    $scope.ShowCancelButton = false;
                    $scope.ShowUpdateButton = false;

                }
                else {
                    var getAccount = new Object();
                    getAccount.AccountID = accountID;
                    accountsService.getAccount(getAccount, $scope.getAccountCompleted, $scope.getAccountError);
                }
            }

            $scope.getAccountCompleted = function (response) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;

                $scope.FIRSTNAME = response.Account.FIRSTNAME;
                $scope.LASTNAME = response.Account.LASTNAME;
                $scope.EMAILADDRESS = response.Account.EMAILADDRESS;
                $scope.ADDRESS = response.Account.ADDRESS;
                $scope.POSTTOSLACK = response.Account.POSTTOSLACK;
                $scope.SLACKBOTCHANNEL = response.Account.SLACKBOTCHANNEL;
                $scope.PASSWORD = response.Account.PASSWORD;
              
                $scope.setOriginalValues();
            }

            $scope.getAccountError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            $scope.clearValidationErrors = function () {

                $scope.EmailAddressInputError = false;
            }

            $scope.setOriginalValues = function () {

                $scope.OriginalFirstName = $scope.FIRSTNAME;
                $scope.OriginalLastName = $scope.LASTNAME;
                $scope.OriginalEmailAddress = $scope.EMAILADDRESS;
                $scope.OriginalAddress = $scope.ADDRESS;
                $scope.OriginalPostToSlack = $scope.POSTTOSLACK;
                $scope.OriginalSlackBotChannel = $scope.SLACKBOTCHANNEL;
                $scope.OriginalPassword = $scope.PASSWORD;  
            }

            $scope.resetValues = function () {

                $scope.FIRSTNAME = $scope.OriginalFirstName;
                $scope.LASTNAME = $scope.OriginalLastName;
                $scope.EMAILADDRESS = $scope.OriginalEmailAddress;
                $scope.ADDRESS = $scope.OriginalAddress;
                $scope.POSTTOSLACK = $scope.OriginalPostToSlack;
                $scope.SLACKBOTCHANNEL = $scope.OriginalSlackBotChannel;
                $scope.PASSWORD = $scope.OriginalPassword;
            }

            $scope.editAccount = function () {
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = false;
                $scope.ShowCancelButton = true;
                $scope.ShowUpdateButton = true;
                $scope.EditMode = true;
                $scope.DisplayMode = false;
            }

            $scope.cancelChanges = function () {

                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;
                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $rootScope.alerts = [];

                $scope.resetValues();
            }

            $scope.createAccount = function () {

                var account = $scope.createAccountObject();
                accountsService.createAccount(account, $scope.createAccountCompleted, $scope.createAccountError);
            }

            $scope.updateAccount = function () {
                var account = $scope.createAccountObject();
                account.AccountID = $scope.AccountID;
                accountsService.updateAccount(account, $scope.updateAccountCompleted, $scope.updateAccountError);
            }

            $scope.createAccountCompleted = function (response, status) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;

                $scope.AccountID = response.Account.AccountID;
                alertsService.RenderSuccessMessage(response.ReturnMessage);

                $scope.setOriginalValues();
            }

            $scope.createAccountError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }

            $scope.updateAccountCompleted = function (response, status) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;

                alertsService.RenderSuccessMessage(response.ReturnMessage);

                $scope.setOriginalValues();
            }

            $scope.updateAccountError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }


            $scope.createAccountObject = function () {

                var account = new Object();
                
                account.FIRSTNAME = $scope.FIRSTNAME;
                account.LASTNAME = $scope.LASTNAME;
                account.EMAILADDRESS = $scope.EMAILADDRESS;
                account.ADDRESS = $scope.ADDRESS;
                account.POSTTOSLACK = $scope.POSTTOSLACK;
                account.SLACKBOTCHANNEL = $scope.SLACKBOTCHANNEL;
                account.PASSWORD = $scope.PASSWORD;
               
                return account;
            }

        }]);
});

