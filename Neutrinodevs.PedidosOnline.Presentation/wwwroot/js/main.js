$(document).ready(function () {

    $('.numbers').keypress(function (e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[\d]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    });

    $(document).on('keypress', '.numbers', function (e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[\d]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    });

    $('.letters').keypress(function (e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[A-Za-zñÑÀ-ÿ\s]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    });

    $('.alphanumeric').keypress(function (e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[0-9A-Za-zñÑÀ-ÿ\s]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    });

    const btn_menu = document.querySelector(".btn-menu");
    if (btn_menu) {
        btn_menu.addEventListener("click", () => {
            const menu_items = document.querySelector(".menu-items");
            menu_items.classList.toggle('show');
        });
    }

});

function EsCedulaValida(cedula) {

    if (cedula.length == 10) {
        //Obtenemos el digito de la region que son los dos primeros digitos
        var digito_region = cedula.substring(0, 2);

        //Pregunto si la region existe ecuador se divide en 24 regiones
        if (digito_region >= 1 && digito_region <= 24) {

            // Extraigo el ultimo digito
            var ultimo_digito = cedula.substring(9, 10);

            //Agrupo todos los pares y los sumo
            var pares = parseInt(cedula.substring(1, 2)) + parseInt(cedula.substring(3, 4)) + parseInt(cedula.substring(5, 6)) + parseInt(cedula.substring(7, 8));

            //Agrupo los impares, los multiplico por un factor de 2, si la resultante es > que 9 le restamos el 9 a la resultante
            var numero1 = cedula.substring(0, 1);
            var numero1 = (numero1 * 2);
            if (numero1 > 9) { var numero1 = (numero1 - 9); }

            var numero3 = cedula.substring(2, 3);
            var numero3 = (numero3 * 2);
            if (numero3 > 9) { var numero3 = (numero3 - 9); }

            var numero5 = cedula.substring(4, 5);
            var numero5 = (numero5 * 2);
            if (numero5 > 9) { var numero5 = (numero5 - 9); }

            var numero7 = cedula.substring(6, 7);
            var numero7 = (numero7 * 2);
            if (numero7 > 9) { var numero7 = (numero7 - 9); }

            var numero9 = cedula.substring(8, 9);
            var numero9 = (numero9 * 2);
            if (numero9 > 9) { var numero9 = (numero9 - 9); }

            var impares = numero1 + numero3 + numero5 + numero7 + numero9;

            //Suma total
            var suma_total = (pares + impares);

            //extraemos el primero digito
            var primer_digito_suma = String(suma_total).substring(0, 1);

            //Obtenemos la decena inmediata
            var decena = (parseInt(primer_digito_suma) + 1) * 10;

            //Obtenemos la resta de la decena inmediata - la suma_total esto nos da el digito validador
            var digito_validador = decena - suma_total;

            //Si el digito validador es = a 10 toma el valor de 0
            if (digito_validador == 10)
                var digito_validador = 0;

            //Validamos que el digito validador sea igual al de la cedula
            if (digito_validador == ultimo_digito) {
                return true;
            } else {
                return false;
            }

        } else {
            console.log('Esta cedula no pertenece a ninguna region');
            return false;
        }
    } else {
        console.log('Esta cedula tiene menos de 10 Digitos');
        return false;
    }
}