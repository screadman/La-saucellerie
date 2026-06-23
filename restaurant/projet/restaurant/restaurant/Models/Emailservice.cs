using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public interface IEmailService
{
    Task SendValidationEmail(string toEmail, int code);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendValidationEmail(string toEmail, int code)
    {
        var client = new MailjetClient(
            _configuration["Mailjet:ApiKey"],
            _configuration["Mailjet:ApiSecret"]
        );

        // Créer un objet de demande pour envoyer un email
        var request = new MailjetRequest
        {
            Resource = Send.Resource
        };

        // Utiliser les propriétés pour définir les paramètres
        request.Property("FromEmail", _configuration["Mailjet:SenderEmail"]);
        request.Property("FromName", "restaurant La Saucelerrie");
        request.Property("Subject", "Votre code de validation");
        request.Property("Text-part", $"Votre code de validation est : {code}");
        request.Property("Html-part", $"<h3>Votre code de validation est : {code}</h3>");
        request.Property("To", toEmail); // Passer correctement l'adresse email

        try
        {
            // Envoi de l'email
            var response = await client.PostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Email envoyé avec succès
                Console.WriteLine("Email envoyé avec succès !");
            }
            else
            {
                // Gestion des erreurs d'email
                throw new Exception($"Erreur d'envoi d'email : {response.StatusCode} - {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erreur lors de l'envoi de l'email", ex);
        }
    }
}
