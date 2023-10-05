using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using PF_Pach_OS.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PF.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;

    public string SendGridApi { get; set; }

    public EmailSender(IConfiguration _config,
        ILogger<EmailSender> logger)
    {
        SendGridApi = _config.GetValue<string>("SendGrid:SecretKey");
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (SendGridApi == null)
        {
            throw new Exception("Null SendGridKey");
        }
        await Execute(SendGridApi, subject, message, toEmail);
    }

    public async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("pachitoche2501259@gmail.com"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);

        var response = await client.SendEmailAsync(msg);
        _logger.LogInformation(response.IsSuccessStatusCode
                               ? $"Email to {toEmail} queued successfully!"
                               : $"Failure Email to {toEmail}");

    }
}