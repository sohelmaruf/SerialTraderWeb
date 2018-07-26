"use strict";

define(['application-configuration', 'contactsService', 'alertsService'], function (app) {

    app.register.controller('createContactController', ['$scope', '$rootScope', '$routeParams', 'contactsService', 'alertsService',
        function ($scope, $rootScope, $routeParams, contactsService, alertsService) {

            $scope.initializeController = function () {

                var contactID = ($routeParams.id || "");

                $rootScope.applicationModule = "Contacts";
                $rootScope.alerts = [];

                $scope.ContactID = contactID;

                if (contactID == "") {

                    $scope.FirstName = "";
                    $scope.LastName = "";
                    $scope.OfficePhone = "";
                    $scope.Address = "";
                    $scope.Email = "";

                    $scope.setOriginalValues();

                    $scope.EditMode = true;
                    $scope.DisplayMode = false;

                    $scope.ShowCreateButton = true;
                    $scope.ShowEditButton = false;
                    $scope.ShowCancelButton = false;
                    $scope.ShowUpdateButton = false;

                }
                else {
                    var getContact = new Object();
                    getContact.ContactID = contactID;
                    contactsService.getContact(getContact, $scope.getContactCompleted, $scope.getContactError);

                }

            }

            $scope.getContactCompleted = function (response) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;


                $scope.FirstName = response.Contact.FirstName;
                $scope.LastName = response.Contact.LastName;
                $scope.OfficePhone = response.Contact.OfficePhone;
                $scope.Address = response.Contact.Address;
                $scope.Email = response.Contact.Email;

                $scope.setOriginalValues();
            }

            $scope.getContactError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
            }

            $scope.clearValidationErrors = function () {

                $scope.EmailAddressInputError = false;
            }

            $scope.setOriginalValues = function () {


                $scope.OriginalFirstName = $scope.FirstName;
                $scope.OriginalLastName = $scope.LastName;
                $scope.OriginalOfficePhone = $scope.OfficePhone;
                $scope.OriginalAddress = $scope.Address;
                $scope.OriginalEmail = $scope.Email;
            }

            $scope.resetValues = function () {

                $scope.FirstName = $scope.OriginalFirstName;
                $scope.LastName = $scope.OriginalLastName;
                $scope.OfficePhone = $scope.OriginalOfficePhone;
                $scope.Address = $scope.OriginalAddress;
                $scope.Email = $scope.OriginalEmail;

            }

            $scope.editContact = function () {
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

            $scope.createContact = function () {

                var contact = $scope.createContactObject();
                contactsService.createContact(contact, $scope.createContactCompleted, $scope.createContactError);
            }

            $scope.updateContact = function () {
                var contact = $scope.createContactObject();
                contact.ContactID = $scope.ContactID;
                contactsService.updateContact(contact, $scope.updateContactCompleted, $scope.updateContactError);
            }

            $scope.createContactCompleted = function (response, status) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;

                $scope.ContactID = response.Contact.ContactID;
                alertsService.RenderSuccessMessage(response.ReturnMessage);

                $scope.setOriginalValues();
            }

            $scope.createContactError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }

            $scope.updateContactCompleted = function (response, status) {

                $scope.EditMode = false;
                $scope.DisplayMode = true;
                $scope.ShowCreateButton = false;
                $scope.ShowEditButton = true;
                $scope.ShowCancelButton = false;
                $scope.ShowUpdateButton = false;

                alertsService.RenderSuccessMessage(response.ReturnMessage);

                $scope.setOriginalValues();
            }

            $scope.updateContactError = function (response) {
                alertsService.RenderErrorMessage(response.ReturnMessage);
                $scope.clearValidationErrors();
                alertsService.SetValidationErrors($scope, response.ValidationErrors);
            }


            $scope.createContactObject = function () {

                var contact = new Object();


                contact.ID = $scope.ContactID;
                contact.FirstName = $scope.FirstName;
                contact.LastName = $scope.LastName;
                contact.OfficePhone = $scope.OfficePhone;
                contact.Address = $scope.Address;
                contact.Email = $scope.Email;

                return contact;
            }

        }]);
});

