document.addEventListener("DOMContentLoaded", function () {
  var loginForm = document.getElementById("loginForm");
  if (loginForm) {
    loginForm.addEventListener("submit", function (event) {
      event.preventDefault(); // Prevent the form from submitting the default way

      var userEmail = document.getElementById("userEmail").value;
      var userPassword = document.getElementById("userPassword").value;

      // Log for debugging purposes
      console.log(userEmail);
      console.log(userPassword);

      // Validate input fields if necessary
      if (!userEmail || !userPassword) {
        alert("Please enter both email and password.");
        return;
      }

      

      // Construct the query string
      var queryString = `${userPassword}?email=${userEmail}`;

      alert(queryString)

      // Send login data to the C# controller
      fetch(`http://localhost:5292/api/UserAccounts/${queryString}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        }
      })
      .then((response) => response.json())
      .then((data) => {
        alert(JSON.stringify(data));
        if (data) {
          localStorage.setItem("userToken", data.token); // Save the token if login is successful
          window.location.href = "./index.html"; // Redirect to the homepage
        } else {
          alert("Invalid credentials, please try again.");
          localStorage.removeItem("userToken"); // Remove the token if login is not successful, so dashboard option is not available 
        }
      })
      .catch((error) => {
        console.error("Invalid credentials, please try again.");
        alert("Error logging in, please try again later.");
      });
    });
  }
});
