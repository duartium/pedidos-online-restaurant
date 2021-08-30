﻿var pushbar = new Pushbar({
    blur: true,
    overlay: true
});

$(".card").click(function () {
    $("#txt_quantity").val('1');
    let dish = $(this).data('dish');
    current_item_price = parseFloat(dish.price).toFixed(2);
    console.log(current_item_price);
    $("#product-modal").data('id', dish.id);
    $("#product-title").text(dish.name);
    $("#modal-price").text(current_item_price);
    $("#product-modal").modal();
});


$("#add-cart").click(function () {

    var product_order = {
        id: parseInt($("#product-modal").data('id')),
        name: $("#product-title").text(),
        price: $("#modal-price").text(),
        quantity: parseInt($("#txt_quantity").val())
    }
    let quantity_cart = parseInt($("#quantity_cart").text());
    quantity_cart += product_order.quantity;
    console.log(quantity_cart);
    $("#quantity_cart").text(quantity_cart.toString());

    saveLocalStorage(product_order);

    $("#txt_quantity").val('1');
    $("#product-modal").modal('hide');
});


const saveLocalStorage = (product) => {
    let order = localStorage.getItem('order');
    if (order) {
        let items = JSON.parse(order);
        items.push(product);
        console.log(items);
        localStorage.setItem("order", JSON.stringify(items));
    } else {
        let items = [];
        items.push(product);
        console.log(items);
        localStorage.setItem("order", JSON.stringify(items));
    }
    
}

const setPendingOrder = () => {
    let order = localStorage.getItem('order');
    if (order) {
        let items = JSON.parse(order);
        let num_items = 0, subtotal = 0;
        items.forEach(elem => {
            subtotal += parseFloat(elem.price);
            num_items += elem.quantity;
        });

        $("#quantity_cart").text(num_items);
        setElementsToCart(items);

        $("#subtotal-cart").text(subtotal.toFixed(2));
        enabledCartButtons(false);
    } else {
        $("#subtotal-cart").text(parseFloat(0).toFixed(2));
        enabledCartButtons(true);
    }
}

const setElementsToCart = (items) => {
    let wrap_items = $(".order-items");
    let subtotal = 0;
    let content = '';
    items.forEach(x => {
        subtotal += parseFloat(x.price);
        content += `<li><a class="bg-danger text-white">×</a><span class="title-item-cart">${x.name} (${x.quantity})</span><br />`;
        content += `<span class="price-item-cart">$${x.price}</span></li>`;
    });
    wrap_items.html(content);
    $("#subtotal-cart").text(subtotal.toFixed(2));
    enabledCartButtons(false);
}

const enabledCartButtons = (enabled) => {
    if (enabled) {
        $("#btn_order").attr("disabled", true);
        $("#btn_empty_cart").attr("disabled", true);
    } else {
        $("#btn_order").removeAttr("disabled");
        $("#btn_empty_cart").removeAttr("disabled");
    }
}

$(document).ready(function () {
    setPendingOrder();
});

$("#btn_shopping_cart").click(function () {
    let items = localStorage.getItem('order');
    if (items) {
        setElementsToCart(JSON.parse(items));
    }
    pushbar.open('pushbar-cart');
});

$("#btn_empty_cart").click(function () {
    Swal.fire({
        title: 'Vaciar carrito',
        text: '¿Está seguro de eliminar todos los productos seleccionados?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            localStorage.removeItem("order");
            $(".order-items").html('');
            $("#subtotal-cart").text(parseFloat(0).toFixed(2));
            enabledCartButtons(false);
        }
    })
    
    
});
