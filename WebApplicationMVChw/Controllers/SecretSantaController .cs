using Microsoft.AspNetCore.Mvc;
using WebApplicationMVChw.Models;

namespace WebApplicationMVChw.Controllers
{
    public class SecretSantaController : Controller
    {
        public IActionResult Index()
        {
            var names = new List<string>
            {
                "Anna",
                "John",
                "Alex",
                "Alise"
            };

            var hat = new List<string>(names);

            var random = new Random();

            for (int i = hat.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                var temp = hat[i];
                hat[i] = hat[j];
                hat[j] = temp;
            }

            for (int i = 0; i < names.Count; i++)
            {
                if (names[i] == hat[i])
                {
                    return RedirectToAction("Index");
                }
            }

            var participants = new List<Participant>();

            for (int i = 0; i < names.Count; i++)
            {
                participants.Add(new Participant
                {
                    Name = names[i],
                    Recipient = hat[i]
                });
            }

            return View(participants);
        }
    }
}
