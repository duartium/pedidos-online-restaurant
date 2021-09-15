$(document).ready(function () {
    let my_order = localStorage.getItem('order');
    if (my_order != undefined) {
        let wrap_items = $("#tbody-invoice");
        let total = 0;
        let content = '';
        JSON.parse(my_order).forEach(x => {
            total += parseFloat(x.price);
            let line_subtotal = parseFloat(x.price) * parseInt(x.quantity);
            content += `<tr><td>${x.name}</td><td class='text-right'>${x.price}</td><td class='text-center'>${x.quantity}</td><td class='text-right'>${line_subtotal.toFixed(2)}</td></tr>`;
        });
        wrap_items.html(content);

        let iva = parseFloat(total) * 0.12;
        let subtotal = parseFloat(total) - parseFloat(iva);
        $("#subtotal").text(subtotal.toFixed(2));
        $("#iva").text(iva.toFixed(2));
        $("#total").text(total.toFixed(2));


        let order_invoice = {
            id_client: 0,
            items: JSON.parse(my_order),
            subtotal: parseFloat($("#subtotal").text()),
            iva: parseFloat($("#iva").text()),
            total: parseFloat($("#total").text())
        }
        localStorage.setItem('order_invoice', JSON.stringify(order_invoice));
    }
});