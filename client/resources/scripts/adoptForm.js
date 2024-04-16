document.addEventListener("DOMContentLoaded", function () {
  var adoptionFormButton = document.getElementById("adoptionFormButton");

  if (adoptionFormButton) {
      adoptionFormButton.addEventListener("click", function (event) {
          event.preventDefault(); // Prevent the link from navigating immediately

          // Check if the user is logged in
          if (!localStorage.getItem("userToken")) {
              alert("Please login to complete the adoption form.");
              window.location.href = "./login.html"; // Redirect to the login page
          } else {
              window.location.href = "./adoptForm.html"; // Navigate to the form if logged in
          }
      });
  }
});
