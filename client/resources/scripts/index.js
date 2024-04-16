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

// let table = new DataTable('#myTable', {
//   responsive: true
//   // config options...
// });

