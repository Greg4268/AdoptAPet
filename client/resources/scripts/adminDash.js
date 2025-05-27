const shelterURL = "https://adoptapet-production-1bb7.up.railway.app:8080/api/Shelters";
const usersURL = "https://adoptapet-production-1bb7.up.railway.app:8080/api/UserAccounts";
const petsURL = "https://adoptapet-production-1bb7.up.railway.app:8080/api/Pets";
const appointmentsURL = "https://adoptapet-production-1bb7.up.railway.app:8080/api/Appointments";

$(document).ready(function () {
  $("#table1").DataTable();
});

$(document).ready(function () {
  $("#table2").DataTable();
});

function handleOnLoad() {
  fetchShelters(shelterURL);
  fetchUsers(usersURL);
  fetchPets(petsURL);
  fetchAppointments(appointmentsURL);
}

async function fetchShelters(shelterURL) {
  try {
    const response = await fetch(shelterURL);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const shelters = await response.json();
    displayShelters(shelters);
    console.log("Shelters Objects: ", shelters);
  } catch (error) {
    console.error("Error fetching shelters:", error);
  }
}

async function fetchUsers(usersURL) {
  try {
    const response = await fetch(usersURL);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const users = await response.json();
    displayUsers(users);
    console.log("users Objects: ", users);
  } catch (error) {
    console.error("Error fetching users:", error);
  }
}

async function fetchPets(petsURL) {
  try {
    const response = await fetch(petsURL);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const pets = await response.json();
    displayPets(pets);
    console.log("Pets Objects: ", pets);
  } catch (error) {
    console.error("Error fetching pets:", error);
  }
}

async function fetchAppointments(appointmentsURL) {
  try {
    const response = await fetch(appointmentsURL);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const appointments = await response.json();
    displayAppointments(appointments);
    console.log("Appointments Objects: ", appointments);
  } catch (error) {
    console.error("Error fetching appointments:", error);
  }
}

async function displayShelters(shelters) {
  const shelterContainer = document.querySelector(".shelter-container");
  shelterContainer.innerHTML = ""; // Clear existing content

  for (const shelter of shelters) {
    const sheltersCard = `
        <tr>
            <td>${shelter.shelterUsername}</td>
            <td>${shelter.email}</td>
            <td>${shelter.shelterId}</td>
            <td>${shelter.hoursOfOperation}</td>
            <td>${shelter.address}</td>
            <td>${shelter.approved}</td>
            <td><button onclick="AltShelterApproval(${shelter.shelterId}, ${shelter.approved})">Approve/Revoke Approval</button></td>
          </tr>
      `;
    shelterContainer.innerHTML += sheltersCard; 
  }
}

async function displayUsers(users) {
  const usersContainer = document.querySelector(".users-container");
  usersContainer.innerHTML = ""; 

  for (const user of users) {
    //const imageUrl = await fetchImage(); // Fetch image for each pet
    const usersCard = `
          <tr>
              <td>${user.userId}</td>
              <td>${user.firstName + " " + user.lastName}</td>
              <td>${user.email}</td>
              <td>${user.hasForm}</td>
              <td>${user.accountType}</td>
              <td>${user.deleted}</td>
              <td><button onclick="softDeleteUser(${user.userId},${
      user.deleted
    })">delete / undelete </td>
            </tr>
      `;
    usersContainer.innerHTML += usersCard; 
  }
}

async function displayPets(pets) {
  const petsContainer = document.querySelector(".pet-container");
  petsContainer.innerHTML = ""; // Clear existing content

  for (const pet of pets) {
    //const imageUrl = await fetchImage(); // Fetch image for each pet
    const petsCard = `
          <tr>
              <td>${pet.petProfileId}</td>
              <td>${pet.name}</td>
              <td>${pet.species}</td>
              <td>${pet.breed}</td>
              <td>${pet.favoriteCount}</td>
              <td>${pet.deleted}</td>
              <td>${pet.shelterId}</td>
              <td><button onclick="softDeletePet(${pet.petProfileId},${pet.deleted})">delete / undelete </td>
            </tr>
      `;
    petsContainer.innerHTML += petsCard; // Append the new card
  }
}

