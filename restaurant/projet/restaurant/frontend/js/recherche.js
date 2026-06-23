// Lorsque le formulaire est soumis, l'événement par défaut (rechargement de la page) est annulé. 
// Une fonction asynchrone est déclenchée pour gérer la recherche.
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('search-form').addEventListener('submit', async function (event) {
        event.preventDefault();

        const mealInput = document.getElementById('meal-input');
        const resultsDiv = document.getElementById('search-results');

        // Réinitialiser les résultats
        resultsDiv.innerHTML = '';

        // verifie que l'utilisateur a bien saisi le nom du repas
        const mealName = mealInput.value.trim();
        if (!mealName) {
            alert("Veuillez entrer le nom d'un repas.");
            return;
        }

        try {
            // Envoie une requête GET à une API REST pour chercher des repas correspondant au nom saisi.
            //fetch() permet d'effectuer des requêtes HTTP.
            // fetch() permet d'effectuer des requêtes HTTP.
            const response = await fetch(`https://localhost:7090/Restaurant/search?name=${encodeURIComponent(mealName)}`);

            // Si le serveur retourne un statut 404, un message visuel informe l'utilisateur qu'aucun repas n'a été trouvé.
            if (response.status === 404) {
                resultsDiv.innerHTML = `
                <div style="
                    text-align: center; 
                    margin-top: 20px; 
                    font-family: Arial, sans-serif; 
                    color: red;">
                    <p style="font-size: 1.5em;">
                        <strong>Aucun repas trouvé avec le nom :</strong> "${mealName}"
                    </p>
                    <p style="font-size: 1em; margin-top: 10px;">
                        Veuillez essayer avec un autre nom.
                    </p>
                </div>
            `;
            // Si des repas sont trouvés, ils sont affichés sous forme de cartes stylisées.
            } else if (response.ok) {
                // await response.json() transforme la réponse JSON en objet JavaScript.
                // La méthode .map() génère des cartes HTML pour chaque repas trouvé.
                // cards.join('') assemble toutes les cartes en une chaîne HTML.
                const repasList = await response.json();

                if (!repasList || repasList.length === 0) {
                    resultsDiv.innerHTML = '<p>Aucun repas trouvé.</p>';
                } else {
                    // Créer des cartes pour tous les repas
                    const cards = repasList.map(repas => `
                        <div style="
                            background-color: #f8f9fa; 
                            border: 1px solid #ddd; 
                            border-radius: 10px; 
                            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15); 
                            max-width: 300px; 
                            margin: 10px; 
                            padding: 15px;
                            font-family: Arial, sans-serif; 
                            text-align: center;">
                            
                            <h3 style="font-size: 1.5em; margin-bottom: 10px; color: #333;">${repas.nom}</h3>
                            
                            <p style="font-size: 1em; color: #555;">
                                <strong>Description :</strong> ${repas.description}
                            </p>
                            
                            <p style="font-size: 1em; color: #555;">
                                <strong>Prix :</strong> ${repas.prix} $
                            </p>
                            
                            <p style="font-size: 1em; color: #555;">
                                <strong>Calories :</strong> ${repas.calories} kcal
                            </p>
                            
                            <a href="menu.html#plat${repas.idRepas}" style="
                                display: inline-block; 
                                margin-top: 15px; 
                                padding: 10px 20px; 
                                font-size: 1em; 
                                color: #fff; 
                                background-color: green; 
                                border-radius: 5px; 
                                text-decoration: none;">
                                Voir plus de détails
                            </a>
                        </div>
                    `);

                    resultsDiv.innerHTML = `
                        <div style="
                            display: flex; 
                            flex-wrap: wrap; 
                            justify-content: center; 
                            gap: 20px;">
                            ${cards.join('')}
                        </div>
                    `;
                }
            } else {
                alert("Erreur lors de la recherche. Veuillez réessayer plus tard.");
            }
            // Le bloc try-catch capture les erreurs potentielles liées à l'appel de l'API.
        } catch (error) {
            console.error("Erreur réseau :", error);
            alert("Une erreur réseau est survenue. Veuillez vérifier votre connexion.");
        }
    });
});