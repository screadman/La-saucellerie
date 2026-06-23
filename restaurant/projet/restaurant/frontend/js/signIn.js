document.getElementById('signinForm').addEventListener('submit', (e) => {
    e.preventDefault();

    // Récupérer les données du formulaire
    let credentials = {
        username: document.getElementById('username').value,
        Mdp: document.getElementById('password').value
    };

    try {
        let request = new XMLHttpRequest();
        request.open('POST', 'https://localhost:7090/restaurant/authentification'); 
        request.setRequestHeader("Content-Type", "application/json");

        // Gestion de la réponse de la requête
        request.onload = function() {
            try {
                if (request.status >= 200 && request.status < 300) {
                    // Rediriger vers la page d'accueil après la connexion réussie
                    window.location.href = "index.html";
                } else {
                    const error = JSON.parse(request.responseText);
                    alert(error.message || "Erreur lors de la connexion.");
                }
            } catch (e) {
                console.error('Erreur lors du traitement de la réponse:', e);
                alert("Réponse du serveur invalide.");
            }
        };

        // Gestion des erreurs réseau
        request.onerror = function() {
            console.log("Erreur du client : La requête n'a pas pu aboutir.");
            alert("Erreur réseau, veuillez vérifier votre connexion.");
        };

        // Envoi de la requête avec les données du formulaire
        request.send(JSON.stringify(credentials));
        
    } catch (err) {
        console.error('Erreur :', err);
        alert("Impossible de se connecter au serveur. Veuillez réessayer.");
    }
});

document.getElementById('togglePassword').addEventListener('click', function() {
    const passwordField = document.getElementById('password');
    const icon = document.getElementById('togglePassword1');

    if (passwordField.type === 'password') {
        passwordField.type = 'text'; // Affiche le mot de passe
        icon.classList.remove('bi-eye-slash');
        icon.classList.add('bi-eye'); // Change l'icône en "œil"
    } else {
        passwordField.type = 'password'; // Cache le mot de passe
        icon.classList.remove('bi-eye');
        icon.classList.add('bi-eye-slash'); // Change l'icône en "œil barré"
    }
});
