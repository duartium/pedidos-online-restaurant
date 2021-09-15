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

    console.log(invoice);
    $.ajax({
        url: '/User/Verify',
        type: 'POST',
        data: JSON.stringify(user),
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            if (response == '000') {
                SaveOrder(invoice);
            } else {
                Swal.fire("Verificación", "El código ingresado no es correcto. Por favor corrija y vuelva a intentar.", "");
            }
        },
        error: function (error) {
            console.log(error);
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
                window.location = '/Order/Processing?order=' + parseInt(resp.id_order);
            } else {
                Swal.fire("Orden", "Lo sentimos. No se pudo registrar tu orden.", "warning");
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}