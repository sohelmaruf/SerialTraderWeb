"use strict";

define(['application-configuration', 'apiService', 'alertsService', 'dataGridService'], function (app) {

    app.register.controller('apiController', ['$scope', '$rootScope', 'apiService', 'alertsService', 'dataGridService',
        function ($scope, $rootScope, apiService, alertsService, dataGridService) {

            $scope.initializeController = function () {

                $rootScope.applicationModule = "APIs";

                dataGridService.initializeTableHeaders();

                dataGridService.addHeader("Key ID #", "KEYID");
                dataGridService.addHeader("Account", "ACCOUNTID");
                dataGridService.addHeader("Exchange", "EXCHANGE");
                dataGridService.addHeader("Api-Key", "APIKEY");
                dataGridService.addHeader("Api Secret", "APISECRET");
                dataGridService.addHeader("Pass Phase", "PASSPHRASE");


                $scope.tableHeaders = dataGridService.setTableHeaders();
                $scope.defaultSort = dataGridService.setDefaultSort("KEYID");

                $scope.changeSorting = function (column) {

                    dataGridService.changeSorting(column, $scope.defaultSort, $scope.tableHeaders);

                    $scope.defaultSort = dataGridService.getSort();
                    $scope.SortDirection = dataGridService.getSortDirection();
                    $scope.SortExpression = dataGridService.getSortExpression();
                    $scope.CurrentPageNumber = 1;

                    $scope.getAPIs();
                };


                $scope.setSortIndicator = function (column) {
                    return dataGridService.setSortIndicator(column, $scope.defaultSort);
                };

                $scope.Exchange = "";
                $scope.ApiKey = "";

                $scope.PageSize = 15;
                $scope.SortDirection = "DESC";
                $scope.SortExpression = "KEYID";
                $scope.CurrentPageNumber = 1;

                $rootScope.closeAlert = dataGridService.closeAlert;

                $scope.keys = [];

                $scope.getAPIs();

            }

            $scope.APIInquiryCompleted = function (response, status) {

                alertsService.RenderSuccessMessage(response.ReturnMessage);
                $scope.keys = response.Keys;
                $scope.TotalAPIs = response.TotalRows;
                $scope.TotalPages = response.TotalPages;
            }

            $scope.searchAPIs = function () {
                $scope.CurrentPageNumber = 1;
                $scope.getAPIs();
            }

            $scope.pageChanged = function () {
                $scope.getAPIs();
            }

            $scope.createNew = function () {
                window.location = "#API/CreateAPI";
            }

            $scope.getAPIs = function () {
                var apiInquiry = $scope.createAPIInquiryObject();
                apiService.getAPIs(apiInquiry, $scope.APIInquiryCompleted, $scope.APIInquiryError);
            }

            $scope.APIInquiryError = function (response, status) {
                if (response.IsAuthenicated == false) {
                    window.location = "/index.html";
                }
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            $scope.resetSearchFields = function () {
                $scope.Exchange = "";
                $scope.ApiKey = "";
                $scope.getAPIs();
            }

            $scope.createAPIInquiryObject = function () {

                var apiInquiry = new Object();

                apiInquiry.KEYID = $scope.KEYID;
                apiInquiry.ACCOUNTID = $scope.ACCOUNTID;
                apiInquiry.EXCHANGE = $scope.EXCHANGE;
                apiInquiry.APIKEY = $scope.APIKEY;
                apiInquiry.APISECRET = $scope.APISECRET;
                apiInquiry.PASSPHRASE = $scope.PASSPHRASE;

                return apiInquiry;
            }
        }]);

});
