document.addEventListener("DOMContentLoaded", TotalPrice);

function TotalPrice() {

    let cartspans = document.getElementById("cart").querySelectorAll("span");
    let totalPrice = 0, fromn = 1, ton = cartspans.length - 2;

    for (let i = fromn; i < ton; i += 3) {
        totalPrice += parseInt(cartspans[i].innerHTML) * parseInt(cartspans[i + 1].innerHTML);
    };
    cartspans[0].textContent = (cartspans.length - 2) / 3;
    cartspans[cartspans.length - 1].textContent = totalPrice + "₴";

}


