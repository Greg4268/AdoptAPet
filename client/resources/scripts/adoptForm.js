const userURL = "http://localhost:5292/api/UserAccounts";

// document.addEventListener("DOMContentLoaded", function () {
//   var adoptionFormButton = document.getElementById("adoptionFormButton");

//   if (adoptionFormButton) {
//     adoptionFormButton.addEventListener("click", function (event) {
//       event.preventDefault(); // Prevent the link from navigating immediately

//       // Check if the user is logged in
//       if (!localStorage.getItem("userToken")) {
//         alert("Please login to complete the adoption form.");
//         window.location.href = "./login.html"; // Redirect to the login page
//       } else {
//         window.location.href = "./adoptForm.html"; // Navigate to the form if logged in
//       }
//     });
//   }
// });

function SubmitForm() {
  alert("Adoption form submitting process begun");

  const userToken = localStorage.getItem("userToken"); // get userToken
  const tokenData = JSON.parse(userToken); // parse userToken
  const UserId = tokenData.userId; // get userId from token 

  if (!UserId) {
    alert("User not properly authenticated.");
    return;
  }

  const formData = {
    UserId: UserId,
    YardSize: parseFloat(document.getElementById("yardSize").value.trim()), // Convert to double
    Fenced: document.getElementById("fencedIn").value.trim().toLowerCase() === 'true', // Ensure boolean
    Address: document.getElementById("address").value.trim(),
  };

  console.log("Form Data:", formData); // Confirm the formData before sending


  console.log(`Sending update for UserId: ${UserId}`); // Confirm the UserId

  fetch(`http://localhost:5292/api/UserAccounts/${UserId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
  .then(response => {
    if (!response.ok) {
      return response.json().then(errorData => {
        console.error("Server responded with error:", errorData);
        console.error("Validation errors:", errorData.errors); // Log detailed errors
        alert(`Error during update: ${errorData.title || "Unknown error"}`);
        throw new Error(`HTTP error, status = ${response.status}`);
      });
    }
    return response.json();
  })
  .then(data => {
    console.log("Server response data:", data);
    alert("Update successful!");
  })
  .catch(error => {
    console.error("Error during update:", error);
    alert(`Error during update: ${error.message}`);
  });
}

