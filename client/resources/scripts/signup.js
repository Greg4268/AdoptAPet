
// form submission for sign up page 
function submitForm() {
    alert('form submitting process begun');
    const formData = {
        firstName: document.getElementById('firstName').value,
        lastName: document.getElementById('lastName').value,
        email: document.getElementById('email').value,
        password: document.getElementById('password').value,
        accountType: document.getElementById('accountType').value
    };
  
    fetch('https://your-backend-url/api/signup', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData)
    })
    .then(response => response.json())
    .then(data => {
        alert('Signup successful!');
    })
    .catch((error) => {
        console.error('Error:', error);
        alert('Error signing up.');
    });
  }