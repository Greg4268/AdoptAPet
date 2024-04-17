const userURL = "http://localhost:5292/api/UserAccounts";

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

function SubmitForm() {
  alert("adoption form submitting process begun");
  const formData = {
    address: document.getElementById("firstName").value.trim(),
    yardSize: document.getElementById("lastName").value.trim(),
    fenced: document.getElementById("email").value.trim(),
  };

  fetch("http://localhost:5292/api/UserAccounts", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => {
      if (!response.ok) {
        // Extracting error message from response body
        return response.json().then((errorData) => {
          console.error("Server responded with error:", errorData);
          alert(`Error signing up: ${errorData.title || "Unknown error"}`);
          throw new Error(`HTTP error, status = ${response.status}`);
        });
      }
      return response.json();
    })
    .then((data) => {
      console.log("Server response data:", data);
      alert("Signup successful!");
    })
    .catch((error) => {
      console.error("Error during sign up:", error);
      alert(`Error during sign up: ${error.message}`);
    });
}
