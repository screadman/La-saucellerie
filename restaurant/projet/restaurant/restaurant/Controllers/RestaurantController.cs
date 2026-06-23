using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurant.Models;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace restaurant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {

        private readonly ILogger<RestaurantController> _logger;
        private readonly RestaurantContext _context;
        private readonly IEmailService _emailService;


        public RestaurantController(ILogger<RestaurantController> logger, RestaurantContext context, IEmailService emailService)
        {
            _logger = logger;
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult<Client> GetClients() {
            try
            {
                _logger.LogInformation("Accès au endpoint GET: api/clients");
               var result = _context.Clients.FromSqlRaw("select * from client").ToList();

                if (!result.Any()) {
                    return NoContent();
                
                }
                _logger.LogInformation($"{result.Count} comptes trouvées.");

                return Ok(result);
            }
            catch (Exception ex) {
                _logger.LogError($"Ereur lors de l'obtention des données: {ex}");
                return StatusCode(500, "Une erreur est survenue au moment de l'obtention des données.");


            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRepas(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("Le nom du repas est requis.");

                _logger.LogInformation($"Recherche pour le repas: {name}");

                var repas = await _context.Repas
                    .Where(r => EF.Functions.ILike(r.Nom.Trim().ToLower(), $"%{name.Trim().ToLower()}%"))
                    .ToListAsync(); // Retourner une liste, pas un seul élément

                if (repas == null || !repas.Any())
                {
                    _logger.LogWarning($"Aucun repas trouvé avec le nom : {name}");
                    return NotFound($"Aucun repas trouvé avec le nom : {name}");
                }

                _logger.LogInformation($"Repas trouvés: {string.Join(", ", repas.Select(r => r.Nom))}");
                return Ok(repas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la recherche de repas.");
                return StatusCode(500, "Erreur serveur.");
            }
        }




        [HttpPost("authentification")]
        public IActionResult AuthentifierClient([FromBody] AuthentificationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Mdp))
            {
                return BadRequest("Nom d'utilisateur et mot de passe sont requis."); // 400 - Bad Request
            }

            try
            {
                _logger.LogInformation("Tentative d'authentification de l'utilisateur avec l'identifiant : {Username}", request.Username);

                // Rechercher le client par nom d'utilisateur
                var client = _context.Clients.FirstOrDefault(u => u.Username == request.Username);

                if (client == null)
                {
                    _logger.LogWarning("Aucun utilisateur trouvé avec l'identifiant : {Username}", request.Username);
                    return NotFound("Nom d'utilisateur ou mot de passe incorrect."); 
                }
                bool isMatch = Validation.VerifyPassword(request.Mdp, client.Mdp);
                // Vérifier le mot de passe avec une comparaison sécurisée
                if (isMatch == false)
                {
                    _logger.LogWarning("Mot de passe incorrect pour l'utilisateur : {Username}", request.Username);
                    return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
                }

         
                _logger.LogInformation("Authentification réussie pour l'utilisateur : {Username}", request.Username);
                return Ok(new { Message = "Authentification réussie" }); // 200 - OK
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'authentification de l'utilisateur : {Username}", request.Username);
                return StatusCode(500, "Une erreur est survenue au moment de l'authentification."); // 500 - Internal Server Error
            }
        }


        [HttpPost("nouveauCompte")]
        public async Task<IActionResult> CreerClientAsync([FromBody] AuthentificationRequest request)
        {
            try
            {
                // Vérifier que tous les champs nécessaires sont remplis
                if (string.IsNullOrWhiteSpace(request.EMail) ||
                    string.IsNullOrWhiteSpace(request.Mdp) ||
                    string.IsNullOrWhiteSpace(request.Username))
                {
                    _logger.LogWarning("Les champs obligatoires sont manquants.");
                    return BadRequest("Tous les champs (email, mot de passe, nom d'utilisateur) sont requis."); // 400 - Bad Request
                }
                  var existingClient = _context.Clients.FirstOrDefault(u => u.Username == request.Username);

                if (existingClient != null)
                {
                    _logger.LogWarning("Le nom d'utilisateur {Username} est déjà pris.", request.Username);
                    return Conflict("Le nom d'utilisateur est déjà pris."); // 409 - Conflict
                }

                // Vérifier si l'email est déjà utilisé
                var utilisateurExistant = _context.Clients.SingleOrDefault(u => u.EMail == request.EMail);
                if (utilisateurExistant != null)
                {
                    _logger.LogWarning("Un compte associé à cet email existe déjà : {Email}", request.EMail);
                    return Conflict("Un compte associé à cet email existe déjà."); // 409 - Conflict
                }

                // Valider le mot de passe
                var motDePasseValide = Regex.Match(
                    request.Mdp,
                    @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$"
                ).Success;

                if (!motDePasseValide)
                {
                    _logger.LogWarning("Le mot de passe fourni ne respecte pas les critères de sécurité.");
                    return UnprocessableEntity(
                        "Le mot de passe doit contenir au moins 8 caractères, avec des lettres majuscules, minuscules, des chiffres, et des caractères spéciaux."
                    ); // 422 - Unprocessable Entity
                }

                var validationCode = new ValidationCode { 
                    Code =  int.Parse(GenerateRandomCode.Generate(6)), 
                    Expiration = DateTime.UtcNow.AddMinutes(10), 
                    UserMail = request.EMail };

                _context.ValidationCodes.Add(validationCode); 
                await _context.SaveChangesAsync();
                await _emailService.SendValidationEmail(request.EMail, validationCode.Code); 
                return Ok(new { 
                    message = " A validation code has been sent to your email." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création de l'utilisateur.");
                return StatusCode(500, $"Une erreur est survenue lors de la création de l'utilisateur.{ex.InnerException?.Message ?? ex.Message}"); // 500 - Internal Server Error
            }
        }



        [HttpPost("validerEtEnregistrer")]
        public async Task<IActionResult> ValiderEtEnregistrerUtilisateur([FromBody] intermediaiaire request)
        {
            try
            {
                _logger.LogWarning("Données reçues : {UserMail}, {Code}, {Expiration}, {Username}", request.UserMail, request.Code, request.Expiration, request.Username);

                if (request.Code <= 0 || request.Expiration == DateTime.MinValue || string.IsNullOrEmpty(request.UserMail))
                {
                    return BadRequest("L'email, le code et la date d'expiration sont requis."); // 400 - Bad Request
                }

                // Rechercher le code de validation dans la base de données pour l'email donné
                var validationCode = await _context.ValidationCodes
                    .Where(v => v.UserMail == request.UserMail && v.Code == request.Code)
                    .FirstOrDefaultAsync();

                if (validationCode == null)
                {
                    _logger.LogWarning("Aucun code de validation trouvé pour l'email : {Email} et le code : {Code}", request.UserMail, request.Code);
                    return NotFound("mauvais code de validation"); // 404 - Not Found
                }

                // Vérifier si le code a expiré
                if (validationCode.Expiration < DateTime.UtcNow)
                {
                    _logger.LogWarning("Le code de validation a expiré pour l'email : {Email}", request.UserMail);
                    return BadRequest("Le code de validation a expiré."); // 400 - Bad Request
                }
               
                if (string.IsNullOrEmpty(request.Mdp))
                {
                    return BadRequest("mauvais");
                }
                if (string.IsNullOrEmpty(request.Username))
                {
                    return BadRequest("mauvais");
                }
                string hashedPassword = HashPassword.HashPass(request.Mdp);
                var nouvelUtilisateur = new Client
                {
                    Username = request.Username,
                    EMail = request.UserMail,
                    Mdp = hashedPassword 
                };

                _context.Clients.Add(nouvelUtilisateur);
                _context.SaveChanges();
                _logger.LogInformation("Utilisateur créé avec succès : {Email}", request.UserMail);

                await _context.SaveChangesAsync();

                //Supprimer le code de validation après son utilisation(facultatif)
                _context.ValidationCodes.Remove(validationCode);
                await _context.SaveChangesAsync();

                _logger.LogInformation("L'utilisateur a été activé avec succès pour l'email : {Email}", request.UserMail);
                return Ok(new {message = "L'utilisateur a été activé avec succès." }); // 200 - OK
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la validation et de l'enregistrement de l'utilisateur : {Email}", request.UserMail);
                return StatusCode(500, $"Une erreur est survenue lors de la création de l'utilisateur.{ex.InnerException?.Message ?? ex.Message}"); // 500 - Internal Server Error
            }
        }

    }
}
