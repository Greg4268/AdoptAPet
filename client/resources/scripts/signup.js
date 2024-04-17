const profURL = "http://localhost:5292/api/UserAccounts";

async function fetchProfiles(profURL) {
  try {
    const response = await fetch(profURL);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const profiles = await response.json();
    displayPets(profiles);
    console.log("Profiles Objects: ", profiles);
  } catch (error) {
    console.error("Error fetching pets:", error);
  }
}

// form submission for sign up page
function submitForm() {
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
