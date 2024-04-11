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
