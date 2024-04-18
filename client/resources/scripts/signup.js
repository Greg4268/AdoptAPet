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

  let formData;
  let accountType = document.getElementById("accountType").value;

  if (accountType === "adopter") {

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
      AccountType: accountType,
    };
    registerAdopter(formData);
  } else if (accountType === "shelter") {

    const formData = {
      ShelterUsername: document.getElementById("shelterName").value.trim(),
      ShelterEmail: document.getElementById("shelterEmail").value.trim(),
      ShelterPassword: document.getElementById("shelterPassword").value.trim(),
      Address: document.getElementById("shelterAddress").value.trim(),
      AccountType: accountType,
      HoursOfOperation: document.getElementById("shelterHours").value.trim(),
      deleted: false,
      approved: false,
    };
    registerShelter(formData);
  } else {
    alert("Please select a valid account type");
  }
  if (event) event.preventDefault();
}

function registerAdopter(formData) {
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
        return response.text().then((text) => {
          try {
            // Try to parse it as JSON
            const data = JSON.parse(text);
            console.error("Server responded with error:", data);
            alert(`Error signing up: ${data.title || "Unknown error"}`);
          } catch {
            // If not JSON, use the text directly
            alert(`Error signing up: ${text}`);
          }
          throw new Error(`HTTP error, status = ${response.status}`);
        });
      }
      // Handle no content
      if (
        response.headers.get("Content-Length") === "0" ||
        response.status === 204
      ) {
        console.log("No content to parse");
        return {};
      } else {
        return response.json();
      }
    })
    .then((data) => {
      console.log("Server response data:", data);
    })
    .catch((error) => {
      console.error("Error during sign up:", error);
      alert(`Error during sign up: ${error.message}`);
    });

    // document.getElementById('signupForm').reset();
    // updateFormFields();
}
