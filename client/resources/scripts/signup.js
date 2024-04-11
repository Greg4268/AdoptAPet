document.addEventListener("DOMContentLoaded", function () {
  var signupForm = document.getElementById("signupForm");
  if (signupForm) {
      signupForm.addEventListener("submit", function (event) {
          event.preventDefault(); // Prevent the default form submission

          var formData = {
              firstName: document.getElementById("firstName").value,
              lastName: document.getElementById("lastName").value,
              email: document.getElementById("userEmail").value,
              password: document.getElementById("userPassword").value,
              accountType: document.getElementById("accountType").value
          };

          fetch('http://localhost:5292/api/signup', {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json',
              },
              body: JSON.stringify(formData)
          })
          .then(response => response.json())
          .then(data => {
              if (data.success) {
                  alert('Registration successful!');
                  window.location.href = './index.html'; // Redirect after successful registration
              } else {
                  alert('Registration failed: ' + data.message);
              }
          })
          .catch(error => {
              console.error('Error during registration:', error);
              alert('Error during registration. Please try again later.');
          });
      });
  }
});
