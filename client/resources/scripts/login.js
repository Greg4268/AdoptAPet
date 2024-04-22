document.addEventListener("DOMContentLoaded", function () {
  var loginForm = document.getElementById("loginForm");
  if (loginForm) {
    loginForm.addEventListener("submit", function (event) {
      event.preventDefault(); // Prevent the form from submitting the default way

      var userEmail = document.getElementById("userEmail").value;
      var userPassword = document.getElementById("userPassword").value;

      console.log(userEmail);
      console.log(userPassword);

      if (!userEmail || !userPassword) {
        alert("Please enter both email and password.");
        return;
      }

      var queryString = `email=${encodeURIComponent(
        userEmail
      )}&password=${encodeURIComponent(userPassword)}`;

      fetch(
        `http://localhost:5292/api/UserAccounts/by-credentials?email=${userEmail}&password=${userPassword}`,
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
            window.location.href = "./index.html";
          }
        })
        .catch((error) => {
          localStorage.removeItem("userToken");
          console.error("There was an error: ", error);
          alert("Invalid credentials, please try again.");
        });
    });
  }
});
