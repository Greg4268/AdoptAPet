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
  const userToken = localStorage.getItem("userToken");
  const tokenData = JSON.parse(userToken);
  const userId = tokenData.userId;

  if (!userId) {
      alert("User not properly authenticated.");
      return;
  }

  FetchUserData(userId).then(existingUserData => {
      if (!existingUserData) {
          console.error("No user data received");
          alert("Failed to fetch user data.");
          return;
      }

      const formData = {
          UserId: userId,
          FirstName: existingUserData.FirstName,
          LastName: existingUserData.LastName,
          Email: existingUserData.Email,
          Password: existingUserData.Password,
          AccountType: existingUserData.AccountType,
          Address: document.getElementById("address").value.trim(),
          YardSize: parseFloat(document.getElementById("yardSize").value.trim()),
          Fenced: document.getElementById("fencedIn").value.trim().toLowerCase() === 'true'
      };

      console.log("Form Data with existing user data:", formData);

      fetch(`http://localhost:5292/api/UserAccounts/${userId}`, {
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
                  alert(`Error during update: ${errorData.title || "Unknown error"}`);
                  throw new Error(`HTTP error, status = ${response.status}`);
              });
          }
          return response.json();
      })
      .then(data => {
          console.log("Update successful:", data);
          alert("Update successful!");
      })
      .catch(error => {
          console.error("Error during update:", error);
          alert(`Error during update: ${error.message}`);
      });
  }).catch(error => {
      console.error("Error fetching user data:", error);
      alert("Failed to fetch user data.");
  });
}


function FetchUserData(userId) {
  return fetch(`http://localhost:5292/api/UserAccounts/by-id/${userId}`, {
      method: 'GET',
      headers: {
          'Content-Type': 'application/json'
      }
  })
  .then(response => {
      if (!response.ok) {
          throw new Error('Network response was not ok');
      }
      return response.json(); // Parse and return the data so it can be used by the caller
  })
  .catch(error => {
      console.error("Failed to fetch user data:", error);
      throw error; // Rethrow the error to be handled by the caller
  });
}
