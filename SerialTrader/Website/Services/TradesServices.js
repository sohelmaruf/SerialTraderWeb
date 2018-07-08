define(['application-configuration', 'ajaxService'], function (app) {

    app.register.service('tradesService', ['ajaxService', function (ajaxService) {

        this.createTrade = function (trade, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(trade, "/api/trades/CreateTrade", successFunction, errorFunction);
        };

        this.updateTrade = function (trade, successFunction, errorFunction) {
            ajaxService.AjaxPost(trade, "/api/trades/UpdateTrade", successFunction, errorFunction);
        };

        this.getTrade = function (successFunction, errorFunction) {
            ajaxService.AjaxGet("/api/trades/GetTrade", successFunction, errorFunction);
        };

        this.getTrades = function (trade, successFunction, errorFunction) {
            ajaxService.AjaxPost(trade, "/api/trades/GetTrades", successFunction, errorFunction);
        };

    }]);
});