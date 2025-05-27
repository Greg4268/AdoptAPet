const profURL = "https://adoptapet-production-1bb7.up.railway.app/api/UserAccounts";
const shelterURL = "https://adoptapet-production-1bb7.up.railway.app/api/Shelters";

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

// determine which account type is selected to call the correct form the register 
function submitForm(event) {

  let formData;
  let accountType = document.getElementById("accountType").value.toLowerCase(); // Normalize input
  // alert(accountType); // Debugging to check account type

  if (accountType === "adopter") {
    formData = {  // Use the already declared formData
      FirstName: document.getElementById("firstName").value.trim(),
      LastName: document.getElementById("lastName").value.trim(),
      Age: document.getElementById("age").value.trim(),
      Email: document.getElementById("adopterEmail").value.trim(),
      Password: document.getElementById("adopterPassword").value,
      deleted: false,
      Address: "N/A",
      YardSize: 0,
      HasForm: false,
      Fenced: false,
      AccountType: "adopter",
    };
    registerAdopter(formData);
  } else if (accountType === "shelter") {
    formData = {  // Use the already declared formData
      ShelterUsername: document.getElementById("shelterName").value.trim(),
      Email: document.getElementById("shelterEmail").value.trim(),
      ShelterPassword: document.getElementById("shelterPassword").value.trim(),
      Address: document.getElementById("shelterAddress").value.trim(),
      AccountType: accountType,
      HoursOfOperation: document.getElementById("shelterHours").value.trim(),
      deleted: false,
      Approved: false,
    };
    registerShelter(formData);
  } else {
    alert("Please select a valid account type");
  }
}


function registerAdopter(formData) {
  //alert("register adopter called")
  fetch("https://adoptapet-production-1bb7.up.railway.app/api/UserAccounts", {
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
      alert("Registration successful!");
      window.location.href = "index.html";
    })
    .catch((error) => {
      console.error("Error during sign up:", error);
      alert(`Error during sign up: ${error.message}`);
    });
}

function registerShelter(formData) {
  fetch("https://adoptapet-production-1bb7.up.railway.app/api/shelters", {
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
      alert("Registration successful!");
      window.location.href = "index.html";
    })
    .catch((error) => {
      console.error("Error during sign up:", error);
      alert(`Error during sign up: ${error.message}`);
    });

    // document.getElementById('signupForm').reset();
    // updateFormFields();
}
