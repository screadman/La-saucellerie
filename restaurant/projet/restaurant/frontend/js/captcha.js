// Fonction pour générer un calcul mathématique aléatoire
function generateMathCaptcha() {
    const num1 = Math.floor(Math.random() * 10) + 1; // Un nombre entre 1 et 10
    const num2 = Math.floor(Math.random() * 10) + 1; // Un autre nombre entre 1 et 10
    const operator = Math.random() > 0.5 ? '+' : '*'; // Choisir entre addition et multiplication
    const question = `${num1} ${operator} ${num2} = ?`; // Affichage de l'équation avec un "?"
    const answer = operator === '+' ? num1 + num2 : num1 * num2;

    // Stocker la réponse dans un champ caché
    document.getElementById('signupForm').dataset.captchaAnswer = answer;

    // Dessiner le calcul sur le canvas avec des effets
    const canvas = document.getElementById('captchaCanvas');
    const ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height); // Effacer le canvas

    // Ajouter un bruit de fond
    for (let i = 0; i < 100; i++) {
        const x = Math.random() * canvas.width;
        const y = Math.random() * canvas.height;
        ctx.fillStyle = 'rgba(0, 0, 0, 0.1)';
        ctx.fillRect(x, y, 1, 1);
    }

    // Ajouter des lignes courbes aléatoires pour complexifier le fond
    for (let i = 0; i < 3; i++) {
        ctx.strokeStyle = 'rgba(0, 0, 0, 0.2)';
        ctx.lineWidth = 1;
        ctx.beginPath();
        ctx.moveTo(Math.random() * canvas.width, Math.random() * canvas.height);
        ctx.lineTo(Math.random() * canvas.width, Math.random() * canvas.height);
        ctx.stroke();
    }

    // Définir les styles de texte
    ctx.font = '24px Arial';
    ctx.textAlign = 'center';
    ctx.textBaseline = 'middle';

    // Ajouter un effet de distorsion au texte
    ctx.save();
    ctx.setTransform(1, 0, 0.1, 1, Math.random() * 10, Math.random() * 5);
    ctx.fillStyle = 'rgba(0, 0, 255, 0.8)';
    ctx.fillText(question, canvas.width / 2, canvas.height / 2);
    ctx.restore();
}

// Vérifier la réponse au CAPTCHA avant la soumission du formulaire
document.getElementById('signupForm').addEventListener('submit', function(event) {
    const userAnswer = parseInt(document.getElementById('captchaAnswer').value);
    const correctAnswer = parseInt(this.dataset.captchaAnswer);

    if (isNaN(userAnswer) || userAnswer !== correctAnswer) {
        event.preventDefault(); // Annuler la soumission
        alert("La réponse au CAPTCHA est incorrecte. Essayez à nouveau.");
        generateMathCaptcha(); // Générer un nouveau CAPTCHA
        return; // Arrêter l'exécution ici
    }
});

// Initialiser le CAPTCHA au chargement de la page
window.onload = generateMathCaptcha;