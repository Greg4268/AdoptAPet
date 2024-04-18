const profURL = "http://localhost:5292/api/UserAccounts";
const shelterURL = "http://localhost:5292/api/Shelters";

function updateFormFields() {
  console.log("Updating form fields displayed");
  var accountType = document.getElementById("accountType").value;
  var adopterFields = document.getElementById("adopterFields");
  var shelterFields = document.getElementById("shelterFields");

  // Select all input fields within adopterFields and shelterFields
  var adopterInputs = adopterFields.querySelectorAll("input");
  var shelterInputs = shelterFields.querySelectorAll("input");

  // Disable all input fields initially
  adopterInputs.forEach((input) => {
    input.disabled = true;
  });
  shelterInputs.forEach((input) => {
    input.disabled = true;
  });

  // Enable and show only the fields relevant to the selected account type
  if (accountType === "adopter") {
    adopterInputs.forEach((input) => {
      input.disabled = false;
    });
    adopterFields.style.display = "block";
    shelterFields.style.display = "none";
  } else if (accountType === "shelter") {
    shelterInputs.forEach((input) => {
      input.disabled = false;
    });
    shelterFields.style.display = "block";
    adopterFields.style.display = "none";
  }
}

// Call updateFormFields on page load to set the correct initial state
// document.addEventListener("DOMContentLoaded", updateFormFields);

// form submission for sign up page
function submitForm(event) {
  alert("submitForm() has been called");

  let formData;
  let accountType = document.getElementById("accountType").value;

  if (accountType === "adopter") {
    alert("Adopter signup data being sent");

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
    registerAdopter(formData);
  } else if (accountType === "shelter") {
    alert("Shelter signup data being sent");

    const formData = {
      ShelterName: document.getElementById("shelterName").value.trim(),
      ShelterEmail: document.getElementById("shelterEmail").value.trim(),
      ShelterPassword: document.getElementById("shelterPassword").value.trim(),
      ShelterAddress: document.getElementById("shelterAddress").value.trim(),
      AccountType: document.getElementById("accountType").value,
      HoursOfOperation: document.getElementById("shelterHours").value.trim(),
    };
    registerShelter(formData);
  } else {
    alert("Please select a valid account type");
  }
  if (event) event.preventDefault();
}

function registerAdopter(formData) {
  alert("registerAdopter() has been called");
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

function registerShelter(formData) {
  alert("registerShelter() has been called");
  fetch("http://localhost:5292/api/shelters", {
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
