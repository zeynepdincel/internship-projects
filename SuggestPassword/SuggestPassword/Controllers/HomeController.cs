using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SuggestPassword.Models;

namespace SuggestPassword.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string password = GeneratePassword(); 
            ViewData["password"] = password;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        static string GeneratePassword()
        {
            int length = 8;
            bool useSpecialChar = true;
            string baseString = Guid.NewGuid().ToString("N");

            while (baseString.Length < length)
            {
                baseString += Guid.NewGuid().ToString();
            }
            if (useSpecialChar)
            {
                string specialChars = "!@#$%^&*()_+-=[]{}|;:'\",.<>?";
                Random random = new Random();
                for (int i = 0; i < length / 4; i++)
                {
                    int insertIndex = random.Next(baseString.Length);
                    char specialChar = specialChars[random.Next(specialChars.Length)];
                    baseString = baseString.Insert(insertIndex, specialChar.ToString());
                }

            }
           
            return baseString.Substring(0, length);
            
        }
    }
}
