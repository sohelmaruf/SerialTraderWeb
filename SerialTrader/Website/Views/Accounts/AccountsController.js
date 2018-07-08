"use strict";

define(['application-configuration', 'accountsService', 'alertsService', 'dataGridService'], function (app) {

    app.register.controller('accountsController', ['$scope', '$rootScope', 'accountsService', 'alertsService', 'dataGridService',
        function ($scope, $rootScope, accountsService, alertsService, dataGridService) {

            $scope.initializeController = function () {

                $rootScope.applicationModule = "Accounts";

                dataGridService.initializeTableHeaders();

                dataGridService.addHeader("Account #", "ACCOUNTID");
                dataGridService.addHeader("First Name", "FIRSTNAME");
                dataGridService.addHeader("Last Name", "LASTNAME");
                dataGridService.addHeader("Email", "EMAILADDRESS");
                dataGridService.addHeader("Slack", "SLACKBOTCHANNEL");

                $scope.tableHeaders = dataGridService.setTableHeaders();
                $scope.defaultSort = dataGridService.setDefaultSort("FIRSTNAME");

                $scope.changeSorting = function (column) {

                    dataGridService.changeSorting(column, $scope.defaultSort, $scope.tableHeaders);

                    $scope.defaultSort = dataGridService.getSort();
                    $scope.SortDirection = dataGridService.getSortDirection();
                    $scope.SortExpression = dataGridService.getSortExpression();
                    $scope.CurrentPageNumber = 1;

                    $scope.getAccounts();

                };


                $scope.setSortIndicator = function (column) {
                    return dataGridService.setSortIndicator(column, $scope.defaultSort);
                };

                $scope.FirstName = "";
                $scope.LastName = "";

                $scope.PageSize = 15;
                $scope.SortDirection = "DESC";
                $scope.SortExpression = "FIRSTNAME";
                $scope.CurrentPageNumber = 1;

                $rootScope.closeAlert = dataGridService.closeAlert;

                $scope.Accounts = [];

                $scope.getAccounts();

            }

            $scope.accountInquiryCompleted = function (response, status) {

                alertsService.RenderSuccessMessage(response.ReturnMessage);
                $scope.accounts = response.Accounts;
                $scope.TotalAccounts = response.TotalRows;
                $scope.TotalPages = response.TotalPages;
            }

            $scope.searchAccounts = function () {
                $scope.CurrentPageNumber = 1;
                $scope.getAccounts();
            }

            $scope.pageChanged = function () {
                $scope.getAccounts();
            }

            $scope.getAccounts = function () {
                var accountInquiry = $scope.createAccountInquiryObject();
                accountsService.getAccounts(accountInquiry, $scope.accountInquiryCompleted, $scope.accountInquiryError);
            }

            $scope.accountInquiryError = function (response, status) {
                if (response.IsAuthenicated == false) {
                    window.location = "/index.html";
                }
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            $scope.createNew = function () {
                window.location = "#Accounts/CreateAccount";
            }

            $scope.resetSearchFields = function () {
                $scope.FirstName = "";
                $scope.LastName = "";
                $scope.getAccounts();
            }

            $scope.createAccountInquiryObject = function () {

                var accountInquiry = new Object();

                accountInquiry.FirstName = $scope.FirstName;
                accountInquiry.LastName = $scope.LastName;
                accountInquiry.CurrentPageNumber = $scope.CurrentPageNumber;
                accountInquiry.SortExpression = $scope.SortExpression;
                accountInquiry.SortDirection = $scope.SortDirection;
                accountInquiry.PageSize = $scope.PageSize;

                return accountInquiry;
            }
        }]);

});
