const userURL = "https://adoptapet-production-1bb7.up.railway.app:5292/api/UserAccounts";

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

async function SubmitForm() {
  const userToken = localStorage.getItem("userToken"); // get user token at form submission 
  const tokenData = JSON.parse(userToken); // parse user token
  const userId = parseInt(tokenData.userId); // get user id from token 
  console.log("User Id: ", userId);

  FetchUserData(userId).then(existingUserData => {
      if (!existingUserData) {
          console.error("No user data received");
          alert("Failed to fetch user data.");
          return;
      }

      console.log("Existing user data:", existingUserData);

      let Address = document.getElementById("address").value.trim();
      let YardSize = parseInt(document.getElementById("yardSize").value.trim());
      let Fenced = document.getElementById("fencedIn").value.trim().toLowerCase() === 'true'

      const formData = {
          UserId: existingUserData.userId,
          FirstName: existingUserData.firstName,
          LastName: existingUserData.lastName,
          Email: existingUserData.email,
          Password: existingUserData.password,
          AccountType: existingUserData.accountType,
          Address: Address,
          YardSize: YardSize,
          Fenced: Fenced
      };

      console.log("Form Data with existing user data:", formData);

      fetch(`https://adoptapet-production-1bb7.up.railway.app/api/UserAccounts/${userId}`, {
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


async function FetchUserData(userId) {
    console.log(userId);

  return fetch(`https://adoptapet-production-1bb7.up.railway.app/api/UserAccounts/by-id/${userId}`, {
      method: 'GET',
      headers: {
          "Content-Type": 'application/json'
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
