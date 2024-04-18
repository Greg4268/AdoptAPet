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

function handleOnLoadCommon() {
  loginValDropDown();
}

function loginValidationForm() {
  const userData = getParsedUserToken();

  if (userData && userData.accountType == "adopter") {
    window.location.href = "./adoptForm.html";
  } else if (!userData) {
    alert("Please login first.");
    window.location.href = "./login.html";
  } else {
    alert("Adopter profiles only have access to this page.");
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

document.addEventListener("DOMContentLoaded", function () {
  loginValDropDown(); // Check token and adjust UI on page load
});

function loginValDropDown() {
  const userData = getParsedUserToken();

  if (userData) {
    document.getElementById("dashboardLink").style.display = "block";
    document.getElementById("logoutLink").style.display = "block";
  } else {
    document.getElementById("dashboardLink").style.display = "none";
    document.getElementById("logoutLink").style.display = "none";
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

  switch (userData.accountType) {
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
