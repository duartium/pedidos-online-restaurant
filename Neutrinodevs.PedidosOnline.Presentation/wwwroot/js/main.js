$(document).ready(function () {

    $('.numbers').keypress(function (e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[\d]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    });

    $('.letters').keypress(function (e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[A-Za-zñÑ\s]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    });

    $('.alphanumeric').keypress(function (e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[0-9A-Za-zñÑ\s]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    });

});