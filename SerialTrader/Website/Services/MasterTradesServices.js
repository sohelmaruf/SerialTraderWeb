define(['application-configuration', 'ajaxService'], function (app) {

    app.register.service('masterTradesService', ['ajaxService', function (ajaxService) {
        
        this.createMasterTrade = function (masterTrade, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(masterTrade, "/api/masterTrades/CreateMasterTrade", successFunction, errorFunction);
        };


        this.updateMasterTrade = function (masterTrade, successFunction, errorFunction) {
            ajaxService.AjaxPost(masterTrade, "/api/masterTrades/UpdateMasterTrade", successFunction, errorFunction);
        };


        this.getMasterTrade = function (masterID, successFunction, errorFunction) {
            ajaxService.AjaxGetWithData(masterID, "/api/masterTrades/GetMasterTrade", successFunction, errorFunction);
        };

        this.getMasterTrades = function (masterTrade, successFunction, errorFunction) {
            ajaxService.AjaxPost(masterTrade, "/api/masterTrades/GetMasterTrades", successFunction, errorFunction);
        };


        this.getExchanges = function (successFunction, errorFunction) {
            ajaxService.AjaxPost("/api/exchanges/GetExchanges", successFunction, errorFunction);
        };

        this.getMarkets = function (successFunction, errorFunction) {
            ajaxService.AjaxPost("/api/markets/GetMarkets", successFunction, errorFunction);
        };

    }]);
});