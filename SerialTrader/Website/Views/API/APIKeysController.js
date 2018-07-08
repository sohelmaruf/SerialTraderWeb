"use strict";

define(['application-configuration', 'tradesService', 'alertsService', 'dataGridService'], function (app) {

    app.register.controller('tradesController', ['$scope', '$rootScope', 'tradesService', 'alertsService', 'dataGridService',
        function ($scope, $rootScope, tradesService, alertsService, dataGridService) {

            $scope.initializeController = function () {

                $rootScope.applicationModule = "Trades";

                dataGridService.initializeTableHeaders();

                dataGridService.addHeader("ID #", "TID");
                dataGridService.addHeader("API", "EXCHANGE");
                dataGridService.addHeader("KEY", "ORDERRESULT");


                $scope.tableHeaders = dataGridService.setTableHeaders();
                $scope.defaultSort = dataGridService.setDefaultSort("OrderID");

                $scope.changeSorting = function (column) {

                    dataGridService.changeSorting(column, $scope.defaultSort, $scope.tableHeaders);

                    $scope.defaultSort = dataGridService.getSort();
                    $scope.SortDirection = dataGridService.getSortDirection();
                    $scope.SortExpression = dataGridService.getSortExpression();
                    $scope.CurrentPageNumber = 1;

                    $scope.getTrades();
                };


                $scope.setSortIndicator = function (column) {
                    return dataGridService.setSortIndicator(column, $scope.defaultSort);
                };

                $scope.OrderID = "";
                $scope.OrderStatus = "";

                $scope.PageSize = 15;
                $scope.SortDirection = "DESC";
                $scope.SortExpression = "ORDERID";
                $scope.CurrentPageNumber = 1;

                $rootScope.closeAlert = dataGridService.closeAlert;

                $scope.trades = [];

                $scope.getTrades();

            }

            $scope.tradeInquiryCompleted = function (response, status) {

                alertsService.RenderSuccessMessage(response.ReturnMessage);
                $scope.trades = response.Trades;
                $scope.TotalTrades = response.TotalRows;
                $scope.TotalPages = response.TotalPages;
            }

            $scope.searchTrades = function () {
                $scope.CurrentPageNumber = 1;
                $scope.getTrades();
            }

            $scope.pageChanged = function () {
                $scope.getTrades();
            }

            $scope.createNew = function () {
                window.location = "#Trades/CreateTrade";
            }

            $scope.getTrades = function () {
                var tradeInquiry = $scope.createTradeInquiryObject();
                tradesService.getTrades(tradeInquiry, $scope.tradeInquiryCompleted, $scope.tradeInquiryError);
            }

            $scope.tradeInquiryError = function (response, status) {
                if (response.IsAuthenicated == false) {
                    window.location = "/index.html";
                }
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            $scope.resetSearchFields = function () {
                $scope.OrderID = "";
                $scope.OrderStatus = "";
                $scope.getTrades();
            }

            $scope.createTradeInquiryObject = function () {

                var tradeInquiry = new Object();

                tradeInquiry.OrderID = $scope.OrderID;
                tradeInquiry.OrderStatus = $scope.OrderStatus;
                tradeInquiry.CurrentPageNumber = $scope.CurrentPageNumber;
                tradeInquiry.SortExpression = $scope.SortExpression;
                tradeInquiry.SortDirection = $scope.SortDirection;
                tradeInquiry.PageSize = $scope.PageSize;

                return tradeInquiry;
            }
        }]);

});
