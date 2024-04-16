const petsURL = "http://localhost:5292/api/Pets";
// const imageURL = "https://dog.ceo/api/breeds/image/random";
let allPets = [];

function handleOnLoad() {
  fetchPets(petsURL);
}

async function fetchPets(petsURL) {
  try {
    const response = await fetch(petsURL);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const pets = await response.json();
    allPets = pets;
    displayPets(pets);
    console.log("Pets Objects: ", pets);
  } catch (error) {
    console.error("Error fetching pets:", error);
  }
}

async function fetchImage() {
  try {
    const response = await fetch(imageURL);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const imageData = await response.json();
    return imageData.message; // Directly return the image URL
  } catch (error) {
    console.error("Error fetching image:", error);
    return "default-dog.jpg"; // Fallback image if fetch fails
  }
}

async function displayPets(pets) {
  const petsContainer = document.querySelector(".row.align-items-md-stretch");
  petsContainer.innerHTML = ""; // Clear existing content

  for (const pet of pets) {
    //const imageUrl = await fetchImage(); // Fetch image for each pet
    const petCard = `
      <div class="col-md-4">
        <div class="h-100 p-5 bg-light border rounded-3 pet-card">
          <div class="card-body">
            <div class="card-info">
              <h2>${pet.name}</h2>
              <h5>${pet.breed}</h5>
              <p>${pet.species}</p>
              <a class="btn btn-outline-secondary" role="button" href="./petProfile.html?petId=${pet.id}">See ${pet.name}</a>
            </div>
            <img src="${pet.image}" class="ms-3">
          </div>
        </div>
      </div>
    `;
    petsContainer.innerHTML += petCard; // Append the new card
  }
}


document.getElementById('searchForm').addEventListener('submit', function(event) {
  event.preventDefault();
  const searchQuery = document.getElementById('searchInput').value.toLowerCase();
  searchPets(searchQuery);
});

function searchPets(query) {
  const filteredPets = allPets.filter(pet => 
      pet.name.toLowerCase().includes(query) || pet.breed.toLowerCase().includes(query)
  );
  displayPets(filteredPets);
}

document.getElementById('clearSearch').addEventListener('click', function() {
  document.getElementById('searchInput').value = '';  // Clear the search input
  displayPets(allPets);  // Display all pets
});
