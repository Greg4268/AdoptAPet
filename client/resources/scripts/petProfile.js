import API_ENDPOINTS from "./apiConfig";

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
    const petResponse = await fetch(API_ENDPOINTS.pets + `/${petId}`);
    if (!petResponse.ok) {
      throw new Error("Failed to fetch pet details");
    }
    const pet = await petResponse.json();
    const shelterResponse = await fetch(API_ENDPOINTS.shelters + `/${pet.shelterId}`);
    if (!shelterResponse.ok) {
      throw new Error("Failed to fetch shelter details");
    }
    const shelter = await shelterResponse.json();

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
      "Shelter: " + shelter.shelterUsername;

    // Update button onclick attributes to include only petId since userId will be fetched in the function
    document
      .querySelector("button[onclick='FavoritePet();']")
      .setAttribute("onclick", `FavoritePet('${petId}');`);
    document
      .querySelector("button[onclick='MakeAppointment();']")
      .setAttribute("onclick", `MakeAppointment('${petId}');`);
  } catch (error) {
    console.error("Error loading pet and shelter details:", error);
    alert("Error loading pet and shelter details.");
  }
}

function FavoritePet(petId) {
  const tokenString = localStorage.getItem("userToken");
  const userToken = JSON.parse(tokenString); // Assuming userToken is a JSON string that includes userId
  let userId = userToken.userId;

  userId = parseInt(userId);
  petId = parseInt(petId);

  console.log("User ID: ", userId);
  console.log("Pet ID: ", petId);

  fetch(API_ENDPOINTS.fav_pet + `/${userId},${petId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to toggle favorite");
      }
      return response.text(); // Assuming no body is returned
    })
    .then((result) => {
      console.log("Favorite toggled successfully:", result);
      alert("Pet Favorited!");
    })
    .catch((error) => {
      console.error("Error toggling favorite:", error);
    });
}

function MakeAppointment(petId) {
  console.log("MakeAppointment Function Called For Pet ID: ", petId);
  const tokenString = localStorage.getItem("userToken");
  const userToken = JSON.parse(tokenString);
  // console.log(userToken);

  let userId = userToken.userId;

  //console.log("User ID: ", userId, " Pet ID: ", petId);

  DisplayAppointmentForm(petId, userId);
}

function DisplayAppointmentForm(petId, userId) {
  const aptContainer = document.querySelector(".apt-container"); // Ensure the class selector is correctly used.

  console.log("DisplayAppointmentForm Function Called"); // document path for error checking

  // Clear any existing content in the appointment container
  aptContainer.innerHTML = "";

  // Create a new date input element for appointment date
  const dateInput = document.createElement("input");
  dateInput.type = "date";
  dateInput.id = "appointmentDate";
  dateInput.name = "appointmentDate";
  dateInput.className = "form-control";

  let today = new Date(); // Get current date
  let formattedDate = today.toISOString().substring(0, 10); // Format YYYY-MM-DD

  // Create a button to submit the appointment
  let submitButton = document.createElement("button");
  submitButton.textContent = "Schedule Appointment";
  submitButton.className = "btn btn-primary";
  submitButton.onclick = function () {
    scheduleAppointment(petId, formattedDate, userId);
  };

  // Append the date input and submit button to the apt-container
  aptContainer.appendChild(dateInput);
  aptContainer.appendChild(submitButton);
}

function scheduleAppointment(petId, formattedDate, userId) {
  console.log("scheduleAppointment Function Called"); // document path for error checking

  let appointmentDate = document.getElementById("appointmentDate").value;
  if (!appointmentDate) {
    alert("Please select a date for the appointment.");
  } else if (appointmentDate < formattedDate) {
    alert("Please select a future date for the appointment.");
  }
  console.log("userId: ", userId, " petId: ", petId, " appointmentDate: ", appointmentDate);
  petId = parseInt(petId)
  userId = parseInt(userId)
  // appointmentDate = new Date(appointmentDate)
  console.log("userId: ", userId, " petId: ", petId, " appointmentDate: ", appointmentDate);
  
  fetch(API_ENDPOINTS.appointments, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      PetProfileId: petId,
      UserId: userId,
      AppointmentDate: appointmentDate,
      Deleted: false,
    }),
  })
    .then(
      console.log("Appointment Scheduled Successfully"),
      alert("Appointment Scheduled Successfully")
    )
}

