const petsUrl = "https://adoptapet-production-1bb7.up.railway.app/api/Pets";
const shelterUrl = "https://adoptapet-production-1bb7.up.railway.app/api/Shelters";

const userToken = localStorage.getItem("userToken");
const tokenData = JSON.parse(userToken);
const shelterId = parseInt(tokenData.shelterId);
console.log("Shelter ID: ", shelterId);

function compareByFavCount(a, b)
{
  return b.favoriteCount - a.favoriteCount;
}

document.addEventListener("DOMContentLoaded", () => {
  fetchAndDisplayPets(shelterId);
});

function fetchAndDisplayPets(shelterId) 
{
  fetch(`${shelterUrl}/${shelterId}/Pets`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${userToken}`,
      "Content-Type": "application/json",
    },
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to fetch pets");
      }
      return response.json();
    })
    .then((pets) => {
      const tableBody = document
        .getElementById("pets-table")
        .getElementsByTagName("tbody")[0];
      tableBody.innerHTML = "";

      pets.sort(compareByFavCount);
      pets.forEach((pet) => {
        const row = tableBody.insertRow();

        let cell = row.insertCell(0);
        cell.textContent = pet.name;
        cell.className = "text-center";

        cell = row.insertCell(1);
        cell.textContent = pet.breed;
        cell.className = "text-center";

        cell = row.insertCell(2);
        cell.textContent = pet.species;
        cell.className = "text-center";

        cell = row.insertCell(3);
        cell.textContent = pet.age;
        cell.className = "text-center";

        cell = row.insertCell(4);
        cell.textContent = pet.favoriteCount;
        cell.className = "text-center";

        const actionsCell = row.insertCell(5);
        actionsCell.innerHTML = `<button onclick="editPet(${pet.petProfileId})" class="btn btn-primary">Edit</button>
                                 <button onclick="deletePet(${pet.petProfileId})" class="btn btn-danger">Delete</button>`;
        actionsCell.className = "text-center";
      });
    })
    .catch((error) => {
      console.error("Error fetching pets:", error);
    });
}

function editPet(petId) 
{
  fetch(`${petsUrl}/${petId}`)
    .then((response) => response.json())
    .then((pet) => {
      let newName = prompt("Enter new name for the pet:", pet.Name);
      if (newName === null || newName.trim() === "") return;

      let newBreed = prompt("Enter new breed for the pet:", pet.Breed);
      if (newBreed === null || newBreed.trim() === "") return;

      let newAge = prompt("Enter new age for the pet:", pet.Age);
      if (newAge === null || newAge.trim() === "") return;

      const updatedPet = {
        Name: newName,
        Breed: newBreed,
        Age: newAge,
        Species: pet.species,
        ImageUrl: pet.imageUrl,
        BirthDate: pet.birthDate,
        deleted: pet.deleted,
        ShelterId: pet.shelterId,
        favoriteCount: pet.favoriteCount,
      };
      updatePet(petId, updatedPet);
    })
    .catch((error) => {
      console.error("Error fetching pet details:", error);
    });
}

function updatePet(petId, updatedPet) 
{
  fetch(`${petsUrl}/${petId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("userToken")}`,
    },
    body: JSON.stringify(updatedPet),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to update pet");
      }
      alert("Pet updated successfully");
      fetchAndDisplayPets(shelterId);
    })
    .catch((error) => {
      console.error("Error updating pet:", error);
    });
}

function deletePet(petId) 
{
  if (!confirm("Are you sure you want to delete this pet?")) {
    return;
  }
  const token = localStorage.getItem("userToken");
  fetch(`${petsUrl}/${petId}`, {
    method: "DELETE",
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  })
    .then((response) => {
      if (response.ok) {
        console.log("Pet deleted successfully");
        alert("Pet has been deleted successfully.");
        fetchAndDisplayPets(shelterId);
      } else {
        throw new Error("Failed to delete the pet");
      }
    })
    .catch((error) => {
      console.error("Error deleting pet:", error);
      alert("Error deleting pet: " + error.message);
    });
}

function toggleAddPetForm() 
{
  const form = document.getElementById("add-pet-form");
  form.style.display = form.style.display === "none" ? "block" : "none";
}

async function addPet(event) {
  event.preventDefault(); 

  const name = document.getElementById("petName").value;
  const breed = document.getElementById("petBreed").value;
  const species = document.getElementById("petSpecies").value;
  const age = document.getElementById("petAge").value;
  const imageUrl = document.getElementById("petImageUrl").value;
  var currentDateTime = new Date();
  if(!name || !breed || !species || isNaN(age))
  {
    alert("Invalid Input: Please enter valid information for each field.");
    return;
  }
  const pet = {
    Name: name,
    Breed: breed,
    Species: species,
    Age: parseInt(age, 10),
    ImageUrl: imageUrl,
    ShelterId: shelterId,
    deleted: false,
    FavoriteCount: 0,
    BirthDate: currentDateTime
  };
  console.log(JSON.stringify(pet));

  const response =  await fetch(petsUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("userToken")}`,
    },
    body: JSON.stringify(pet)
  })
  fetchAndDisplayPets(shelterId);
}
