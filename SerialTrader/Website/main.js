﻿/// <reference path="Scripts/ui-bootstrap-tpls-0.11.0.js" />
/// <reference path="Scripts/ui-bootstrap-tpls-0.11.0.js" />
/// <reference path="Scripts/ui-bootstrap-tpls-0.11.0.js" />
require.config({

    baseUrl: "",

    // alias libraries paths
    paths: {
        'application-configuration': 'scripts/application-configuration',       
        'angular': 'scripts/angular',
        'angular-route': 'scripts/angular-route',
        'angularAMD': 'scripts/angularAMD',
         'ui-bootstrap' : 'scripts/ui-bootstrap-tpls-0.11.0',
        'blockUI': 'scripts/angular-block-ui',
        'ngload': 'scripts/ngload',       
        'mainService': 'services/mainServices',
        'ajaxService': 'services/ajaxServices',
        'alertsService': 'services/alertsServices',
        'adminService': 'services/AdminServices',
        'contactsService': 'services/contactsServices',
        'helpService': 'services/HelpServices',
        'dataGridService': 'services/DataGridService',


        'apiService': 'services/apiServices',
        'tradesService': 'services/tradesServices',
        'masterTradesService': 'services/masterTradesServices',
        'accountsService': 'services/accountsServices',
        'angular-sanitize': 'scripts/angular-sanitize'
    },

    //'customersService': 'services/customersServices',
    //'ordersService': 'services/ordersServices',
    //'productsService': 'services/productsServices',
    //'customersController': 'Views/Shared/CustomersController',
    //'productLookupModalController': 'Views/Shared/ProductLookupModalController'

    // Add angular modules that does not support AMD out of the box, put it in a shim
    shim: {
        'angularAMD': ['angular'],
        'angular-route': ['angular'],
        'blockUI': ['angular'],
        'angular-sanitize': ['angular'],
        'ui-bootstrap': ['angular']
         
    },

    // kick start application
    deps: ['application-configuration']
});
