var pushbar = new Pushbar({
    blur: true,
    overlay: true
});

$(".card").click(function () {
    let dish = $(this).data('dish');
    console.log(dish.name);
    $("#product-title").text(dish.name);
    $("#modal-price").text(dish.price);
    $("#img-modal").attr("src", dish.image_url);
    $("#product-modal").modal();
});

