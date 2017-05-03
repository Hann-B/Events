let addToCart = (itemId) => {
    $.ajax({
        url: "/home/ShoppingCart/" + itemId,
        method: "POST",
        dataType: "html",
        success: (partial) => {
            $("#shoppingCart").html(partial);
        }
    });
}


let removeFromCart = (itemId) => {
    $.ajax({
        url: "/Home/RemoveFromCart/" + itemId,
        method: "DELETE",
        dataType: "html",
        success: (partial) => {
            $("#cart").html(partial);
        }
    });
}
$(document).ready(() => {

});