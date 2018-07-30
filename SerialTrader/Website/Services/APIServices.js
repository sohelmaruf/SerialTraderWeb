define(['application-configuration', 'ajaxService'], function (app) {

    app.register.service('apiService', ['ajaxService', function (ajaxService) {
        
        this.initializeAPI = function (api, successFunction, errorFunction) {
            ajaxService.AjaxPost(api, "/api/keys/InitializeAPI", successFunction, errorFunction);
        };

        this.createAPI = function (api, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(api, "/api/keys/CreateAPI", successFunction, errorFunction);
        };

        this.updateAPI = function (api, successFunction, errorFunction) {
            ajaxService.AjaxPost(api, "/api/keys/UpdateAPI", successFunction, errorFunction);
        };
        
        this.getAPI = function (keyID, successFunction, errorFunction) {
            ajaxService.AjaxGetWithData(keyID, "/api/keys/GetAPI", successFunction, errorFunction);
        };
        
        this.getAPIs = function (api, successFunction, errorFunction) {
            ajaxService.AjaxPost(api, "/api/keys/GetAPIs", successFunction, errorFunction);
        };

    }]);
});