define(['application-configuration', 'ajaxService'], function (app) {

    app.register.service('accountsService', ['ajaxService', function (ajaxService) {

        this.createAccount = function (account, successFunction, errorFunction) {
            ajaxService.AjaxPost(account, "/api/accounts/CreateAccount", successFunction, errorFunction);
        };

        this.createAccountDetailLineItem = function (account, successFunction, errorFunction) {
            ajaxService.AjaxPost(account, "/api/accounts/CreateAccountLineItem", successFunction, errorFunction);
        };

        this.updateAccountDetailLineItem = function (account, successFunction, errorFunction) {
            ajaxService.AjaxPost(account, "/api/accounts/UpdateAccountLineItem", successFunction, errorFunction);
        };

        this.deleteAccountDetailLineItem = function (account, successFunction, errorFunction) {
            ajaxService.AjaxPost(account, "/api/accounts/DeleteAccountLineItem", successFunction, errorFunction);
        };

        this.initializeAccount = function (account, successFunction, errorFunction) {
            ajaxService.AjaxPost(account, "/api/accounts/InitializeAccount", successFunction, errorFunction);
        };

        this.getAccounts = function (account, successFunction, errorFunction) {
            ajaxService.AjaxPost(account, "/api/accounts/GetAccounts", successFunction, errorFunction);
        };

        this.getAccount = function (accountID, successFunction, errorFunction) {
            ajaxService.AjaxGetWithData(accountID, "/api/accounts/GetAccount", successFunction, errorFunction);
        };

        this.updateAccount = function (account, successFunction, errorFunction) {
            ajaxService.AjaxPost(account, "/api/accounts/UpdateAccount", successFunction, errorFunction);
        };

    }]);
});