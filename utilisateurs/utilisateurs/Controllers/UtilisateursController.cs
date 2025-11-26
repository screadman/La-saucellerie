using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using utilisateurs.Models;
using System.Text.RegularExpressions;
using System.Linq;

namespace utilisateurs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtilisateursController : ControllerBase
    {
        private readonly ILogger<UtilisateursController> _logger;
        private readonly UtilisateursContext _context;

        public UtilisateursController(ILogger<UtilisateursController> logger, UtilisateursContext context)
        {
            _logger = logger;
            _context = context;
        }

        // POST utilisateurs/authentification
        [HttpPost("authentification")]
        public IActionResult AuthentifierUtilisateur([FromBody] AuthentificationRequest request)
        {
            try
            {
                _logger.LogInformation("Tentative d'authentification de l'utilisateur avec l'identifiant : {Identifiant}", request.Courriel);

                var utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.Courriel == request.Courriel);

                if (utilisateur == null)
                {
                    _logger.LogWarning("Aucun utilisateur trouvé avec l'identifiant fourni.");
                    return NotFound("Aucun utilisateur trouvé avec l'identifiant fourni.");  // 404 - Not Found
                }

                _logger.LogInformation("Authentification réussie pour l'utilisateur avec l'identifiant : {Identifiant}", request.Courriel);
                return Ok(utilisateur);  // 200 - OK avec les informations de l'utilisateur
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de l'authentification de l'utilisateur avec l'identifiant : {Identifiant}. Erreur : {Erreur}", request.Courriel, ex.Message);
                return StatusCode(500, "Une erreur est survenue au moment de l'authentification.");  // 500 - Internal Server Error
            }
        }

        // POST utilisateurs
        [HttpPost]
        public IActionResult CreerUtilisateur([FromBody] AuthentificationRequest request)
        {
            try
            {
                var utilisateurExistant = _context.Utilisateurs.SingleOrDefault(u => u.Courriel == request.Courriel);
                if (utilisateurExistant != null)
                {
                    _logger.LogWarning("Un compte associé à ce courriel existe déjà.");
                    return Conflict("Un compte associé à ce courriel existe déjà.");  // 409 - Conflict
                }

                var motDePasseValide = Regex.Match(request.MotPasse, @"^(?=(.*\d))(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$").Success;
                if (!motDePasseValide)
                {
                    _logger.LogWarning("Le mot de passe ne respecte pas les critères de sécurité.");
                    return UnprocessableEntity("Le mot de passe doit être composé d'au moins 8 caractères, de lettres majuscules et minuscules, de chiffres et de caractères spéciaux.");  // 422 - Unprocessable Entity
                }

                var typeUtilisateur = _context.TypeUtilisateurs
                              .Where(t => t.Type == "Client")
                              .Select(t => t.IdType)
                              .FirstOrDefault();

                if (typeUtilisateur == 0)
                {
                    _logger.LogError("Le type d'utilisateur 'Client' n'existe pas.");
                    return StatusCode(500, "Le type d'utilisateur 'Client' n'existe pas.");  // 500 - Internal Server Error
                }

                var nouvelUtilisateur = new Utilisateur
                {
                    Courriel = request.Courriel,
                    MotPasse = request.MotPasse,
                    IdTypeUtilisateur = typeUtilisateur
                };

                _context.Utilisateurs.Add(nouvelUtilisateur);
                _context.SaveChanges();

                _logger.LogInformation("Utilisateur créé avec succès.");
                return Ok(nouvelUtilisateur);  // 200 - OK
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la création de l'utilisateur. Erreur : {Erreur}", ex.Message);
                return StatusCode(500, "Une erreur est survenue au moment de la création de l'utilisateur.");  // 500 - Internal Server Error
            }
        }
    }
}