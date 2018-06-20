define(['application-configuration', 'ajaxService'], function (app) {

    app.register.service('adminService', ['ajaxService', function (ajaxService) {

        this.registerClient = function (register, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(register, "/api/admin/RegisterClient", successFunction, errorFunction);
        };

        this.login = function (user, successFunction, errorFunction) {
          ajaxService.AjaxPostWithNoAuthenication(user, "/api/admin/Login", successFunction, errorFunction);
        };

        //this.getUser = function (successFunction, errorFunction) {
        //  ajaxService.AjaxGet("/api/admin/GetUser", successFunction, errorFunction);
        //};        

        //this.updateUser = function (user, successFunction, errorFunction) {
        //  ajaxService.AjaxPost(user, "/api/admin/UpdateUser", successFunction, errorFunction);
        //};

    }]);
});