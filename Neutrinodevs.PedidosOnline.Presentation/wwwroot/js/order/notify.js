"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/Pesca/OrdersHub").build();

connection.start().then(function () {
    console.log("conectado globalmente");
}).catch(function (err) {
    return console.error(err.toString());
});