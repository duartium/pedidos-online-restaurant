$("#btnVerify").click(function () {

    let code = document.querySelector("#code").value;
    if (code.trim().length < 6) {
        Swal.fire("Datos no válidos", "El código debe ser de 6 dígitos.", "warning");
        return;
    }

    let invoice = JSON.parse(localStorage.getItem('order_invoice'));
    let user = {
        id_client: invoice.id_client,
        code: code
    };

    $.ajax({
        url: '/User/Verify',
        type: 'POST',
        data: JSON.stringify(user),
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (response == '000') {
                window.location = '/Order/Processing';
            } else {
                Swal.fire("Verificación", "El código ingresado no es correcto. Por favor corrija y vuelva a intentar.", "");
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
});