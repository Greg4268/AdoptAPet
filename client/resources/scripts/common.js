document.addEventListener("DOMContentLoaded", function () {
  const navbarContainer = document.getElementById("navbar-container");
  if (navbarContainer) {
    fetch("./navbar.html")
      .then((response) => response.text())
      .then((html) => {
        navbarContainer.innerHTML = html;
      })
      .catch((error) => console.error("Error loading the navbar:", error));
  }
});

function getParsedUserToken() {
  const tokenString = localStorage.getItem("userToken");
  return tokenString ? JSON.parse(tokenString) : null;
}

document.addEventListener("DOMContentLoaded", function () {
  loginValDropDown(); // This will adjust the display settings of dashboard and logout links correctly.
});

function toggleDropdownMenu() {
  const dropdownMenu = document.querySelector(".dropdown-menu");
  if (!dropdownMenu) return;
  loginValDropDown(); // Ensure visibility states are correct based on login before toggling
  dropdownMenu.style.display =
    dropdownMenu.style.display === "block" ? "none" : "block";
}

// function loginValDropDown() {
//   const dashboardLink = document.getElementById("dashboardLink");
//   const logoutLink = document.getElementById("logoutLink");

//   const userToken = getParsedUserToken();
//   if (userToken) {
//     dashboardLink.style.display = "block";
//     logoutLink.style.display = "block";
//   } else {
//     dashboardLink.style.display = "none";
//     logoutLink.style.display = "none";
//     const dropdownMenu = document.querySelector(".dropdown-menu");
//     if (dropdownMenu) dropdownMenu.style.display = "none"; // Hide dropdown if not logged in
//   }
// }

function loginValDropDown() {
  const dashboardLink = document.getElementById("dashboardLink");
  const logoutLink = document.getElementById("logoutLink");

  // Ensure getParsedUserToken() is called and the result is stored in userToken
  const userToken = getParsedUserToken(); // This should fetch and parse the user token

  // Ensure userToken is defined before using it
  if (userToken) {
    if (dashboardLink && logoutLink) {
      // Also check if the elements exist
      dashboardLink.style.display = "block";
      logoutLink.style.display = "block";
    }
  } else {
    if (dashboardLink && logoutLink) {
      // Check elements exist before accessing
      dashboardLink.style.display = "none";
      logoutLink.style.display = "none";
    }
    const dropdownMenu = document.querySelector(".dropdown-menu");
    if (dropdownMenu) dropdownMenu.style.display = "none"; // Hide dropdown if not logged in
  }
}

function loginValidationForm() {
  const userData = getParsedUserToken();

  if (
    userData &&
    userData.accountType == "adopter" &&
    userData.hasForm == false
  ) {
    window.location.href = "./adoptForm.html";
  } else if (!userData) {
    alert("Please login first.");
    window.location.href = "./login.html";
  } else if (userData.hasForm == true) {
    alert("You already completed the adoption form!");
  } else if (
    userData.accountType == "shelter" ||
    userData.accountType == "admin"
  ) {
    alert("Adoption forms are only available for adopter profiles.");
  } else {
    alert("There was an error, please try again later.");
  }
}

function loginValidationLogin() {
  const userData = getParsedUserToken();

  if (userData) {
    alert("You are already logged in!");
  } else {
    window.location.href = "./login.html";
  }
}

function loginValidationSignUp() {
  const userData = getParsedUserToken();

  if (userData) {
    alert("You are already logged in!");
  } else {
    window.location.href = "./signup.html";
  }
}

function logout() {
  localStorage.removeItem("userToken"); // Remove token from local storage
  loginValDropDown(); // Update link visibility
  alert("You have been logged out!"); // Inform user
  window.location.href = "./index.html"; // Redirect to login page
}

function switchDash() {
  const userData = getParsedUserToken();

  if (!userData) {
    console.error("No user token found, redirecting to login.");
    window.location.href = "./login.html";
    return;
  }

  switch (userData.accountType || shelterToken.accountType) {
    case "admin":
      window.location.href = "./adminDash.html";
      break;
    case "adopter":
      window.location.href = "./adopterDash.html";
      break;
    case "shelter":
      window.location.href = "./shelterDash.html";
      break;
    default:
      console.error("Unauthorized or undefined account type.");
      window.location.href = "./login.html";
  }
}
