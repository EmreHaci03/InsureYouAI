using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult ChatWithAI()
        {
            return View();
        }
    }
}
