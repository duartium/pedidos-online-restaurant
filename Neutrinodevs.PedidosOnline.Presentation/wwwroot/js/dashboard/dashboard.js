document.addEventListener("DOMContentLoaded", function (event) {
    
    var conn = new signalR.HubConnectionBuilder().withUrl("/OrdersHub").build();
    conn.on("UpdateOrdersTable", function () {
        fillOrdersTable();
    });

    conn.start().catch(function (e) {
        console.log('error signalr');
        console.error(e);
    });
    
    
});

function fillOrdersTable() {
    console.log('fill');
    $.ajax({
        url: "/Order/GetAll",
        type: "GET",
        contentType: "application/json; charset-utf-8",
        dateType: "json",
        success: function (resp) {
            if (resp.length < 0) return;
            
            let tbody_orders = document.getElementById('tbody_orders');

            let rows = "<tr>";
            for (var i = 0; i < resp.length; i++) {
                rows += `<td>${resp[i].number}</td><td>${resp[i].customer_name}</td><td>${resp[i].date}</td><td>${resp[i].total_amount}</td>`;
            }
            rows += "</tr>";
            tbody_orders.innerHTML = rows;
        },
        error: function (err) {
            console.log(err);
        }
    });
}

