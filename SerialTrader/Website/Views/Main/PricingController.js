"use strict";

define(['application-configuration'], function (app) {

    app.register.controller('pricingController', ['$scope', '$rootScope', function ($scope, $rootScope) {

        $scope.initializeController = function () {

            $rootScope.applicationModule = "Main";
          
        }


    }]);
});
