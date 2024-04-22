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

  fetch(
    `http://localhost:5292/api/UserAccounts/by-credentials?email=${encodeURIComponent(
      userEmail
    )}&password=${encodeURIComponent(userPassword)}`,
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
        });
        localStorage.setItem("userToken", token);
        window.location.href = "./index.html"; // Adjust as needed
      }
    })
    .catch((error) => {
      localStorage.removeItem("userToken");
      console.error("There was an error: ", error);
      alert("Invalid credentials, please try again.");
    });
}

function loginShelter() {
  var userEmail = document.getElementById("userEmail").value;
  var userPassword = document.getElementById("userPassword").value;

  if (!userEmail || !userPassword) {
    alert("Please enter both email and password.");
    return;
  }

  fetch(
    `http://localhost:5292/api/Shelters/by-credentials?email=${encodeURIComponent(
      userEmail
    )}&password=${encodeURIComponent(userPassword)}`,
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
          hasForm: data.hasForm,
        });
        localStorage.setItem("shelterToken", token);
        window.location.href = "./shelter-dashboard.html"; // Redirect to a different dashboard for shelters
      }
    })
    .catch((error) => {
      localStorage.removeItem("shelterToken");
      console.error("There was an error: ", error);
      alert("Invalid credentials, please try again.");
    });
}
