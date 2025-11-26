using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recettes.Models;
using System.Collections.Generic;
using System.Linq;

namespace recettes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecettesController : ControllerBase
    {
        private readonly ILogger<RecettesController> _logger;
        private readonly RecettesContext _context;

        public RecettesController(ILogger<RecettesController> logger, RecettesContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("par_nom")]
        public ActionResult<IEnumerable<Recette>> GetRecettesParNom([FromQuery] string? nom)
        {
            try
            {
                // Création de la requête de base
                IQueryable<Recette> query = _context.Recettes;

                // Filtrer par nom, si un nom est fourni
                if (!string.IsNullOrEmpty(nom))
                {
                    query = query.Where(r => EF.Functions.Like(r.Nom.ToLower(), $"%{nom.ToLower()}%"));
                }

                // Trier par ordre alphabétique
                var result = query.OrderBy(r => r.Nom).ToList();

                // Si aucune recette n'est trouvée
                if (!result.Any())
                {
                    _logger.LogWarning("Aucune recette trouvée.");
                    return NoContent();  // 204 - No Content
                }

                _logger.LogInformation($"{result.Count} recettes trouvées.");
                return Ok(result);  // 200 - OK avec les recettes
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur lors de l'obtention des données: {ex}");
                return StatusCode(500, "Une erreur est survenue au moment de l'obtention des données.");  // 500 - Internal Server Error
            }
        }

        // GET: /recettes/{id}
        [HttpGet("{id}")]
        public ActionResult<Recette> GetRecetteById(int id)
        {
            try
            {
                _logger.LogInformation($"Accès à GetRecetteById avec l'ID : {id}");

                // Récupérer la recette par ID sans inclure les ingrédients ni les étapes
                var recette = (from r in _context.Recettes
                               where r.IdRecette == id
                               select new Recette
                               {
                                   IdRecette = r.IdRecette,
                                   Nom = r.Nom,
                                   Description = r.Description,
                                   TempsPreparation = r.TempsPreparation,
                                   TempsCuisson = r.TempsCuisson,
                                   NbrePortions = r.NbrePortions,
                                   Calorie = r.Calorie,
                                   Proteine = r.Proteine,
                                   MatiereGrasse = r.MatiereGrasse,
                                   Glucide = r.Glucide,
                                   Fibre = r.Fibre,
                                   Fer = r.Fer,
                                   Calcium = r.Calcium,
                                   Sodium = r.Sodium,
                                   // Inclure les ingrédients associés
                                   Ingredients = r.Ingredients.Select(i => new Ingredient
                                   {
                                       IdIngredient = i.IdIngredient,
                                       Ingredient1 = i.Ingredient1,
                                       Quantite = i.Quantite
                                   }).ToList(),
                                   // Inclure les étapes associées, triées par numéro d'étape
                                   Etapes = r.Etapes.OrderBy(e => e.NoEtape).Select(e => new Etape
                                   {
                                       IdEtape = e.IdEtape,
                                       IdRecette = e.IdRecette,
                                       NoEtape = e.NoEtape,
                                       Description = e.Description
                                   }).ToList()
                               }).FirstOrDefault();


                // Si aucune recette n'est trouvée
                if (recette == null)
                {
                    _logger.LogWarning("Aucune recette trouvée.");
                    return NoContent();  // 204 - No Content
                }

                return Ok(recette);  // 200 - OK avec la recette trouvée
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur lors de l'obtention de la recette avec l'ID : {id}, Erreur: {ex.Message}");
                return StatusCode(500, "Une erreur est survenue au moment de l'obtention des données.");  // 500 - Internal Server Error
            }
        }

        // DELETE: /recettes/{id}
        [HttpDelete("{id:int}")]
        public IActionResult DeleteRecette(int id)
        {
            try
            {
                _logger.LogInformation($"Tentative de suppression de la recette avec l'ID : {id}");

                // Récupérer la recette par ID
                var recette = _context.Recettes.Find(id);

                // Si aucune recette n'est trouvée
                if (recette == null)
                {
                    _logger.LogWarning($"Recette avec l'ID : {id} non trouvée.");
                    return NotFound("Aucune recette trouvée. Impossible d'effectuer la suppression.");  // 404 - Not Found
                }

                // Supprimer les étapes associées à la recette
                _context.Etapes.RemoveRange(recette.Etapes);

                // Supprimer les ingrédients associés à la recette
                _context.Ingredients.RemoveRange(recette.Ingredients);

                // Supprimer la recette elle-même
                _context.Recettes.Remove(recette);

                // Enregistrer les modifications
                _context.SaveChanges();

                _logger.LogInformation($"Recette avec l'ID : {id} supprimée avec succès.");
                return Ok($"Recette avec l'ID : {id} supprimée avec succès.");  // 200 - OK
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur lors de la suppression de la recette avec l'ID : {id}, Erreur: {ex.Message}");
                return StatusCode(500, "Une erreur est survenue au moment de la suppression des données.");  // 500 - Internal Server Error
            }
        }
    }
}