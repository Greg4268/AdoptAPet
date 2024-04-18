
document.addEventListener('DOMContentLoaded', function() {
    const urlParams = new URLSearchParams(window.location.search);
    const petId = urlParams.get('petId');
    if (petId) {
        fetchPetDetails(petId);
    } else {
        alert("Pet not found!");
        window.location.href = './index.html'; // Optionally redirect if no pet ID is found
    }
});

async function fetchPetDetails(petId) {
    try {
        const response = await fetch(`http://localhost:5292/api/Pets/${petId}`);
        if (!response.ok) {
            throw new Error('Failed to fetch pet details');
        }
        const pet = await response.json();
        document.getElementById('petName').textContent = pet.name;
        document.getElementById('petImage').src = pet.imageUrl;
        document.getElementById('petImage').alt = `Image of ${pet.name}`;
        // Populate other elements as needed
    } catch (error) {
        console.error('Error loading pet details:', error);
        alert('Error loading pet details.');
    }
}
