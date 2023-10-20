document.addEventListener("DOMContentLoaded", TotalPrice);

function TotalPrice() {

    let prices = document.getElementsByName("price");
    let totalprice = 0;
    prices.forEach(number => {
        totalprice += Number.parseInt(number.textContent);
    });
    document.getElementById("total").innerText = totalprice + "₴";

}