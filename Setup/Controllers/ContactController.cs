using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using Setup.Models;

namespace Setup.Controllers
{
    public class ContactController : Controller
    {
        public async Task<IActionResult> Index(Developer developer)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel(developer);
            await sendMail();

            return View(profileViewModel);
        }
        public async Task sendMail()
        {
            var apiKey =  "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";
            Console.WriteLine(apiKey);
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("s1169740@student.windesheim.nl", "Thijs Soepenberg"),
                Subject = "Send Grid mail thijs",
                PlainTextContent = "and easy to do anywhere, especially with C#"
            };
            msg.AddTo(new EmailAddress("s1169740@student.windesheim.nl", "hoi"));
            var response = await client.SendEmailAsync(msg);
          

            // A success status code means SendGrid received the email request and will process it.
            // Errors can still occur when SendGrid tries to send the email. 
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");

        }
        [HttpPost] 
        public IActionResult Result(string Naam, int Id)
        {
            Console.WriteLine("RESULT CONTROLLER");
            Console.WriteLine(Naam);
            Console.WriteLine(Id.ToString());
            Person p = new Person(Naam, Id, new List<Friendship>());
            Console.WriteLine(p.Username);
            PersonViewModel personViewModel = new PersonViewModel(p);
            Console.WriteLine(personViewModel.Name);




            return View("ResultPage", personViewModel);
        }

        public IActionResult ResultPage(PersonViewModel pvw)
        {


            return View(pvw);
        }
    }
}
