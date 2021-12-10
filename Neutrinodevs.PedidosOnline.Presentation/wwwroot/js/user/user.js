$(document).ready(function () {
    $("#frmUser").validate({
        rule: {
            identification: { required: true, maxlength: 3 },
            name: { required: true, minlength: 3, maxlength: 3 },
            last_name: { required: true, minlength: 3 },
            email: { required: true, minlength: 6 },
            user: { required: true, minlength: 6, maxlength: 20 },
            password: { required: true, minlength: 6 },
            passsword2: { required: true, minlength: 6 },
        }
    });

    $("#frmUser").submit(function (e) {
        e.preventDefault();
        
        let user_register = {
            identification: document.getElementById("identification").value,
            first_name: document.getElementById("name").value,
            last_name: document.getElementById("last_name").value,
            email: document.getElementById("email").value,
            username: document.getElementById("user").value,
            password: document.getElementById("password").value,
            client_type: document.getElementById("sel_role").value
        }
        
        if (!$("#frmUser").valid()) {
            return;
        }

        $("#loader").fadeIn();

        $.ajax({
            url: base_url+'/User/RegisterEmploye',
            method: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(user_register),
            success: function (response) {
                $("#loader").fadeOut();
                let resp = response;
                console.log(resp);
                if (resp.code === '000') {
                    Swal.fire('Notificación', 'Usuario registrado con éxito.', 'success');
                    $("#frmUser")[0].reset();
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
});