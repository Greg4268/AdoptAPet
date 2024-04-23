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
    const petResponse = await fetch(`http://localhost:5292/api/Pets/${petId}`);
    if (!petResponse.ok) {
      throw new Error("Failed to fetch pet details");
    }
    const pet = await petResponse.json();
    const shelterResponse = await fetch(
      `http://localhost:5292/api/Shelters/${pet.shelterId}`
    );
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

  fetch(`http://localhost:5292/api/Favorite/${userId},${petId}`, {
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
  console.log("Making appointment for Pet ID: ", petId);
  const tokenString = localStorage.getItem("userToken");
  const userToken = JSON.parse(tokenString); // Assuming userToken is a JSON string that includes userId
  let userId = userToken.userId;

  userId = parseInt(userId);
  petId = parseInt(petId);

  console.log("User ID: ", userId);

  DisplayAppointmentForm(petId);
}

function DisplayAppointmentForm(petId) {
  const aptContainer = document.querySelector(".apt-container"); // Ensure the class selector is correctly used.

  // Clear any existing content in the appointment container
  aptContainer.innerHTML = "";

  // Create a new date input element for appointment date
  const dateInput = document.createElement("input");
  dateInput.type = "date";
  dateInput.id = "appointmentDate";
  dateInput.name = "appointmentDate";
  dateInput.className = "form-control"; // Assuming Bootstrap or similar for styling

  // Set the minimum date to today to ensure the date is in the future
  const today = new Date();
  const formattedDate = today.toISOString().substring(0, 10); // Format YYYY-MM-DD
  dateInput.min = formattedDate;

  // Create a button to submit the appointment
  const submitButton = document.createElement("button");
  submitButton.textContent = "Schedule Appointment";
  submitButton.className = "btn btn-primary"; // Bootstrap button styling
  submitButton.onclick = function () {
    scheduleAppointment(petId, today);
  };

  // Append the date input and submit button to the apt-container
  aptContainer.appendChild(dateInput);
  aptContainer.appendChild(submitButton);
}

function scheduleAppointment(petId, today) {
  const appointmentDate = document.getElementById("appointmentDate").value;
  if (!appointmentDate) {
    alert("Please select a date for the appointment.");
    return;
  } else if (appointmentDate < today.toISOString().substring(0, 10)) {
    alert("Please select a future date for the appointment.");
    return;
  }

  console.log(
    "Scheduling appointment for Pet ID:",
    petId,
    "on:",
    appointmentDate
  );


  fetch(`http://localhost:5292/api/Appointments`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      petProfileId: petId,
      userId: parseInt(localStorage.getItem("userToken").userId), 
      appointmentDate: appointmentDate,
    }),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to schedule appointment");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Appointment scheduled successfully:", data);
      alert("Appointment scheduled successfully!");
    })
    .catch((error) => {
      console.error("Error scheduling appointment:", error);
      alert("Failed to schedule appointment. Please try again.");
    });
}
