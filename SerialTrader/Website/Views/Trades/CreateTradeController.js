"use strict";

define(['application-configuration', 'contactsService', 'alertsService'], function (app) {

    app.register.controller('contactController', ['$scope', '$rootScope', 'contactsService', 'alertsService',
        function ($scope, $rootScope, contactsService, alertsService) {

            $rootScope.closeAlert = alertsService.closeAlert;
            $rootScope.applicationModule = "Main";
            $rootScope.alerts = [];

            $scope.initializeController = function () {

                $scope.FullName = "";
                $scope.Email = "";
                $scope.OfficePhone = "";
                $scope.Organization = "";
                $scope.Address = "";
                $scope.Comments = "";
                $scope.AllowNewsLetter = "";
            }

            $scope.registerContact = function () {
                var contact = $scope.createContact();
                contactsService.createContact(contact, $scope.registerContactCompleted, $scope.registerContactError);
            }

            $scope.registerContactCompleted = function (response) {

                $scope.FullName = "";
                $scope.Email = "";
                $scope.OfficePhone = "";
                $scope.Organization = "";
                $scope.Address = "";
                $scope.Comments = "";
                $scope.AllowNewsLetter = "";

                alertsService.RenderSuccessMessage("Your message has been sent to WaterCons, a representative will contact you soon.");
                //window.location = "/index.html#/Main/Contact";
            }

            $scope.registerContactError = function (response) {

                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }

            $scope.clearValidationErrors = function () {
                $scope.EmailAddressInputError = false;
            }

            $scope.createContact = function () {

                var contact = new Object();

                contact.FullName = $scope.FullName;
                contact.Email = $scope.Email;
                contact.OfficePhone = $scope.OfficePhone;
                contact.Organization = $scope.Organization;
                contact.Address = $scope.Address;
                contact.Comments = $scope.Comments;
                contact.AllowNewsLetter = $scope.AllowNewsLetter;

                return contact;
            }

        }]);
});

