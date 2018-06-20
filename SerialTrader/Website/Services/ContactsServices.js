define(['application-configuration', 'ajaxService'], function (app) {

    app.register.service('contactsService', ['ajaxService', function (ajaxService) {

        this.createContact = function (contact, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(contact, "/api/contacts/CreateContact", successFunction, errorFunction);
        };

        this.getContact = function (successFunction, errorFunction) {
            ajaxService.AjaxGet("/api/contacts/GetContact", successFunction, errorFunction);
        };        

        this.updateContact = function (contact, successFunction, errorFunction) {
            ajaxService.AjaxPost(contact, "/api/contacts/UpdateContact", successFunction, errorFunction);
        };

    }]);
});