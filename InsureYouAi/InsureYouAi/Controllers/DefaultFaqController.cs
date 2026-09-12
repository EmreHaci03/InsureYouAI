using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    public class DefaultFaqController : Controller
    {
        public IActionResult DefaultFagList()
        {
            return View();
        }
    }
}
