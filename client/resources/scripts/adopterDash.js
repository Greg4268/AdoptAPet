
const userToken = localStorage.getItem("userToken"); // get user token at form submission 
const tokenData = JSON.parse(userToken); // parse user token
const userId = parseInt(tokenData.userId); // get user id from token 
console.log("User Id: ", userId);

$(document).ready(function () {
  $("#myTable").DataTable();
});

$(document).ready(function () {
  $("#table2").DataTable();
});

function handleOnLoad() {
  fetchFavPets(userId)
  fetchUserInfo(userId)
  displayAppointments(userId)
}

async function fetchFavPets(userId) {
  try {
    const response = await fetch(`https://adoptapet-production-1bb7.up.railway.app/api/Favorite?user=${userId}`);
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

async function fetchUserInfo(userId) {
  try {
    const response = await fetch(`https://adoptapet-production-1bb7.up.railway.app/api/UserAccounts/by-id/${userId}`);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const user = await response.json();
    displayUserInfo(user);
    console.table(user);
  } catch (error) {
    console.error("Error fetching user info:", error);
  }
}

async function fetchUserAppointments(userId){

}

async function displayFavPets(pets) {
  const petsContainer = document.querySelector(".col:nth-child(3)"); 
  petsContainer.innerHTML = ""; 

  const petsHeader = `
    <h2>Favorite Pets</h2>
  `;
  petsContainer.innerHTML += petsHeader;

  for (const pet of pets) {
    const petCard = `
      <div class="h-100 p-5 bg-light border rounded-3 pet-card">
        <div class="card-body">
          <img src="${pet.imageUrl}" alt="${pet.name}";">
          <div class="card-info">
            <h2>${pet.name}</h2>
            <h5>${pet.breed}</h5>
            <p>${pet.species}</p>
            <a class="btn btn-outline-secondary" role="button" onclick="redirectToPetProfile(${pet.petProfileId});" href="#">See ${pet.name}</a>
          </div>
        </div>
      </div>
    `;
    petsContainer.innerHTML += petCard;
  }
}

async function displayUserInfo(user) {
  console.log("display user info called")
  const labelsContainer = document.querySelector('.labels');
  const dataContainer = document.querySelector('.data');

  labelsContainer.innerHTML = '';
  dataContainer.innerHTML = '';

  const userInfo = {
    "Full Name": user.firstName + " " + user.lastName,
    "Email": user.email,
    "Age": user.age,
    "Address": user.address,
    "Account Type": user.accountType,
    "Yard Size": user.yardSize + " sq/ft",
    "Fenced In": user.fenced,
    "Adoption Form": user.hasForm
  };

  for (const [key, value] of Object.entries(userInfo)) {
    const label = document.createElement('div');
    label.classList.add('label');
    label.textContent = key + ':';
    labelsContainer.appendChild(label);

    const data = document.createElement('div');
    data.classList.add('value');
    data.textContent = value;
    dataContainer.appendChild(data);
  }
}

function formatDate(dateString) {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
}

const formattedDate = formatDate(appointment.appointmentDate);

async function displayAppointments(userId) {
  const appointmentsBody = document.getElementById("appointmentsBody");
  appointmentsBody.innerHTML = ""; 

  const petProfilesResponse = await fetch(`https://adoptapet-production-1bb7.up.railway.app/api/Pets`);
  const petProfiles = await petProfilesResponse.json();

  const appointmentsResponse = await fetch(`https://adoptapet-production-1bb7.up.railway.app/api/Appointments/ByUser/${userId}?deleted=0`);  
  const appointments = await appointmentsResponse.json();

  const sheltersResponse = await fetch(`https://adoptapet-production-1bb7.up.railway.app/api/Shelters`);
  const shelters = await sheltersResponse.json();

  appointments.forEach(appointment => {
    const petProfile = petProfiles.find(pet => pet.petProfileId === appointment.petProfileId);
    const shelterId = petProfile.shelterId;
    console.log(shelterId); // Cannot read properties of undefined (reading 'shelterId') 
    const shelter = shelters.find(shelter => shelter.shelterId === shelterId);

    const row = document.createElement("tr");
    row.innerHTML = `
      <td>${petProfile ? petProfile.name : 'N/A'}</td>
      <td>${formatDate(appointment.appointmentDate)}</td>
      <td>${shelter ? shelter.shelterUsername : 'N/A'}</td>
      <td><button onclick="deleteAppointment(${appointment.appointmentId})"class="deleteButton" data-appointment-id="${appointment.appointmentId}">Delete</button></td> <!-- Button to delete appointment -->
    `;
    appointmentsBody.appendChild(row);
  });
}

async function deleteAppointment(appointmentId) {
  try {
    const response = await fetch(`https://adoptapet-production-1bb7.up.railway.app/api/Appointments/${appointmentId}`, {
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

function redirectToPetProfile(petId) {
  window.location.href = `./petProfile.html?petId=${petId}`;
}