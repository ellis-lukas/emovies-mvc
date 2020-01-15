var checkBox = document.querySelector(".form-row__checkbox");
var checkLabel = document.querySelector(".form-row__check-image");
var nameInput = document.querySelector(".form-row__cell--text");
var requiredNameValidator = document.querySelector(".form-row__required-field-validator");

function checkboxChanged() {
    if (checkBox.checked) {
        checkLabel.style.backgroundImage = "none";
        checkBox.checked = false;
    }

    else {
        checkLabel.style.backgroundImage = "url('../Content/Images/checkmark-black.png')";
        checkBox.checked = true;
    }
}

checkBox.addEventListener("change", checkboxChanged);


