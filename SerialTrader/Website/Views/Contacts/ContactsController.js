"use strict";

define(['application-configuration', 'contactsService', 'alertsService', 'dataGridService'], function (app) {

    app.register.controller('contactsController', ['$scope', '$rootScope', 'contactsService', 'alertsService', 'dataGridService',
        function ($scope, $rootScope, contactsService, alertsService, dataGridService) {

            $scope.initializeController = function () {

                $rootScope.applicationModule = "Contacts";

                dataGridService.initializeTableHeaders();

                dataGridService.addHeader("Contact #", "ID");
                dataGridService.addHeader("First Name", "FirstName");
                dataGridService.addHeader("Last Name", "LastName");
                dataGridService.addHeader("Address", "Address");
                dataGridService.addHeader("Phone", "HomePhone");
                dataGridService.addHeader("Email", "Email");
                dataGridService.addHeader("City", "City");
                dataGridService.addHeader("State", "State");
                dataGridService.addHeader("Zipcode", "Zipcode");
                
                $scope.tableHeaders = dataGridService.setTableHeaders();
                $scope.defaultSort = dataGridService.setDefaultSort("FirstName");

                $scope.changeSorting = function (column) {

                    dataGridService.changeSorting(column, $scope.defaultSort, $scope.tableHeaders);

                    $scope.defaultSort = dataGridService.getSort();
                    $scope.SortDirection = dataGridService.getSortDirection();
                    $scope.SortExpression = dataGridService.getSortExpression();
                    $scope.CurrentPageNumber = 1;

                    $scope.getContacts();
                };


                $scope.setSortIndicator = function (column) {
                    return dataGridService.setSortIndicator(column, $scope.defaultSort);
                };

                $scope.FirstName = "";
                $scope.LastName = "";

                $scope.PageSize = 15;
                $scope.SortDirection = "DESC";
                $scope.SortExpression = "FirstName";
                $scope.CurrentPageNumber = 1;

                $rootScope.closeAlert = dataGridService.closeAlert;

                $scope.contacts = [];

                $scope.getContacts();

            }

            $scope.contactInquiryCompleted = function (response, status) {

                alertsService.RenderSuccessMessage(response.ReturnMessage);
                $scope.contacts = response.Contacts;
                $scope.TotalContacts = response.TotalRows;
                $scope.TotalPages = response.TotalPages;
            }

            $scope.searchContacts = function () {
                $scope.CurrentPageNumber = 1;
                $scope.getContacts();
            }

            $scope.pageChanged = function () {
                $scope.getContacts();
            }

            $scope.createNew = function () {
                window.location = "#Contacts/CreateContact";
            }

            $scope.getContacts= function () {
                var contactInquiry = $scope.createContactInquiryObject();
                contactsService.getContacts(contactInquiry, $scope.contactInquiryCompleted, $scope.contactInquiryError);
            }

            $scope.contactInquiryError = function (response, status) {
                if (response.IsAuthenicated == false) {
                    window.location = "/index.html";
                }
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            $scope.resetSearchFields = function () {
                $scope.FirstName = "";
                $scope.LastName = "";
                $scope.getContacts();
            }

            $scope.createContactInquiryObject = function () {

                var contactInquiry = new Object();

                contactInquiry.FirstName = $scope.FirstName;
                contactInquiry.LastName = $scope.LastName;
                contactInquiry.CurrentPageNumber = $scope.CurrentPageNumber;
                contactInquiry.SortExpression = $scope.SortExpression;
                contactInquiry.SortDirection = $scope.SortDirection;
                contactInquiry.PageSize = $scope.PageSize;

                return contactInquiry;
            }
        }]);

});
