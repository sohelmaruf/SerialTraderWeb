"use strict";

define(['application-configuration', 'apiService', 'alertsService'], function (app) {

    app.register.controller('createAPIController', ['$scope', '$rootScope', '$routeParams', 'apiService', 'alertsService',
        function ($scope, $rootScope, $routeParams, apiService, alertsService) {

            $scope.initializeController = function () {

                var keyID = ($routeParams.id || "");

                $rootScope.applicationModule = "APIs";
                $rootScope.alerts = [];

                $scope.KEYID = keyID;

                if (keyID == "") {

                    var key = new Object();
                    key.KEYID = keyID;
                   // apiService.initializeAPI(key, $scope.initializeAPICompleted, $scope.initializeAPIError);

                    $scope.ACCOUNTID = "";
                    $scope.EXCHANGE = "";
                    $scope.APIKEY = "";
                    $scope.APISECRET = "";
                    $scope.PASSPHRASE = "";
                    
                    $scope.setOriginalValues();

                    $scope.EditMode = true;
                    $scope.DisplayMode = false;

                    $scope.ShowCreateButton = true;
                    $scope.ShowEditButton = false;
                    $scope.ShowCancelButton = false;
                    $scope.ShowUpdateButton = false;

                }
                else {
                    var gAPI = new Object();
                    gAPI.KEYID = keyID;
                    apiService.getAPI(gAPI, $scope.getAPICompleted, $scope.getAPIError);
                }
            }


            $scope.initializeAPICompleted = function (response) {

                $scope.EditMode = true;
                $scope.DisplayMode = false;
                $scope.ShowCreateButton = true;
                $scope.ShowEditButton = false;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;
                $scope.ShowDetailsButton = false;

                $scope.ACCOUNTID = "";
                $scope.EXCHANGE = "";
                $scope.APIKEY = "";
                $scope.APISECRET = "";
                $scope.PASSPHRASE = "";

                $scope.ACCOUNTID = response.Key.ACCOUNTID;
                $scope.EXCHANGE = response.Key.EXCHANGE;
                $scope.APIKEY = response.Key.APIKEY;
                $scope.APISECRET = response.Key.APISECRET;
                $scope.PASSPHRASE = response.Key.PASSPHRASE;
             

                $scope.Exchanges = response.Key.Exchanges;

                $scope.setOriginalValues();
            }

            $scope.initializeAPIError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            
            $scope.setDefaultExchange = function () {

                for (var i = 0; i < $scope.Exchanges.length; i++) {
                    if ($scope.Exchanges[i].EXCHANGEID == $scope.EXCHANGEID) {
                        $scope.EXCHANGE = $scope.Exchanges[i];
                        $scope.selectedExchange = $scope.Exchanges[i].EXCHANGE;
                        break;
                    }
                }

            }


            $scope.getAPICompleted = function (response) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;
                
                $scope.ACCOUNTID = response.Key.ACCOUNTID;
                $scope.EXCHANGE = response.Key.EXCHANGE;
                $scope.APIKEY = response.Key.APIKEY;
                $scope.APISECRET = response.Key.APISECRET;
                $scope.PASSPHRASE = response.Key.PASSPHRASE;

                $scope.Exchanges = response.Key.Exchanges;

                $scope.setDefaultExchange();
                $scope.setOriginalValues();
            }

            $scope.getAPIError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            $scope.clearValidationErrors = function () {

                $scope.EmailAddressInputError = false;
            }

            $scope.setOriginalValues = function () {

                $scope.OriginalACCOUNTID = $scope.ACCOUNTID;
                $scope.OriginalEXCHANGE = $scope.EXCHANGE;
                $scope.OriginalAPIKEY = $scope.APIKEY;
                $scope.OriginalAPISECRET = $scope.APISECRET;
                $scope.OriginalPASSPHRASE = $scope.PASSPHRASE;
            }

            $scope.resetValues = function () {

                $scope.ACCOUNTID = $scope.OriginalACCOUNTID;
                $scope.EXCHANGE = $scope.OriginalEXCHANGE;
                $scope.APIKEY = $scope.OriginalAPIKEY;
                $scope.APISECRET = $scope.OriginalAPISECRET;
                $scope.PASSPHRASE = $scope.OriginalPASSPHRASE;

                $scope.setDefaultExchange();
            }

            $scope.editAPI = function () {
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

            $scope.createAPI = function () {

                var key = $scope.createKeyObject();
                apiService.createAPI(key, $scope.createAPICompleted, $scope.createAPIError);
            }

            $scope.updateAPI = function () {
                var key = $scope.createKeyObject();
                key.KEYID = $scope.keyID;
                apiService.updateAPI(key, $scope.updateAPICompleted, $scope.updateAPIError);
            }

            $scope.createAPICompleted = function (response, status) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;

                $scope.KEYID = response.key.KEYID;
                alertsService.RenderSuccessMessage(response.ReturnMessage);


                $scope.setDefaultExchange();

                $scope.setOriginalValues();
            }

            $scope.createAPIError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }

            $scope.updateAPICompleted = function (response, status) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;

                alertsService.RenderSuccessMessage(response.ReturnMessage);

                $scope.setOriginalValues();
            }

            $scope.updateAPIError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }


            $scope.createKeyObject = function () {
                var key = new Object();

                key.ACCOUNTID = $scope.ACCOUNTID;
                key.EXCHANGE = $scope.EXCHANGE;
                key.APIKEY = $scope.APIKEY;
                key.APISECRET = $scope.APISECRET;
                key.PASSPHRASE = $scope.PASSPHRASE;

                return key;
            }

        }]);
});

