const petsURL = ""; // needs to be the localhost link: ex. http://localhost:5604/Pets
const testURL = "http://localhost:5292/WeatherForecast";

function testFunc() {
  console.log("testFunc() function called on load.");
  displayPets();
}

let pets = [
  {
    name: "Max",
    breed: "Labrador",
    description: "A friendly and energetic Labrador.",
  },
  {
    name: "Bella",
    breed: "German Shepherd",
    description: "Loyal and protective companion.",
  },
  {
    name: "Luna",
    breed: "Golden Retriever",
    description: "Fun and friendly companion.",
  },
  { name: "Josie", breed: "Labrador", description: "Nervous but loving buddy" },
  // Add more pets as needed
]; // array of pets while fetch function is currently getting fixed

function fetchPets(petsURL) {
  console.log("fetchPets() function called.");
  fetch(petsURL)
    .then((response) => response.json())
    .then((pets) => {
      displayPets(pets);
      console.log(pets);
    })
    .catch((error) => console.error("Error fetching pets:", error));
}

function displayPets() {
  console.log("displayPets() function called.");
  const petsContainer = document.querySelector(".row.align-items-md-stretch"); // Adjust if necessary
  petsContainer.innerHTML = ""; // Clear existing content

  pets.forEach((pet) => {
    const petCard = `
          <div class="col-md-4">
              <div class="h-100 p-5 bg-body-tertiary border rounded-3">
                  <h2>${pet.name}</h2>
                  <h5>${pet.breed}</h5>
                  <p>${pet.description}</p>
                  <a class="btn btn-outline-secondary" role="button" href="./petProfile.html">See ${pet.name}</a>
              </div>
          </div>
      `;

    petsContainer.innerHTML += petCard; // Append the new card
  });
}

// event listener to check login button is clicked and setting a userToken in local storage
// so that other event listener can display profile icon 'dashboard' & 'logout' options
// also redirects to index.html main page
document.addEventListener("DOMContentLoaded", function () {
  var loginButton = document.getElementById("loginButton");
  loginButton.addEventListener("click", function () {
    (window.location.href = "./index.html"),
      localStorage.setItem("userToken", "password"),
      console.log(localStorage.getItem("userToken"));
  });
});

// event listener to check register button is clicked then routing back to main page index.html
document.addEventListener("DOMContentLoaded", function () {
  console.log("DOMContentLoaded fired!");
  var registerButton = document.getElementById("registerButton");
  console.log("Button element:", registerButton);
  registerButton.addEventListener("click", function () {
    window.location.href = "./index.html";
  });
});

// event listener to update profile icon dropdown once logged in
document.addEventListener("DOMContentLoaded", function () {
  var profileDropdown = document.getElementById("profileDropdownMenu");
  var dropdownMenu = profileDropdown.nextElementSibling;

  // Check for user authentication (e.g., checking local storage for a token)
  var isAuthenticated = localStorage.getItem("userToken") !== null;

  if (isAuthenticated) {
    // User is signed in, populate the dropdown menu
    var dashboardLink =
      '<a class="dropdown-item" href="/dashboard.html">Dashboard</a>';
    var logoutLink = '<a class="dropdown-item" href="/logout.html">Logout</a>';
    dropdownMenu.innerHTML = dashboardLink + logoutLink;

    // Attach onclick event to show/hide the dropdown
    profileDropdown.onclick = function (event) {
      dropdownMenu.style.display =
        dropdownMenu.style.display === "block" ? "none" : "block";
    };
  } else {
    // User is not signed in, disable the dropdown
    profileDropdown.style.cursor = "default";
    profileDropdown.onclick = function (event) {
      event.preventDefault(); // Prevents any attached link actions if accidentally used in <a> tags
    };
  }

  // Handle clicking outside the dropdown to close it
  window.onclick = function (event) {
    if (!event.target.matches("#profileDropdownMenu")) {
      if (dropdownMenu.style.display === "block") {
        dropdownMenu.style.display = "none";
      }
    }
  };
});
