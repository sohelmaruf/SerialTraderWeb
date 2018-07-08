"use strict";

define(['application-configuration', 'masterTradesService', 'alertsService', 'dataGridService'], function (app) {

    app.register.controller('masterTradesController', ['$scope', '$rootScope', 'masterTradesService', 'alertsService', 'dataGridService',
        function ($scope, $rootScope, masterTradesService, alertsService, dataGridService) {

            $scope.initializeController = function () {

                $rootScope.applicationModule = "MasterTrades";

                dataGridService.initializeTableHeaders();


                dataGridService.addHeader("ID #", "MASTERID");
                dataGridService.addHeader("Type", "TRADETYPE");
                dataGridService.addHeader("Exchange", "EXCHANGE");
                dataGridService.addHeader("Paid", "TRADINGPAIR");
                dataGridService.addHeader("Buy Price", "BUYPRICE");
                dataGridService.addHeader("Buy Total", "BUYTOTAL");
                dataGridService.addHeader("Sell Price", "SELLPRICE");
                dataGridService.addHeader("Sell Total", "SELLTOTAL");
                dataGridService.addHeader("Sell Qnt", "SELLQUANTITY");
                dataGridService.addHeader("Action", "FIRSTACTION");




                $scope.tableHeaders = dataGridService.setTableHeaders();
                $scope.defaultSort = dataGridService.setDefaultSort("TradeType");

                $scope.changeSorting = function (column) {

                    dataGridService.changeSorting(column, $scope.defaultSort, $scope.tableHeaders);

                    $scope.defaultSort = dataGridService.getSort();
                    $scope.SortDirection = dataGridService.getSortDirection();
                    $scope.SortExpression = dataGridService.getSortExpression();
                    $scope.CurrentPageNumber = 1;

                    $scope.getMasterTrades();
                };


                $scope.setSortIndicator = function (column) {
                    return dataGridService.setSortIndicator(column, $scope.defaultSort);
                };

                $scope.TradeType = "";
                $scope.TradingPair = "";

                $scope.PageSize = 15;
                $scope.SortDirection = "DESC";
                $scope.SortExpression = "TRADETYPE";
                $scope.CurrentPageNumber = 1;

                $rootScope.closeAlert = dataGridService.closeAlert;

                $scope.mastertrades = [];

                $scope.getMasterTrades();

            }

            $scope.mastertradeInquiryCompleted = function (response, status) {

                alertsService.RenderSuccessMessage(response.ReturnMessage);
                $scope.mastertrades = response.MasterTrades;
                $scope.TotalMasterTrades = response.TotalRows;
                $scope.TotalPages = response.TotalPages;
            }

            $scope.searchMasterTrades = function () {
                $scope.CurrentPageNumber = 1;
                $scope.getMasterTrades();
            }

            $scope.pageChanged = function () {
                $scope.getMasterTrades();
            }

            $scope.getMasterTrades = function () {
                var mastertradeInquiry = $scope.createMasterTradeInquiryObject();
                masterTradesService.getMasterTrades(mastertradeInquiry, $scope.mastertradeInquiryCompleted, $scope.mastertradeInquiryError);
            }

            $scope.mastertradeInquiryError = function (response, status) {
                if (response.IsAuthenicated == false) {
                    window.location = "/index.html";
                }
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            $scope.resetSearchFields = function () {
                $scope.TradeType = "";
                $scope.TradingPair = "";
                $scope.getMasterTrades();
            }

            $scope.createNew = function () {
                window.location = "#Trades/CreateMasterTrade";
            }

            $scope.createMasterTradeInquiryObject = function () {

                var mastertradeInquiry = new Object();

                mastertradeInquiry.TradeType = $scope.TradeType;
                mastertradeInquiry.TradingPair = $scope.TradingPair;
                mastertradeInquiry.CurrentPageNumber = $scope.CurrentPageNumber;
                mastertradeInquiry.SortExpression = $scope.SortExpression;
                mastertradeInquiry.SortDirection = $scope.SortDirection;
                mastertradeInquiry.PageSize = $scope.PageSize;

                return mastertradeInquiry;
            }
        }]);

});
