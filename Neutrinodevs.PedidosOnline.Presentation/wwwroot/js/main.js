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