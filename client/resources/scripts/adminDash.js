const shelterURL = "http://localhost:5292/api/Shelters";
const usersURL = "http://localhost:5292/api/UserAccounts";

$(document).ready(function () {
  $("#table1").DataTable();
});

$(document).ready(function () {
  $("#table2").DataTable();
});


function handleOnLoad() {
  fetchShelters(shelterURL);
  fetchUsers(usersURL);
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

async function displayShelters(shelters) {
  const shelterContainer = document.querySelector(".shelter-container");
  shelterContainer.innerHTML = ""; // Clear existing content

  for (const shelter of shelters) {
    const sheltersCard = `
        <tr>
            <td>${shelter.shelterUsername}</td>
            <td>[email add holder]</td>
            <td>${shelter.shelterID}</td>
            <td>[pet count holder]</td>
            <td>${shelter.address}</td>
            <td>[approval status holder]</td>
          </tr>
      `;
    shelterContainer.innerHTML += sheltersCard; // Append the new card
  }
}

async function displayUsers(users) {
  const usersContainer = document.querySelector(".users-container");
  usersContainer.innerHTML = ""; // Clear existing content

  for (const user of users) {
    //const imageUrl = await fetchImage(); // Fetch image for each pet
    const usersCard = `
          <tr>
              <td>${user.userId}</td>
              <td>${user.firstName + " " + user.lastName}</td>
              <td>${user.email}</td>
              <td>[placeholder]</td>
              <td>${user.accountType}</td>
              <td>[Action buttons column?]</td>
            </tr>
      `;
    usersContainer.innerHTML += usersCard; // Append the new card
  }
}
