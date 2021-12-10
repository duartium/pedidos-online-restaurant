var formData = new FormData();
var fileName = '';

$(document).ready(function () {

    document.querySelector('#image').addEventListener('change', function (e) {

        try {
            var file = e.target.files[0];
            console.log(file);
            if (file != undefined) {

                fileName = file.name;
                let reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function () {
                    SaveImage(file);
                    formData.append('Photo', reader.result);
                }
            }
        } catch (e) {
            console.log('catch: ' + e);
        }
    });

    $("#frmDish").validate({
        rule: {
            name: { required: true, minlength: 4 },
            price: { required: true, minlength: 3, maxlength: 8 },
        }
    });

    $("#frmDish").submit(function (e) {
        e.preventDefault();

        if (!$("#frmDish").valid()) {
            return;
        }

        let dish = {
            name: document.getElementById("name").value,
            price: document.getElementById("price").value.replace(',', '.'),
            image_name: fileName,
            type: $("#sel_product_type option:selected").val()
        }
        
        $("#loader").fadeIn();

        $.ajax({
            url: base_url+'/Dish/New',
            method: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(dish),
            success: function (response) {
                $("#loader").fadeOut();
                let resp = response;
                console.log(resp);
                if (resp === '000') {
                    Swal.fire('Notificación', 'Producto registrado con éxito.', 'success');
                    $("#frmDish")[0].reset();
                } else if (resp === '002') {
                    Swal.fire('Notificación', 'No se enviaron los datos correctamente.', 'error ');
                } else {
                    Swal.fire('Notificación', 'No se pudo registrar el producto.', 'error ');
                }
            },
            error: function () {
                $("#loader").fadeOut();
                Swal.fire('Notificación', 'Lo sentimos, no se pudo completar la solicitud.', 'error');
            }
        });
    });
});

function SaveImage(img) {
    $("#loader").fadeIn("fast");
    var formData = new FormData();
    formData.append("image", img);

    $.ajax({
        url: base_url+"/Dish/SaveResource",
        type: 'POST',
        contentType: false,
        processData: false,
        data: formData,
        success: function (resp) {
            $("#loader").fadeOut("slow");
            console.log(resp);
        }, error: function (err) {
            $("#loader").fadeOut("slow");
            sweetAlert({
                type: 'warning',
                title: 'Transacción fallida',
                text: 'Lo sentimos. El archivo que intentas cargar no es válido.'
            });
        }
    });

}
