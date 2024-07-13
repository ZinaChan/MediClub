using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MediClubApp.Models;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Google.Apis.Auth.OAuth2; 
using Google.Apis.Util.Store; 

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    public HomeController(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> ContactForm(ContactFormModel model)
    {

        if (ModelState.IsValid)
        {
            try
            {
                await SendEmailAsync(model);
                return Json(new { success = true, message = "Your message has been received. We will contact you shortly." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to send message. Please try again later." });
            }
        }

        return Json(new { success = false, message = "Invalid data. Please check your inputs." });
    }
   
        private async Task SendEmailAsync(ContactFormModel model)
        {
           var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]!);
            var smtpUsername = _configuration["EmailSettings:SmtpUser"];
            var smtpPassword = _configuration["EmailSettings:SmtpPass"];

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(smtpUsername));
            email.To.Add(MailboxAddress.Parse(model.Email));
            email.Subject = "New Contact Us Message";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = $"Name: {model.Name}\nEmail: {model.Email}\n\nMessage:\n{model.Message}"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(smtpUsername, smtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

} 