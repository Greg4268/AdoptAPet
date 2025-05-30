import API_ENDPOINTS from "./apiConfig";


document.addEventListener("DOMContentLoaded", function () {
  var userLoginButton = document.getElementById("loginButton");
  var shelterLoginButton = document.getElementById("loginShelterButton");

  userLoginButton.addEventListener("click", function (event) {
    event.preventDefault();
    loginUser();
  });

  shelterLoginButton.addEventListener("click", function (event) {
    event.preventDefault();
    loginShelter();
  });
});

function loginUser() {
  var userEmail = document.getElementById("userEmail").value;
  var userPassword = document.getElementById("userPassword").value;

  if (!userEmail || !userPassword) {
    alert("Please enter both email and password.");
    return;
  }

  fetch(API_ENDPOINTS.login_user + `?email=${encodeURIComponent(userEmail)}&password=${encodeURIComponent(userPassword)}`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }
  )
    .then((response) => {
      if (!response.ok) throw new Error("Invalid credentials");
      return response.json();
    })
    .then((data) => {
      if (data) {
        const token = JSON.stringify({
          userId: data.userId,
          accountType: data.accountType,
          hasForm: data.hasForm,
          deleted: data.deleted,
        });
        localStorage.setItem("userToken", token);
        window.location.href = "./index.html"; 
      }
      if (data.deleted === true) {
        alert("Your account has been deleted.");
        localStorage.removeItem("userToken");
        window.location.href = "./login.html"; 
      }
    })
    .catch((error) => {
      localStorage.removeItem("userToken");
      console.error("There was an error: ", error);
      alert("Invalid credentials, please try again.");
    });
}

function loginShelter() {
  var email = document.getElementById("userEmail").value;
  var password = document.getElementById("userPassword").value;

  if (!email || !password) {
    alert("Please enter both email and password.");
    return;
  }

  fetch(API_ENDPOINTS.login_shelter + `?email=${encodeURIComponent(email)}&password=${encodeURIComponent(password)}`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }
  )
    .then((response) => {
      if (!response.ok) throw new Error("Invalid credentials");
      return response.json();
    })
    .then((data) => {
      if (data) {
        const token = JSON.stringify({
          shelterId: data.shelterId,
          accountType: "shelter",
          deleted: data.deleted,
          approved: data.approved,
        });
        localStorage.setItem("userToken", token);
        window.location.href = "./shelterDash.html"; 
      }
      if (data.deleted === true) {
        alert("Your account has been deleted.");
      }
    })
    .catch((error) => {
      localStorage.removeItem("shelterToken");
      console.error("There was an error: ", error);
      alert("Invalid credentials, please try again.");
    });
}
