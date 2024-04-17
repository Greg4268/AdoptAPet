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
    AccountType: document.getElementById("accountType").value,
    deleted: false,
    Address: "N/A",
  };

  fetch("http://localhost:5292/api/UserAccounts", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => response.json()) // Convert the response to JSON
    .then((data) => {
      Alert("Server response data:", data); // Log the response data
    });
}
