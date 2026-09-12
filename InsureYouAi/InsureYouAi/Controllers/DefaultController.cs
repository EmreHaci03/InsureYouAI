using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
