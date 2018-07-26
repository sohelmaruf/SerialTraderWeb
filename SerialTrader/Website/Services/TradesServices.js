define(['application-configuration', 'ajaxService'], function (app) {

    app.register.service('tradesService', ['ajaxService', function (ajaxService) {
        
        this.initializeTrade = function (trade, successFunction, errorFunction) {
            ajaxService.AjaxPost(trade, "/api/trades/InitializeTrade", successFunction, errorFunction);
        };

        this.createTrade = function (trade, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(trade, "/api/trades/CreateTrade", successFunction, errorFunction);
        };

        this.updateTrade = function (trade, successFunction, errorFunction) {
            ajaxService.AjaxPost(trade, "/api/trades/UpdateTrade", successFunction, errorFunction);
        };
        
        this.getTrade = function (tID, successFunction, errorFunction) {
            ajaxService.AjaxGetWithData(tID, "/api/trades/GetTrade", successFunction, errorFunction);
        };
        
        this.getTrades = function (trade, successFunction, errorFunction) {
            ajaxService.AjaxPost(trade, "/api/trades/GetTrades", successFunction, errorFunction);
        };

        this.getExchanges = function (successFunction, errorFunction) {
            ajaxService.AjaxPost("/api/exchanges/GetExchanges", successFunction, errorFunction);
        };

        this.getMarkets = function (successFunction, errorFunction) {
            ajaxService.AjaxPost("/api/markets/GetMarkets", successFunction, errorFunction);
        };

    }]);
});