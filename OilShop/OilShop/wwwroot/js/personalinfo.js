const oldPassword = document.querySelector('#oldPassword');
const newPassword = document.querySelector('#newPassword');
const confirmPassword = document.querySelector('#confirmPassword');

document.querySelector('#toggleOldPassword').addEventListener('click', function (e) {
    if (oldPassword.type === "password") {
        oldPassword.type = "text";
        this.className = 'far fa-eye';
    } else {
        oldPassword.type = "password";
        this.className = 'far fa-eye-slash';
    }
});

document.querySelector('#toggleNewPassword').addEventListener('click', function (e) {
    if (newPassword.type === "password") {
        newPassword.type = "text";
        this.className = 'far fa-eye';
    } else {
        newPassword.type = "password";
        this.className = 'far fa-eye-slash';
    }
});

document.querySelector('#toggleConfirmPassword').addEventListener('click', function (e) {
    if (confirmPassword.type === "password") {
        confirmPassword.type = "text";
        this.className = 'far fa-eye';
    } else {
        confirmPassword.type = "password";
        this.className = 'far fa-eye-slash';
    }
});

document.getElementById("btnEdit").addEventListener('click', EnableInputs);
document.getElementById("mdb").addEventListener('click', CancelForm);
document.getElementById("closeModal").addEventListener('click', CloseModal);
document.getElementById("formdata").addEventListener("submit", ValidateForm);
document.getElementById("formpassword").addEventListener("submit", ValidatePassword);

function CancelForm(event) {
    event.preventDefault();
    var maind = document.getElementById("dwui");
    maind.querySelectorAll("input").forEach(number => {
        number.disabled = true;

    });
    maind.querySelector("select").disabled = true;
    document.getElementById("dwb").querySelectorAll("button").forEach(number => {
        number.hidden = true;
    });
}

function CloseModal(event) {
    $('#changepasswordmodal').hide();
    event.preventDefault();
}

function EnableInputs() {
    var maind = document.getElementById("dwui");
    maind.querySelectorAll("input").forEach(number => {
        number.disabled = false;
    });
    maind.querySelector("select").disabled = false;
    document.getElementById("dwb").querySelectorAll("button").forEach(number => {
        number.hidden = false;
    });
}

function ValidateForm(event) {
    document.getElementsByName("usinfo").forEach(number => {
        if (number.querySelector("input").value == "") {
            event.preventDefault();
            number.querySelector("span").innerHTML = "Поле має бути заповненим";
        }
        else {
            number.querySelector("span").innerHTML = "";
            if (number.querySelector("input").id == "Email") {
                if (!number.querySelector("input").value.includes('@') || !number.querySelector("input").value.includes(".")) {
                    event.preventDefault();
                    number.querySelector("span").innerHTML = "Неправильна пошта";
                }
                else {
                    number.querySelector("span").innerHTML = "";
                }
            }
            if (number.querySelector("input").id == "PhoneNumber") {
                var pattern = /^\+?3?8?(0\d{9})$/;
                if (!number.querySelector("input").value.match(pattern)) {
                    event.preventDefault();
                    number.querySelector("span").innerHTML = "Неправильний номер телефону";
                }
                else {
                    number.querySelector("span").innerHTML = "";
                }
            }
        }
    });
}

function ValidatePassword(event) {
    document.getElementsByName("pasinfo").forEach(number => {
        if (number.querySelector("input").value == "") {
            event.preventDefault();
            number.querySelector("input").classList.remove('mg24');
            number.querySelector("span").innerHTML = "Поле має бути заповненим";
        }
        else {
            number.querySelector("input").classList.add('mg24');
            number.querySelector("span").innerHTML = "";
            if (number.querySelector("input").value.length < 8) {
                event.preventDefault();
                number.querySelector("input").classList.remove('mg24');
                number.querySelector("span").innerHTML = "Пароль не може бути меншим за 8 символів";
            }
            else if (number.querySelector("input").value.length > 30) {
                event.preventDefault();
                number.querySelector("input").classList.remove('mg24');
                number.querySelector("span").innerHTML = "Пароль не може бути більшим за 30 символів";
            }
            else {
                number.querySelector("input").classList.add('mg24');
                number.querySelector("span").innerHTML = "";
                if (number.querySelector("input").id == "confirmPassword") {
                    if (number.querySelector("input").value != document.getElementById("newPassword").value) {
                        event.preventDefault();
                        number.querySelector("input").classList.remove('mg24');
                        number.querySelector("span").innerHTML = "Паролі мають співпадати";
                    }
                    else {
                        number.querySelector("input").classList.add('mg24');
                        number.querySelector("span").innerHTML = "";
                    }
                }
            }
        }
    });
}