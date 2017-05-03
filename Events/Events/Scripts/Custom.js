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
        url: "/Ticket/RemoveFromCart/" + itemId,
        method: "DELETE",
        dataType: "html",
        success: (partial) => {
            $("#cart").html(partial);
        }
    });
}
let removeFromCartHP = (itemId) => {
    $.ajax({
        url: "/Home/RemoveFromCart/" + itemId,
        method: "DELETE",
        dataType: "html",
        success: (partial) => {
            $("#shoppingCart").html(partial);
        }
    });
}

let clearCart = () => {
    $.ajax({
        url: "/Home/ClearCart/",
        method: "POST",
        dataType: "html",
        success: (partial) => {
            $("#shoppingCart").html(partial);
        }
    });
}
//function reloadCart() {
//    $('#shoppingCart').load("RemoveFromCart");
//}

$(document).ready(() => {

});