using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;

public class ContactUsService : IContactService
{
    // You can inject a database context or use any other method to persist data

    public async Task<bool> SaveContactForm(ContactForm contactForm)
    {
        try
        {
            var appSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("EmailConfiguration");

            string smtpServer = appSettings["SmtpServer"];
            int smtpPort = int.Parse(appSettings["SmtpPort"]);
            string smtpUsername = appSettings["SmtpUsername"];
            string smtpPassword = appSettings["SmtpPassword"];
            string senderEmail = appSettings["SenderEmail"];
            string toEmail = appSettings["ToEmail"];

            // Create a new instance of SmtpClient
            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                // Create a new MailMessage
                // Create a new MailMessage
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "Automated: New Inquiry Received",
                    Body =
                        $"Dear Nova Sports and Recreation Team,\n\n"
                        + $"This is an automated notification to inform you that a new inquiry has been received through our system. Please find the details below:\n\n"
                        + $"- Date: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\n"
                        + $"- Customer Full Name: {contactForm.Fullname}\n"
                        + $"- Customer Email: {contactForm.Email}\n"
                        + $"- Customer Phone: {contactForm.Phone}\n"
                        + $"- Inquiry Note: {contactForm.Note}\n\n"
                        + $"---\n"
                        + $"This is an automated email from Nova Sports and Recreation."
                };

                // Add recipient email address
                mailMessage.To.Add(toEmail);

                try
                {
                    // Send the email
                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error SaveContactForm: {ex.Message}");
            return false;
        }
    }

    Task<FooterContactUs> IContactService.GetContactInfoAsync()
    {
        return Task.FromResult(GetFooterContactUsFromJsonFile());
    }

    public FooterContactUs GetFooterContactUsFromJsonFile()
    {
        var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Json/contactus.json");

        if (!File.Exists(jsonFilePath))
        {
            // Handle the case when the file does not exist
            return new FooterContactUs();
        }

        var json = File.ReadAllText(jsonFilePath);
        var products = JsonConvert.DeserializeObject<FooterContactUs>(json);

        return products ?? new FooterContactUs();
    }

    Task<bool> IContactService.SaveContactForm(ContactForm contactForm)
    {
        throw new NotImplementedException();
    }

    Task<bool> IContactService.UpdateContactInfoAsync(FooterContactUs updatedContactInfo)
    {
        // Serialize the updated products list to JSON
        string json = JsonConvert.SerializeObject(updatedContactInfo);

        // Specify the path to your JSON file
        string jfilePath = "Json/contactus.json";

        // Write the JSON data to the file
        File.WriteAllTextAsync(jfilePath, json);

        return Task.FromResult(true);
    }
}
