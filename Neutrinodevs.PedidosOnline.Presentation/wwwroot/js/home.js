const selectedMenu = (element) => {
    let wrap_items = document.getElementsByClassName("wrap_item_menu2");
    Array.from(wrap_items).forEach(x => {
        x.classList.remove('item_menu2_selected');
    });
    element.classList.add('item_menu2_selected');
}

Array.from(document.getElementsByClassName("wrap_item_menu2")).forEach(element => {
    element.addEventListener('click', () => {
        selectedMenu(element);
        if (element.id === 'mariscos_menu') {
            getDishes();
        } else if (element.id === 'bebidas_menu') {
            getDrinks();
        }
    })
});

const getDishes = () => {
    $.ajax({
        url: '/Dish/GetAll',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            setProducts(response);
        },
        error: function (error) {
            console.log(error);
        }
    });
}

const getDrinks = () => {
    $.ajax({
        url: '/Dish/GetDrinks',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            setProducts(response);
        },
        error: function (error) {
            console.log(error);
        }
    });
}

const setProducts = (products) => {
    let content = '';
    Array.from(JSON.parse(products)).forEach(item => {
        content += '<div class="col-md-6 col-lg-4 col-sm-12">';
        content += `<div class="card m-2 card-product" data-target="#product-modal" data-dish='${JSON.stringify(item)}'>`;
        content += `<img src="/images/products/${item.image}"/>`;
        content += `<div class="card-body"><strong>${item.name}</strong><br /><span>$${item.price.toFixed(2)}</span></div>`;
        content += '<div class="overlay"><h5 class="text"><i class="fa fa-plus-circle"></i> Agregar al carrito</h5 ></div></div></div>';
    });
    $("#products").html(content).fadeIn();

}

document.getElementById("login").addEventListener("click", function (event) {
    $('#login-modal').modal();
});

const frmLogin = $('#frmSession').validate({
    rules: {
        username: {
            required: true,
            minlength: 4
        },
        user_password: {
            required: true,
            minlength: 6
        }
    }
});

document.getElementById("frmSession").addEventListener("submit", function (e) {
    e.preventDefault();
    console.log(frmLogin.valid());
    if (!frmLogin.valid()) return;

    let user = {
        username: document.querySelector('#username').value,
        password: document.querySelector('#user_password').value,
    }

    login(user);
});


const login = (user) => {

    $.ajax({
        url: '/User/Login',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: user,
        success: function (response) {
            let resp = response;
            console.log(response);
            if (resp.code === '000') {
                localStorage.setItem('user', JSON.stringify(resp));
                if(resp.id_user > 0)
                    window.location = '/Delivery/MyDeliveries';
                else if (resp.id_client > 0)
                    window.location = '/Dashboard';
            }
            else if (resp.code === '002')
                Swal.fire('Notificación', 'Usuario y/o contraseñas incorrectas. Por favor, corrija y vuelva a intentar.', 'error');
            else {
                Swal.fire('Notificación', 'Usuario y/o contraseña son obligatorios.', 'error');
            }
        },
        error: function () {
            Swal.fire('Notificación', 'Lo sentimos, no se pudo completar la autenticación.', 'error');
        }
    });
}