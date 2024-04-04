const petsURL = "wftuqljwesiffol6.cbetxkdyhwsb.us-east-1.rds.amazonaws.com"
async function fetchPets() {
  fetch(petsURL)
    .then((response) => response.json())
    .then((pets) => {
      displayPets(pets);
      console.log(pets);
    })
    .catch((error) => console.error("Error fetching pets:", error));
}

async function displayPets(pets) {
  console.log(JSON.stringify(pets));
}
