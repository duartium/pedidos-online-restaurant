var pushbar = new Pushbar({
    blur: true,
    overlay: true
});

var iterator_prod;
if (localStorage.getItem('iterator_prod')) {
    iterator_prod = parseInt(localStorage.getItem('iterator_prod'));
}
else {
    iterator_prod = 0;
    localStorage.setItem('iterator_prod', iterator_prod.toString());
}

$(document).on("click", ".card", function () {
    $("#txt_quantity").val('1');
    let dish = $(this).data('dish');
    current_item_price = parseFloat(dish.price).toFixed(2);
    $("#product-modal").data('id', dish.id);
    $("#product-title").text(dish.name);
    $("#modal-price").text(current_item_price);
    $("#product-modal").modal();
});


$("#add-cart").click(function () {
    iterator_prod++;
    var product_order = {
        uid: iterator_prod,
        id: parseInt($("#product-modal").data('id')),
        name: $("#product-title").text(),
        price: $("#modal-price").text(),
        quantity: parseInt($("#txt_quantity").val())
    }
    let quantity_cart = parseInt($("#quantity_cart").text());
    quantity_cart += product_order.quantity;
    $("#quantity_cart").text(quantity_cart.toString());

    saveLocalStorage(product_order);
    localStorage.setItem('iterator_prod', iterator_prod.toString());

    $("#txt_quantity").val('1');
    $("#product-modal").modal('hide');
});


const saveLocalStorage = (product) => {
    let order = localStorage.getItem('order');
    if (order) {
        let items = JSON.parse(order);
        items.push(product);
        localStorage.setItem("order", JSON.stringify(items));
    } else {
        let items = [];
        items.push(product);
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
        disabledCartButtons(false);
    } else {
        $("#subtotal-cart").text(parseFloat(0).toFixed(2));
        disabledCartButtons(true);
    }
}

const removeProdFromCart = (id_item, element, num_products) => {
    let order = localStorage.getItem('order');
    let items = JSON.parse(order);
    console.log(items);
    let new_items = items.filter(x => x.uid != id_item);
    console.log(new_items);

    let quantity_cart = parseInt($("#quantity_cart").text());
    quantity_cart -= num_products;
    $("#quantity_cart").text(quantity_cart.toString());

    localStorage.setItem("order", JSON.stringify(new_items));
    element.parentNode.parentNode.removeChild(element.parentNode);
}

const setElementsToCart = (items) => {
    let wrap_items = $(".order-items");
    let subtotal = 0;
    let content = '';
    items.forEach(x => {
        subtotal += parseFloat(x.price);
        content += `<li><a class="bg-danger text-white" onclick="removeProdFromCart(${x.uid}, this, ${x.quantity})">×</a><span class="title-item-cart">${x.name} (${x.quantity})</span><br />`;
        content += `<span class="price-item-cart">$${x.price}</span></li>`;
    });
    wrap_items.html(content);
    $("#subtotal-cart").text(subtotal.toFixed(2));
    disabledCartButtons(false);
}



const disabledCartButtons = (enabled) => {
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
            iterator_prod = 0;
            localStorage.removeItem("iterator_prod");
            $(".order-items").html('');
            $("#subtotal-cart").text(parseFloat(0).toFixed(2));
            $("#quantity_cart").text(0);
            disabledCartButtons(true);
        }
    })
    
    
});
