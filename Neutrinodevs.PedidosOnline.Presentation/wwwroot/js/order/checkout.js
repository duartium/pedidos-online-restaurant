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


    let form_valid = true;
    Object.entries(user).forEach(([clave, valor]) => {

        if (valor.value.length == 0) {
            valor.classList.add('invalid');
            if (form_valid) form_valid = false;
        } else {
            valor.classList.remove('invalid');
        }


    });


    if (user.pass != user.pass2) {
        if ($("#password").next('span').length < 1) {
            var message = document.createElement("span");
            message.innerHTML = "Las contraseñas no son iguales.";
            message.classList.add("color-primary", "ml-2", "font-weight");

            let pass = document.querySelector("#password");
            pass.after(message);
        }
    } else {
        $("#password").next("span").remove();
    }

    if (!form_valid) return;

    //llamada ajax
});
