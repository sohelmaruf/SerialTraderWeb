"use strict";

define(['application-configuration', 'adminService', 'alertsService'], function (app) {

    app.register.controller('passwordRecoveryController', ['$scope', '$rootScope', 'adminService', 'alertsService',
        function ($scope, $rootScope, adminService, alertsService) {

            $rootScope.closeAlert = alertsService.closeAlert;
            $rootScope.applicationModule = "Main";
            $rootScope.alerts = [];

            $scope.initializeController = function () {

                $rootScope.applicationModule = "Main";
            }

    }]);
});
