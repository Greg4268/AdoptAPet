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

      // alert(queryString) // check string content before fetch

      // Send login data to the C# controller
      fetch(`http://localhost:5292/api/UserAccounts/${queryString}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      })
        .then((response) => response.json())
        .then((data) => {
          //alert(JSON.stringify(data));
          //alert(JSON.stringify(data.userId));
          if (data) {
            const token = JSON.stringify({
              userId: data.userId,
              accountType: data.accountType,
            });
            localStorage.setItem("userToken", token);
            window.location.href = './index.html' // Assuming you have a function to handle redirection
          }
        })
        .catch((error) => {
          localStorage.removeItem("userToken");
          console.error("There was an error leading to the catch: ", error);
          alert("Invalid credential, please try again.");
        });
    });
  }
});
