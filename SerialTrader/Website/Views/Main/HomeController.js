"use strict";


define(['application-configuration', 'contactsService', 'alertsService'], function (app) {

    app.register.controller('homeController', ['$scope', '$rootScope', 'contactsService', 'alertsService',
        function ($scope, $rootScope, contactsService, alertsService) {
            
        
        $rootScope.applicationModule = "Main";
        
        $scope.initializeController = function () {

            $scope.FullName = "";
            $scope.OfficePhone = "";
        }

        $scope.registerContact = function () {
            var contact = $scope.createContact();
            contactsService.createContact(contact, $scope.registerContactCompleted, $scope.registerContactError);
        }

        $scope.registerContactCompleted = function (response) {

            $scope.FullName = "";
            $scope.OfficePhone = "";
        }

        $scope.registerContactError = function (response) {
            

        }

        $scope.createContact = function () {

            var contact = new Object();

            contact.FullName = $scope.FullName;
            contact.OfficePhone = $scope.OfficePhone;

            return contact;
        }

    }]);

});
