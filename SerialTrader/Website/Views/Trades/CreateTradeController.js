"use strict";

define(['application-configuration', 'tradesService', 'alertsService'], function (app) {

    app.register.controller('createTradeController', ['$scope', '$rootScope', '$routeParams', 'tradesService', 'alertsService',
        function ($scope, $rootScope, $routeParams, tradesService, alertsService) {

            $scope.initializeController = function () {

                var tID = ($routeParams.id || "");

                $rootScope.applicationModule = "Trades";
                $rootScope.alerts = [];

                $scope.TID = tID;

                if (tID == "") {

                    var trade = new Object();
                    trade.TID = tID;
                    tradesService.initializeTrade(trade, $scope.initializeTradeCompleted, $scope.initializeTradeError);

                    $scope.EXCHANGE = "";
                    $scope.TRADINGPAIR = "";
                    $scope.ACTUALRATE = "";
                    $scope.TRADINGQTY = "";
                    $scope.TRADINGPRICE = "";
                    $scope.TOTAL = "";
                    $scope.ORDERSTATUS = "";
                    $scope.ORDERRESULT = "";
                    $scope.RETRYIFCANCELED = "";
                    
                    $scope.setOriginalValues();

                    $scope.EditMode = true;
                    $scope.DisplayMode = false;

                    $scope.ShowCreateButton = true;
                    $scope.ShowEditButton = false;
                    $scope.ShowCancelButton = false;
                    $scope.ShowUpdateButton = false;

                }
                else {
                    var getTrade = new Object();
                    getTrade.TID = tID;
                    tradesService.getTrade(getTrade, $scope.getTradeCompleted, $scope.getTradeError);
                }
            }


            $scope.initializeTradeCompleted = function (response) {

                $scope.EditMode = true;
                $scope.DisplayMode = false;
                $scope.ShowCreateButton = true;
                $scope.ShowEditButton = false;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;
                $scope.ShowDetailsButton = false;
                
                $scope.EXCHANGE = response.Trade.EXCHANGE;
                $scope.TRADINGPAIR = response.Trade.TRADINGPAIR;
                $scope.ACTUALRATE = response.Trade.ACTUALRATE;
                $scope.TRADINGQTY = response.Trade.TRADINGQTY;
                $scope.TRADINGPRICE = response.Trade.TRADINGPRICE;
                $scope.TOTAL = response.Trade.TOTAL;
                $scope.ORDERSTATUS = response.Trade.ORDERSTATUS;
                $scope.ORDERRESULT = response.Trade.ORDERRESULT;
                $scope.RETRYIFCANCELED = response.Trade.RETRYIFCANCELED;


                $scope.Exchanges = response.Trade.Exchanges;

                $scope.setOriginalValues();
            }

            $scope.initializeTradeError = function (response) {
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


            $scope.getTradeCompleted = function (response) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;
                
                $scope.EXCHANGE = response.Trade.EXCHANGE;
                $scope.TRADINGPAIR = response.Trade.TRADINGPAIR;
                $scope.ACTUALRATE = response.Trade.ACTUALRATE;
                $scope.TRADINGQTY = response.Trade.TRADINGQTY;
                $scope.TRADINGPRICE = response.Trade.TRADINGPRICE;
                $scope.TOTAL = response.Trade.TOTAL;
                $scope.ORDERSTATUS = response.Trade.ORDERSTATUS;
                $scope.ORDERRESULT = response.Trade.ORDERRESULT;
                $scope.RETRYIFCANCELED = response.Trade.RETRYIFCANCELED;


                $scope.Exchanges = response.Trade.Exchanges;

                $scope.setDefaultExchange();
                $scope.setOriginalValues();
            }

            $scope.getTradeError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            $scope.clearValidationErrors = function () {

                $scope.EmailAddressInputError = false;
            }

            $scope.setOriginalValues = function () {

                $scope.OriginalEXCHANGE = $scope.EXCHANGE;
                $scope.OriginalTRADINGPAIR = $scope.TRADINGPAIR;
                $scope.OriginalACTUALRATE = $scope.ACTUALRATE;
                $scope.OriginalTRADINGQTY = $scope.TRADINGQTY;
                $scope.OriginalTRADINGPRICE = $scope.TRADINGPRICE;
                $scope.OriginalTOTAL = $scope.TOTAL;
                $scope.OriginalORDERSTATUS = $scope.ORDERSTATUS;
                $scope.OriginalORDERRESULT = $scope.ORDERRESULT;
                $scope.OriginalRETRYIFCANCELED = $scope.RETRYIFCANCELED;
            }

            $scope.resetValues = function () {

                $scope.EXCHANGE = $scope.OriginalEXCHANGE;
                $scope.TRADINGPAIR = $scope.OriginalTRADINGPAIR;
                $scope.ACTUALRATE = $scope.OriginalACTUALRATE;
                $scope.TRADINGQTY = $scope.OriginalTRADINGQTY;
                $scope.TRADINGPRICE = $scope.OriginalTRADINGPRICE;
                $scope.TOTAL = $scope.OriginalTOTAL;
                $scope.ORDERSTATUS = $scope.OriginalORDERSTATUS;
                $scope.ORDERRESULT = $scope.OriginalORDERRESULT;
                $scope.RETRYIFCANCELED = $scope.OriginalRETRYIFCANCELED;


                $scope.setDefaultExchange();
            }

            $scope.editTrade = function () {
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

            $scope.createTrade = function () {

                var trade = $scope.createTradeObject();
                tradesService.createTrade(trade, $scope.createTradeCompleted, $scope.createTradeError);
            }

            $scope.updateTrade = function () {
                var trade = $scope.createTradeObject();
                trade.TID = $scope.TID;
                tradesService.updateTrade(trade, $scope.updateTradeCompleted, $scope.updateTradeError);
            }

            $scope.createTradeCompleted = function (response, status) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;

                $scope.TID = response.trade.TID;
                alertsService.RenderSuccessMessage(response.ReturnMessage);


                $scope.setDefaultExchange();

                $scope.setOriginalValues();
            }

            $scope.createTradeError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }

            $scope.updateTradeCompleted = function (response, status) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;

                alertsService.RenderSuccessMessage(response.ReturnMessage);

                $scope.setOriginalValues();
            }

            $scope.updateTradeError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }


            $scope.createTradeObject = function () {
                var trade = new Object();

                trade.EXCHANGE = $scope.EXCHANGE;
                trade.TRADINGPAIR = $scope.TRADINGPAIR;
                trade.ACTUALRATE = $scope.ACTUALRATE;
                trade.TRADINGQTY = $scope.TRADINGQTY;
                trade.TRADINGPRICE = $scope.TRADINGPRICE;
                trade.TOTAL = $scope.TOTAL;
                trade.ORDERSTATUS = $scope.ORDERSTATUS;
                trade.ORDERRESULT = $scope.ORDERRESULT;
                trade.RETRYIFCANCELED = $scope.RETRYIFCANCELED;

                return trade;
            }

        }]);
});

