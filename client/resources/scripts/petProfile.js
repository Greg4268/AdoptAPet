document.addEventListener("DOMContentLoaded", function () {
  const urlParams = new URLSearchParams(window.location.search);
  const petId = urlParams.get("petId");
  if (petId) {
    fetchPetDetails(petId);
  } else {
    alert("Pet not found!");
    window.location.href = "./index.html"; // Optionally redirect if no pet ID is found
  }
});

async function fetchPetDetails(petId) {
  try {
    // Fetch pet details
    const petResponse = await fetch(`http://localhost:5292/api/Pets/${petId}`);
    if (!petResponse.ok) {
      throw new Error("Failed to fetch pet details");
    }
    const pet = await petResponse.json();

    // Fetch shelter details using the pet's shelterId
    const shelterResponse = await fetch(
      `http://localhost:5292/api/Shelters/${pet.shelterId}`
    );
    if (!shelterResponse.ok) {
      throw new Error("Failed to fetch shelter details");
    }
    const shelter = await shelterResponse.json();
    const shelterUsername = shelter.shelterUsername;

    // Update DOM with pet and shelter details
    document.getElementById("petDescription").textContent =
      pet.name +
      " is a fun and loving " +
      pet.breed +
      " who loves treats and attention!";
    document.getElementById("petName").textContent = "Meet " + pet.name;
    document.getElementById("petImage").src = pet.imageUrl;
    document.getElementById("petImage").alt = `Image of ${pet.name}`;
    document.getElementById("petBreed").textContent = "Breed: " + pet.breed;
    document.getElementById("petSpecies").textContent =
      "Species: " + pet.species;
    document.getElementById("petAge").textContent = "Age: " + pet.age;
    document.getElementById("shelterName").textContent =
      "Shelter: " + shelterUsername;
    // Populate other elements as needed
  } catch (error) {
    console.error("Error loading pet and shelter details:", error);
    alert("Error loading pet and shelter details.");
  }
}
