define(['application-configuration', 'ajaxService'], function (app) {

    app.register.service('contactsService', ['ajaxService', function (ajaxService) {

        this.createContact = function (contact, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(contact, "/api/contacts/CreateContact", successFunction, errorFunction);
        };

        this.updateContact = function (contact, successFunction, errorFunction) {
            ajaxService.AjaxPost(contact, "/api/contacts/UpdateContact", successFunction, errorFunction);
        };
        
        this.getContact = function (contactID, successFunction, errorFunction) {
            ajaxService.AjaxGetWithData(contactID, "/api/contacts/GetContact", successFunction, errorFunction);
        };


        this.getContacts = function (contact, successFunction, errorFunction) {
            ajaxService.AjaxPost(contact, "/api/contacts/GetContacts", successFunction, errorFunction);
        };

    }]);
});