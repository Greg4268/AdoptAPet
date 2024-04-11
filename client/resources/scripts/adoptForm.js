// event listener to check submit adoption form button is clicked then routing back to main page index.html
document.addEventListener("DOMContentLoaded", function () {
  var formSubmitButton = document.getElementById("formSubmitButton");

  formSubmitButton.addEventListener("click", function () {
    window.location.href = "./index.html";
  });
});
