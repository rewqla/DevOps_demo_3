
document.addEventListener("DOMContentLoaded", TotalPrice);

$("input[type='number']").bind("mouseup", TotalPrice);
$("input[type='number']").bind("mouseup", ValidButton);


function TotalPrice() {
    let totalPrice = 0;
    let tbodies = document.getElementById("cart").querySelectorAll("tbody");
    for (let i = 0; i < tbodies.length; i++) {
        totalPrice += parseInt(tbodies[i].querySelector("input[type='number']").value) * parseInt(tbodies[i].getElementsByTagName("td")[2].innerHTML);
    };
    document.getElementById("totalPrice").innerHTML = "Всього <br/>" + totalPrice + "₴";
}

function ValidButton() {
    let countOfNull = 0;
    let tbodies = document.getElementById("cart").querySelectorAll("tbody");
    for (let i = 0; i < tbodies.length; i++) {
        totalPrice += parseInt(tbodies[i].querySelector("input[type='number']").value) * parseInt(tbodies[i].getElementsByTagName("td")[1].innerHTML);
        if (tbodies[i].querySelector("input[type='number']").value == 0)
            countOfNull++;
    };

    if (countOfNull == document.querySelectorAll("input[type='number']").length)
        document.getElementById("checkout").disabled = true;
    else
        document.getElementById("checkout").disabled = false;
}

