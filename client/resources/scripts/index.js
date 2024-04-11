const petsURL = "http://localhost:5292/api/Pets";

function handleOnLoad() {
  fetchPets(petsURL);
}

async function fetchPets(petsURL) {
  fetch(petsURL)
    .then((response) => {
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      return response.json(); // Parse the JSON from the response
    })
    .then((pets) => {
      displayPets(pets); // Pass the fetched pets to displayPets function
      console.log("Pets Objects: ", pets)
    })
    .catch((error) => console.error("Error fetching pets:", error));
}

function displayPets(pets) {

  const petsContainer = document.querySelector(".row.align-items-md-stretch"); // Adjust the selector as necessary
  petsContainer.innerHTML = ""; // Clear existing content

  pets.forEach((pet) => {
    const petCard = `
      <div class="col-md-4">
        <div class="h-100 p-5 bg-body-tertiary border rounded-3">
          <h2>${pet.name}</h2>
          <h5>${pet.breed}</h5>
          <p>${pet.species}</p>
          <a class="btn btn-outline-secondary" role="button" href="./petProfile.html">See ${pet.name}</a>
        </div>
      </div>
    `;
    petsContainer.innerHTML += petCard; // Append the new card
  });
}

// event listener to update profile icon dropdown once logged in
document.addEventListener("DOMContentLoaded", function () {
  var profileDropdown = document.getElementById("profileDropdownMenu");
  var dropdownMenu = profileDropdown.nextElementSibling;

  // Check for user authentication (e.g., checking local storage for a token)
  var isAuthenticated = localStorage.getItem("userToken") !== null;

  if (isAuthenticated) {
    // User is signed in, populate the dropdown menu
    // need to update so that dashboard link will redirect to either adminDash.html or adopterDash.html or shelterDash.html
    var dashboardLink =
      '<a class="dropdown-item" href="./adminDash.html">Dashboard</a>';
    var logoutLink = '<a class="dropdown-item" href="./logout.html">Logout</a>'; // update 
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

// let table = new DataTable('#myTable', {
//   responsive: true
//   // config options...
// });

