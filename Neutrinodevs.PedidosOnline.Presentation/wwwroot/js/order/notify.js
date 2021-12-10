"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl(base_url+"/OrdersHub").build();

connection.start().then(function () {
    console.log("conectado globalmente");
}).catch(function (err) {
    return console.error(err.toString());
});