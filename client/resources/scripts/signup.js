const profURL = "http://localhost:5292/api/UserAccounts";

function updateFormFields() {
  var accountType = document.getElementById("accountType").value;
  var adopterFields = document.getElementById("adopterFields");
  var shelterFields = document.getElementById("shelterFields");

  // Initially hide all specific fields
  if (adopterFields) adopterFields.style.display = "none";
  if (shelterFields) shelterFields.style.display = "none";

  // Display fields based on account type
  if (accountType === "adopter" && adopterFields) {
    adopterFields.style.display = "block";
  } else if (accountType === "shelter" && shelterFields) {
    shelterFields.style.display = "block";
  }
}

// form submission for sign up page
function submitForm() {
  if (document.getElementById("accountType").value === "Adopter") {
    Alert("Adopter signup data being sent")
    const formData = {
      FirstName: document.getElementById("firstName").value.trim(),
      LastName: document.getElementById("lastName").value.trim(),
      Age: document.getElementById("age").value.trim(),
      Email: document.getElementById("email").value.trim(),
      Password: document.getElementById("password").value,
      deleted: false,
      Address: "N/A",
      YardSize: 0,
      Fenced: false,
      AccountType: document.getElementById("accountType").value,
    };
  } else if (document.getElementById("accountType").value === "Shelter") {
    Alert("Shelter signup data being sent")
    const formData = {
      ShelterName: document.getElementById("shelterName").value.trim(),
      ShelterEmail: document.getElementById("shelterEmail").value.trim(),
      ShelterPassword: document.getElementById("shelterPassword").value.trim(),
      ShelterAddress: document.getElementById("shelterAddress").value.trim(),
      AccountType: document.getElementById("accountType").value,
      HoursOfOperation: document.getElementById("shelterHours").value.trim(),
    };
  }

  fetch("http://localhost:5292/api/UserAccounts", {
    method: "POST",
    headers: {
      Accept: "application/json",
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
      alert("response ok");
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
