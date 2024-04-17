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
  alert("form submitting process begun");

  //   const formData = {
  //     firstName: document.getElementById("firstName").value.trim(),
  //     lastName: document.getElementById("lastName").value.trim(),
  //     email: document.getElementById("email").value.trim(),
  //     password: document.getElementById("password").value,
  //     accountType: document.getElementById("accountType").value,
  //   };

  // Hardcoded test data
  const formData = {
    FirstName: "John",
    LastName: "Doe",
    Age: 13,
    Email: "johndoe@example.com",
    Password: "password123",
    deleted: false,
    Address: "123 Orchard Ave",
    YardSize: 123,
    Fenced: false,
    AccountType: "Adopter",
    // BirthDate: Date.parse("1984-12-11T00:00:00"),
  };
  //accountType = "adopter";

  fetch("http://localhost:5292/api/UserAccounts", {
    method: "POST",
    headers: {
      "Accept": 'application/json',
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => {
      if (!response.ok) {
        // Extracting error message from response body
        return response.json().then(errorData => {
          
          console.error("Server responded with error:", errorData);
          alert(`Error signing up: ${errorData.title || "Unknown error"}`);
          throw new Error(`HTTP error, status = ${response.status}`);
        });
      }
      alert("response ok")
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
