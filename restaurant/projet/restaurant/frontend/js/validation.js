document.getElementById('twoFactorForm').addEventListener('submit', (e) => {
    e.preventDefault(); // Empêche le rechargement de la page

    // Récupérer les données du formulaire
    const validationData = {
        Code: parseInt(document.getElementById('authCode').value.trim(), 10),
        Expiration: new Date().toISOString(), // Heure de la requête au format ISO
        UserMail: sessionStorage.getItem('email'), // Email stocké lors de l'inscription
        Username: sessionStorage.getItem('username'),
        Mdp: sessionStorage.getItem('mdp')
    };

    try {
        let request = new XMLHttpRequest();
        request.open('POST', 'https://localhost:7090/restaurant/validerEtEnregistrer'); // URL de l'API
        request.setRequestHeader("Content-Type", "application/json");

        // Gestion de la réponse de la requête
        request.onload = function () {
            try {
                const contentType = request.getResponseHeader("Content-Type");
                if (contentType && contentType.includes("application/json")) {
                    const response = JSON.parse(request.responseText);
                    if (request.status >= 200 && request.status < 300) {
                        window.location.href = "index.html";
                    } else {
                        alert(response.message || "Une erreur est survenue.");
                    }
                } else {
                    alert("Réponse du serveur inattendue.");
                }
            } catch (e) {
                console.error('Erreur lors du traitement de la réponse :', e);
                alert("Réponse du serveur invalide.");
            }
        };

        // Gestion des erreurs réseau
        request.onerror = function () {
            console.log("Erreur réseau : la requête n'a pas pu aboutir.");
            alert("Erreur réseau, veuillez vérifier votre connexion.");
        };

        // Envoi de la requête avec les données du formulaire en JSON
        request.send(JSON.stringify(validationData));

    } catch (err) {
        console.error('Erreur :', err);
        alert("Impossible de se connecter au serveur. Veuillez réessayer.");
    }
});