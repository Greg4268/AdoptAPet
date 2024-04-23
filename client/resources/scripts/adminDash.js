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
    // console.log("Shelters Objects: ", shelters);
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
    // console.log("users Objects: ", users);
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
            <td>${shelter.email}</td>
            <td>${shelter.shelterId}</td>
            <td>${shelter.hoursOfOperation}</td>
            <td>${shelter.address}</td>
            <td>${shelter.approved}</td>
            <td><button onclick="AltShelterApproval(${shelter.shelterId}, ${shelter.approved})">Approve/Revoke Approval</button></td>
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
              <td>${user.hasForm}</td>
              <td>${user.accountType}</td>
              <td>${user.deleted}</td>
              <td><button onclick="softDeleteUser(${user.userId},${
      user.deleted
    })">Revoke Access</td>
            </tr>
      `;
    usersContainer.innerHTML += usersCard; // Append the new card
  }
}

function AltShelterApproval(shelterId, approved) {
  console.log("Modifying shelter approval of shelter ID: ", shelterId);
  let approved = !approved;

  fetch(`http://localhost:5292/api/Shelter/${userId}/toggle-delete`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      UserId: userId,
      deleted: approved,
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


// soft delete user is not successfully updating the information on the table post changing deleted status 
function softDeleteUser(userId, currentDeletedStatus) {
  console.log("Toggling deletion status for user ID:", userId);

  // Toggle the current deleted status
  let newDeletedStatus = !currentDeletedStatus;

  console.log("Current status:", currentDeletedStatus);
  console.log("New status after toggle:", newDeletedStatus);

  fetch(`http://localhost:5292/api/UserAccounts/${userId}/toggle-delete`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      UserId: userId,
      deleted: newDeletedStatus,
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

