
const userToken = localStorage.getItem("userToken"); // get user token at form submission 
const tokenData = JSON.parse(userToken); // parse user token
const userId = parseInt(tokenData.userId); // get user id from token 
console.log("User Id: ", userId);


function handleOnLoad() {
  fetchFavPets(userId)
}

async function fetchFavPets(userId) {
  try {
    const response = await fetch(`http://localhost:5292/api/Favorite?user=${userId}`);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const pets = await response.json();
    displayFavPets(pets);
    console.table(pets);
  } catch (error) {
    console.error("Error fetching pets:", error);
  }
}

async function displayFavPets(pets) {
  const petsContainer = document.querySelector(".col:nth-child(3)"); // Selecting the third column (Favorite Pets)
  petsContainer.innerHTML = ""; // Clear existing content

  // Add a single header above the pet cards
  const petsHeader = `
    <h2>Favorite Pets</h2>
  `;
  petsContainer.innerHTML += petsHeader;

  // Generate pet cards
  for (const pet of pets) {
    const petCard = `
      <div class="h-100 p-5 bg-light border rounded-3 pet-card">
        <div class="card-body">
          <img src="${pet.ImageUrl}" alt="${pet.name}" style="opacity: 0.7;">
          <div class="card-info">
            <h2>${pet.name}</h2>
            <h5>${pet.breed}</h5>
            <p>${pet.species}</p>
            <a class="btn btn-outline-secondary" role="button" onclick="redirectToPetProfile(${pet.petProfileId});" href="#">See ${pet.name}</a>
          </div>
        </div>
      </div>
    `;
    petsContainer.innerHTML += petCard; // Append the new card within the Favorite Pets column
  }
}

function loginValPetProfile(petId) {
  const userData = localStorage.getItem("userToken");

  if (userData) {
    window.location.href = `./petProfile.html?petId=${petId}`;
  } else {
    alert("Please login first.");
    window.location.href = "./login.html";
  }
}

function redirectToPetProfile(petId) {
  window.location.href = `./petProfile.html?petId=${petId}`;
}