async function displayAppointments(appointments) {
  const aptsContainer = document.querySelector(".apt-container");
  aptsContainer.innerHTML = ""; // Clear existing content

  for (const appointment of appointments) {
    //const imageUrl = await fetchImage(); // Fetch image for each pet
    const aptCard = `
          <tr>
              <td>${appointment.appointmentId}</td>
              <td>${appointment.appointmentDate}</td>
              <td>${appointment.userId}</td>
              <td>${appointment.petProfileId}</td>
              <td>${appointment.deleted}</td>
              <td><button onclick="deleteAppointment(${appointment.appointmentId})">delete</td>
            </tr>
      `;
    aptsContainer.innerHTML += aptCard; 
  }
}

function AltShelterApproval(shelterId, approved) {
  console.log("Modifying shelter approval of shelter ID: ", shelterId);
  console.log(approved)

  fetch(`https://adoptapet-production-1bb7.up.railway.app:5292/api/Shelters/${shelterId}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => {
      alert("Shelter access switched")
      if (!response.ok) {
        return response.json().then((data) => {
          throw new Error(
            data.message || "Failed to toggle user deletion status"
          );
        });
      }
      return response.json();
    })
    .then((data) => {
      console.log("Deletion status toggled successfully", data);
      // Call fetchUsers to update the table with the current information
      fetchUsers(usersURL);
    })
    .catch((error) => {
      console.error("Error toggling deletion status:", error);
    });
}

/* soft delete user is not successfully updating the information on the table post changing deleted status
function softDeleteUser(userId, deleted) {
  console.log("Toggling deletion status for user ID:", userId);

  fetch(`https://adoptapet-production-1bb7.up.railway.app:5292/api/UserAccounts/${userId}/toggle-delete`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      UserId: userId,
      deleted: deleted,
    }),
  })
    .then((response) => {
      if (!response.ok) {
        // To handle non-2xx HTTP responses, we throw an Error here so it can be caught in the catch block
        return response.json().then((data) => {
          throw new Error(
            data.message || "Failed to toggle user deletion status"
          );
        });
      }
      return response.json();
    })
    .then((data) => {
      console.log("Deletion status toggled successfully", data);
      // Call fetchUsers to update the table with the current information
      fetchUsers(usersURL);
    })
    .catch((error) => {
      console.error("Error toggling deletion status:", error);
    });
}
*/
async function deleteAppointment(appointmentId) {
  try {
    const response = await fetch(`https://adoptapet-production-1bb7.up.railway.app:5292/api/Appointments/${appointmentId}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        AppointmentId: appointmentId,
        deleted: true,
      }),
    });
  } catch (error) {
    console.error('Error deleting appointment:', error.message);
  }
}

function softDeletePet(petProfileId, deleted) {
  console.log("Toggling deletion status for pet ID:", petProfileId);

  fetch(`https://adoptapet-production-1bb7.up.railway.app:5292/api/Pets/${petProfileId}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      petProfileId: petProfileId,
      deleted: deleted,
    }),
  })
    .then((response) => {
      if (!response.ok) {
        return response.json().then((data) => {
          throw new Error(
            data.message || "Failed to toggle user deletion status"
          );
        });
      }
      return response.json();
    })
    .then((data) => {
      console.log("Deletion status toggled successfully", data);
      fetchUsers(usersURL);
    })
    .catch((error) => {
      console.error("Error toggling deletion status:", error);
    });
}

function softDeleteAppointment(appointmentId, deleted) {
  console.log("Toggling deletion for appointment: ", appointmentId);

  fetch(`https://adoptapet-production-1bb7.up.railway.app:5292/api/Appointments/${appointmentId}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      appointmentId: appointmentId,
      deleted: deleted,
    }),
  })
    .then((response) => {
      if (!response.ok) {
        return response.json().then((data) => {
          throw new Error(
            data.message || "Failed to toggle user deletion status"
          );
        });
      }
      return response.json();
    })
    .then((data) => {
      console.log("Deletion status toggled successfully", data);
      fetchUsers(usersURL);
    })
    .catch((error) => {
      console.error("Error toggling deletion status:", error);
    });
}
