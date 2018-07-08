define(['application-configuration', 'ajaxService'], function (app) {

    app.register.service('masterTradesService', ['ajaxService', function (ajaxService) {

        this.createMasterTrade = function (masterTrade, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(trade, "/api/masterTrades/CreateMasterTrade", successFunction, errorFunction);
        };

        this.updateMasterTrade = function (masterTrade, successFunction, errorFunction) {
            ajaxService.AjaxPost(masterTrade, "/api/masterTrades/UpdateMasterTrade", successFunction, errorFunction);
        };

        this.getMasterTrade = function (successFunction, errorFunction) {
            ajaxService.AjaxGet("/api/masterTrades/GetMasterTrade", successFunction, errorFunction);
        };

        this.getMasterTrades = function (masterTrade, successFunction, errorFunction) {
            ajaxService.AjaxPost(masterTrade, "/api/masterTrades/GetMasterTrades", successFunction, errorFunction);
        };

    }]);
});