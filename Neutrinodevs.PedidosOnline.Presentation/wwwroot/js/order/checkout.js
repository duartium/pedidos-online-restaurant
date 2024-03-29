﻿$("#frmRegistrarme").submit(function (e) {
    e.preventDefault();

    let user = {
        identification: document.querySelector("#identification"),
        first_name: document.querySelector("#name"),
        last_name: document.querySelector("#last_name"),
        email: document.querySelector("#email"),
        phone: document.querySelector("#phone"),
        username: document.querySelector("#user"),
        pass: document.querySelector("#password"),
        pass2: document.querySelector("#passsword2")
    }

    let user_register = {
        identification: user.identification.value,
        first_name: user.first_name.value,
        last_name: user.last_name.value,
        email: user.email.value,
        phone: user.phone.value,
        username: user.username.value,
        password: user.pass.value,
        address: delivery_position === undefined ? '' : $("#location").val() + '|' + delivery_position.lat + ',' + delivery_position.lng
    }

    if (!EsCedulaValida(user_register.identification)) {
        Swal.fire('Notificación', 'La cédula ingresada no es válida.', 'warning');
        return;
    }
    
    if (!$("#frmRegistrarme").valid()) {
        return;
    }

    $("#loader").fadeIn();

    $.ajax({
        url: base_url+'/User/Register',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(user_register),
        success: function (response) {
            $("#loader").fadeOut();
            let resp = response;
            
            if (resp.code === '000') {
                let order_invoice = JSON.parse(localStorage.getItem('order_invoice'));
                order_invoice.id_client = resp.id_client;
                localStorage.setItem('order_invoice', JSON.stringify(order_invoice));

                window.location = base_url+'/User/Verification';
            }
            else if (resp === '002') {
                Swal.fire('Notificación', 'Usted ya tiene una cuenta activa, por favor inicie sesión.', 'error');
            } else if (resp === '003') {
                Swal.fire('Notificación', `El usuario ${user_register.username} ya está en uso, por favor elija otro nombre de usuario.`, 'warning');
            }
            else {
                Swal.fire('Notificación', 'Lo sentimos, no se pudo completar la solicitud.', 'error');
            }
        },
        error: function () {
            $("#loader").fadeOut();
            Swal.fire('Notificación', 'Lo sentimos, no se pudo completar la solicitud.', 'error');
        }
    });
});


$("#frmLogin").submit(function (e) {
    e.preventDefault();

    let current_user = {
        username: document.querySelector('#username').value,
        password: document.querySelector('#user_password').value,
        is_client: true
    }

    if (!$("#frmLogin").valid()) {
        return;
    }

    $("#loader").fadeIn();

    $.ajax({
        url: base_url+'/User/Login',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: current_user,
        success: function (response) {
            $("#loader").fadeOut();
            let resp = response;
            
            if (resp.code === '000') {
                let order_invoice = JSON.parse(localStorage.getItem('order_invoice'));
                order_invoice.id_client = resp.id_client;
                localStorage.setItem('order_invoice', JSON.stringify(order_invoice));
                SaveOrder(order_invoice);
            }
            else if (resp.code === '002') {
                Swal.fire('Notificación', 'Usuario o contraseña incorrectas. Por favor, corrija y vuelva a intentar.', 'error');
            } else {
                Swal.fire('Notificación', 'Usuario o contraseña son obligatorios.', 'error');
            }
        },
        error: function () {
            $("#loader").fadeOut();
            Swal.fire('Notificación', 'Lo sentimos, no se pudo completar la autenticación.', 'error');
        }
    });
});

$(document).ready(function () {
    $("#location").click(function () {
        $("#map-modal").modal()
    });

    $("#frmRegistrarme").validate({
        rule: {
            identification: { required: true, maxlength: 3 },
            name: { required: true, minlength: 3, maxlength: 3 },
            last_name: { required: true, minlength: 3 },
            email: { required: true, minlength: 6 },
            phone: { required: true, minlength: 10 },
            user: { required: true, minlength: 6, maxlength:20 },
            password: { required: true, minlength: 6 },
            passsword2: { required: true, minlength: 6 },
            location: { required: true }
        }
    });

    $("#frmLogin").validate({
        rule: {
            username: { required: true },
            user_password: { required: true }
        }
    });

    $('#map-modal').on('hidden.bs.modal', function () {
        ok();
    })

});

const notify = () => {
    connection.invoke("NotifyOrder");
}

function notifyOrder(callback, id_order) {
    callback();
    window.location = base_url+'/Order/Processing?id_order=' + id_order;
}

console.log(base_url);
function SaveOrder(order_invoice) {
    $.ajax({
        url: base_url+'/Order/Save',
        type: 'POST',
        data: JSON.stringify(order_invoice),
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            const resp = response;
            if (resp.code == '000') {
                localStorage.removeItem('order_invoice');
                localStorage.removeItem('order');

                notifyOrder(notify, resp.id_order);

            } else {
                Swal.fire("Orden", "Lo sentimos. No se pudo registrar tu orden.", "warning");
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}