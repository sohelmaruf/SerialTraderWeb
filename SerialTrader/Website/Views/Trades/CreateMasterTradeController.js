"use strict";

define(['application-configuration', 'masterTradesService', 'alertsService'], function (app) {

    app.register.controller('createMasterTradeController', ['$scope', '$rootScope', '$routeParams', 'masterTradesService', 'alertsService',
        function ($scope, $rootScope, $routeParams, masterTradesService, alertsService) {

            $scope.initializeController = function () {

                var masterID = ($routeParams.id || "");

                $rootScope.applicationModule = "MasterTrades";
                $rootScope.alerts = [];

                $scope.MASTERID = masterID;

                if (masterID == "") {

                    $scope.TRADETYPE = "";
                    $scope.EXCHANGE = "";
                    $scope.TRADINGPAIR = "";
                    $scope.BUYQUANTITY = "";
                    $scope.BUYPRICE = "";
                    $scope.BUYTOTAL = "";
                    $scope.SELLQUANTITY = "";
                    $scope.SELLPRICE = "";
                    $scope.SELLTOTAL = "";
                    $scope.RUNFREQUENCY = "";
                    $scope.RUNCOUNT = "";
                    $scope.ACTIVE = "";
                    
                    $scope.setOriginalValues();

                    $scope.EditMode = true;
                    $scope.DisplayMode = false;

                    $scope.ShowCreateButton = true;
                    $scope.ShowEditButton = false;
                    $scope.ShowCancelButton = false;
                    $scope.ShowUpdateButton = false;
                }
                else {
                    var createMasterTrade = new Object();
                    createMasterTrade.MASTERID = masterID;
                    masterTradesService.createMasterTrade(createMasterTrade, $scope.getMasterTradeCompleted, $scope.getMasterTradeError);
                }
            }

            $scope.getMasterTradeCompleted = function (response) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;

                $scope.TRADETYPE = response.MasterTrade.TRADETYPE
                $scope.EXCHANGE = response.MasterTrade.EXCHANGE
                $scope.TRADINGPAIR = response.MasterTrade.TRADINGPAIR
                $scope.BUYQUANTITY = response.MasterTrade.BUYQUANTITY
                $scope.BUYPRICE = response.MasterTrade.BUYPRICE
                $scope.BUYTOTAL = response.MasterTrade.BUYTOTAL
                $scope.SELLQUANTITY = response.MasterTrade.SELLQUANTITY
                $scope.SELLPRICE = response.MasterTrade.SELLPRICE
                $scope.SELLTOTAL = response.MasterTrade.SELLTOTAL
                $scope.RUNFREQUENCY = response.MasterTrade.RUNFREQUENCY
                $scope.RUNCOUNT = response.MasterTrade.RUNCOUNT
                $scope.ACTIVE = response.MasterTrade.ACTIVE
                
                $scope.setOriginalValues();
            }

            $scope.getMasterTradeError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            $scope.clearValidationErrors = function () {

                $scope.EmailAddressInputError = false;
            }

            $scope.setOriginalValues = function () {

                $scope.OriginalTRADETYPE = $scope.TRADETYPE;
                $scope.OriginalEXCHANGE = $scope.EXCHANGE;
                $scope.OriginalTRADINGPAIR = $scope.TRADINGPAIR;
                $scope.OriginalBUYQUANTITY = $scope.BUYQUANTITY;
                $scope.OriginalBUYPRICE = $scope.BUYPRICE;
                $scope.OriginalBUYTOTAL = $scope.BUYTOTAL;
                $scope.OriginalSELLQUANTITY = $scope.SELLQUANTITY;
                $scope.OriginalSELLPRICE = $scope.SELLPRICE;
                $scope.OriginalSELLTOTAL = $scope.SELLTOTAL;
                $scope.OriginalRUNFREQUENCY = $scope.RUNFREQUENCY;
                $scope.OriginalRUNCOUNT = $scope.RUNCOUNT;
                $scope.OriginalACTIVE = $scope.ACTIVE;
            }

            $scope.resetValues = function () {

                $scope.TRADETYPE = $scope.OriginalTRADETYPE
                $scope.EXCHANGE = $scope.OriginalEXCHANGE
                $scope.TRADINGPAIR = $scope.OriginalTRADINGPAIR
                $scope.BUYQUANTITY = $scope.OriginalBUYQUANTITY
                $scope.BUYPRICE = $scope.OriginalBUYPRICE
                $scope.BUYTOTAL = $scope.OriginalBUYTOTAL
                $scope.SELLQUANTITY = $scope.OriginalSELLQUANTITY
                $scope.SELLPRICE = $scope.OriginalSELLPRICE
                $scope.SELLTOTAL = $scope.OriginalSELLTOTAL
                $scope.RUNFREQUENCY = $scope.OriginalRUNFREQUENCY
                $scope.RUNCOUNT = $scope.OriginalRUNCOUNT
                $scope.ACTIVE = $scope.OriginalACTIVE
            }

            $scope.editMasterTrade = function () {
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

            $scope.createMasterTrade = function () {

                var masterTrade = $scope.createMasterTradeObject();
                masterTradesService.createMasterTrade(masterTrade, $scope.createMasterTradeCompleted, $scope.createMasterTradeError);
            }

            $scope.updateMasterTrade = function () {
                var masterTrade = $scope.createMasterTradeObject();
                masterTrade.MASTERID = $scope.MASTERID;
                masterTradesService.updateMasterTrade(masterTrade, $scope.updateMasterTradeCompleted, $scope.updateMasterTradeError);
            }

            $scope.createMasterTradeCompleted = function (response, status) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;

                $scope.MASTERID = response.MasterTrade.MASTERID;
                alertsService.RenderSuccessMessage(response.ReturnMessage);

                $scope.setOriginalValues();
            }

            $scope.createMasterTradeError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }

            $scope.updateMasterTradeCompleted = function (response, status) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;

                alertsService.RenderSuccessMessage(response.ReturnMessage);

                $scope.setOriginalValues();
            }

            $scope.updateMasterTradeError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }


            $scope.createMasterTradeObject = function () {

                var masterTrade = new Object();

                masterTrade.TRADETYPE = $scope.TRADETYPE
                masterTrade.EXCHANGE = $scope.EXCHANGE
                masterTrade.TRADINGPAIR = $scope.TRADINGPAIR
                masterTrade.BUYQUANTITY = $scope.BUYQUANTITY
                masterTrade.BUYPRICE = $scope.BUYPRICE
                masterTrade.BUYTOTAL = $scope.BUYTOTAL
                masterTrade.SELLQUANTITY = $scope.SELLQUANTITY
                masterTrade.SELLPRICE = $scope.SELLPRICE
                masterTrade.SELLTOTAL = $scope.SELLTOTAL
                masterTrade.RUNFREQUENCY = $scope.RUNFREQUENCY
                masterTrade.RUNCOUNT = $scope.RUNCOUNT
                masterTrade.ACTIVE = $scope.ACTIVE
                
                return masterTrade;
            }

        }]);
});

