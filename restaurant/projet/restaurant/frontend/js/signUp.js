// Cette page de script gère le formulaire d'inscription d'un utilisateur. 
document.getElementById('signupForm').addEventListener('submit', (e) => {
    e.preventDefault(); // permet de gérer manuellement l'envoi des données
    // let captcha = grecaptcha.getResponse();

    // Récupérer les données du formulaire et envoie au serveur via une requête HTTP
    let newClient = {
        username: document.getElementById('username').value.trim(),
        EMail: document.getElementById('email').value.trim(),
        mdp: document.getElementById('password').value.trim()
    };
    // informations utilisateur sont temporairement stockées dans la session storage du navigateur pour une utilisation ultérieure.
    // Elle est utile dans ce cas pour conserver les données tant que l'onglet reste ouvert.
    sessionStorage.setItem("username", document.getElementById('username').value.trim());
    sessionStorage.setItem("email", document.getElementById('email').value.trim());
    sessionStorage.setItem("mdp", document.getElementById('password').value.trim());

    // Une requête HTTP POST est envoyée à une API REST via XMLHttpRequest pour créer un nouveau compte utilisateur.
    // les donnees sont converties en format JSON avant d'etre envoyer
    try {
        let request = new XMLHttpRequest();
        request.open('POST', 'https://localhost:7090/restaurant/nouveauCompte');
        request.setRequestHeader("Content-Type", "application/json");

        // Gestion de la réponse de la requête
        request.onload = function() {
            try {
                if (request.status >= 200 && request.status < 300) {
                    window.location.href = "validation.html";
                } else {
                    // Si la requête échoue, afficher le message d'erreur
                    const error = JSON.parse(request.responseText);
                    alert(error.message || "Une erreur est survenue lors de l'inscription.");
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
            // Une alerte est déclenchée en cas de problème réseau, comme une coupure d'internet ou une erreur de connexion.
        };
        
        // Envoi de la requête avec les données du formulaire en JSON
        request.send(JSON.stringify(newClient));
        
    } catch (err) {
        console.error('Erreur :', err);
        alert("Impossible de se connecter au serveur. Veuillez réessayer.");
    }
});


document.getElementById('togglePassword1').addEventListener('click', function() {
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

document.getElementById('togglePassword2').addEventListener('click', function() {
    const passwordField = document.getElementById('confirmPassword');
    const icon = document.getElementById('togglePassword2');

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