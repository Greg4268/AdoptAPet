document.addEventListener("DOMContentLoaded", function () {
  var loginButton = document.getElementById("loginButton");

  // Listen for login form submission instead of button click for better practice
  var loginForm = document.getElementById("loginForm");
  if (loginForm) {
    loginForm.addEventListener("submit", function (event) {
      event.preventDefault(); // Prevent the form from submitting the default way

      var userEmail = document.getElementById("userEmail").value;
      var userPassword = document.getElementById("userPassword").value;

      console.log(userEmail);
      console.log(userPassword);

      // Validate input fields if necessary
      if (!userEmail || !userPassword) {
        alert("Please enter both email and password.");
        return;
      }

      // Send login data to the C# controller
      fetch("http://localhost:5292/api/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: userEmail,
          password: userPassword,
        }),
      })
        .then((response) => response.json())
        .then((data) => {
          if (data.isValid) {
            localStorage.setItem("userToken", data.token); // Save the token if login is successful
            window.location.href = "./index.html"; // Redirect to the homepage
          } else {
            alert("Invalid credentials, please try again.");
            localStorage.removeItem("userToken"); // Remove the token if login is not successful, so dashboard option is not available 
          }
        })
        .catch((error) => {
          console.error("Error during login:", error);
          alert("Error logging in, please try again later.");
        });
    });
  }
});