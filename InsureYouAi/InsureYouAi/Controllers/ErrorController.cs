using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult NotFound()
        {
            return View();
        }
        [Route("Error/401")]
        public IActionResult Unauthorized()
        {
            return View();
        }

        [Route("Error/403")]
        public IActionResult Forbidden()
        {
            return View();
        }


        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return RedirectToAction("NotFound");
                case 403:
                    return RedirectToAction("Forbidden");
                case 401:
                    return RedirectToAction("Unauthorized");
                default:
                    return View("Error");
            }
        }


    }
}
