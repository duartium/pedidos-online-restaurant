$(document).ready(function () {
    $("#frmDish").validate({
        rule: {
            name: { required: true, minlength: 4 },
            price: { required: true, minlength: 3, maxlength: 8 },
        }
    });

    $("#frmDish").submit(function (e) {
        e.preventDefault();
        
        let dish = {
            name: document.getElementById("name").value,
            price: document.getElementById("price").value,
            image_name: document.getElementById("image").value
        }

        if (!$("#frmDish").valid()) {
            return;
        }

        $("#loader").fadeIn();

        $.ajax({
            url: '/Dish/New',
            method: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(dish),
            success: function (response) {
                $("#loader").fadeOut();
                let resp = response;
                console.log(resp);
                if (resp.code === '000') {
                    Swal.fire('Notificación', 'Producto registrado con éxito.', 'success');
                    $("#frmUser")[0].reset();
                }
            },
            error: function () {
                $("#loader").fadeOut();
                Swal.fire('Notificación', 'Lo sentimos, no se pudo completar la solicitud.', 'error');
            }
        });
    });
});