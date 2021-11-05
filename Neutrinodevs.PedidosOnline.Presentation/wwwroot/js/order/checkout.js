$("#frmRegistrarme").submit(function (e) {
    e.preventDefault();

    let user = {
        identification: document.querySelector("#identification"),
        first_name: document.querySelector("#name"),
        last_name: document.querySelector("#last_name"),
        email: document.querySelector("#email"),
        pass: document.querySelector("#password"),
        pass2: document.querySelector("#passsword2")
    }


    //let form_valid = true;
    //Object.entries(user).forEach(([clave, valor]) => {

    //    if (valor.value.length == 0) {
    //        valor.classList.add('invalid');
    //        if (form_valid) form_valid = false;
    //    } else {
    //        valor.classList.remove('invalid');
    //    }
    //});


    //if (user.pass != user.pass2) {
    //    if ($("#password").next('span').length < 1) {
    //        var message = document.createElement("span");
    //        message.innerHTML = "Las contraseñas no son iguales.";
    //        message.classList.add("color-primary", "ml-2", "font-weight");

    //        let pass = document.querySelector("#password");
    //        pass.after(message);
    //    }
    //} else {
    //    $("#password").next("span").remove();
    //}

    //if (!form_valid) return;

    let user_register = {
        identification: user.identification.value,
        first_name: user.first_name.value,
        last_name: user.last_name.value,
        email: user.email.value,
        password: user.pass.value
    }
    console.log($("#frmRegistrarme").valid());
    if (!$("#frmRegistrarme").valid()) {
        return;
    }
    return;

    $.ajax({
        url: '/User/Register',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(user_register),
        success: function (response) {
            let resp = response;
            console.log(response);
            if (resp.code === '000') {
                let order_invoice = JSON.parse(localStorage.getItem('order_invoice'));
                order_invoice.id_client = resp.id_client;
                localStorage.setItem('order_invoice', JSON.stringify(order_invoice));
                console.log(order_invoice);
                window.location = '/User/Verification';
            }
            else if (resp.code === '002') {
                Swal.fire('Notificación', 'Usted ya tiene una cuenta activa, por favor inicie sesión.', 'error');
            } else {
                Swal.fire('Notificación', 'Lo sentimos, no se pudo completar la solicitud.', 'error');
            }
        },
        error: function () {
            Swal.fire('Notificación', 'Lo sentimos, no se pudo completar la solicitud.', 'error');
        }
    });
});


$("#frmLogin").submit(function (e) {
    e.preventDefault();

    let current_user = {
        username: document.querySelector('#username').value,
        password: document.querySelector('#user_password').value,
    }

    if (!$("#frmLogin").valid()) return;

    $.ajax({
        url: '/User/Login',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: current_user,
        success: function (response) {
            let resp = response;
            console.log(response);
            if (resp.code === '000') {
                let order_invoice = JSON.parse(localStorage.getItem('order_invoice'));
                order_invoice.id_client = resp.id_client;
                localStorage.setItem('order_invoice', JSON.stringify(order_invoice));
                console.log(order_invoice);
                SaveOrder(order_invoice);
            }
            else if (resp.code === '002') {
                Swal.fire('Notificación', 'Usuario y/o contraseñas incorrectas. Por favor, corrija y vuelva a intentar.', 'error');
            } else {
                Swal.fire('Notificación', 'Usuario y/o contraseña son obligatorios.', 'error');
            }
        },
        error: function () {
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
            identification: { required: true, minlength: 10, maxlength: 10 },
            name: { required: true },
            last_name: { required: true },
            email: { required: true },
            phone: { required: true },
            password: { required: true },
            passsword2: { required: true },
            location: { required: true }
        }
    });

    $("#frmLogin").validate({
        rule: {
            username: { required: true },
            user_password: { required: true }
        }
    });
});

function SaveOrder(order_invoice) {
    $.ajax({
        url: '/Order/Save',
        type: 'POST',
        data: JSON.stringify(order_invoice),
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            const resp = response;
            if (resp.code == '000') {
                localStorage.removeItem('order_invoice');
                localStorage.removeItem('order');
                window.location = '/Order/Processing?id_order=' + parseInt(resp.id_order);
            } else {
                Swal.fire("Orden", "Lo sentimos. No se pudo registrar tu orden.", "warning");
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}