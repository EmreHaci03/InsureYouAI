using InsureYouAi.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.ViewComponents.Default
{
    public class DefaultCounterViewComponent:ViewComponent
    {
        private readonly InsureAiContext _context;

        public DefaultCounterViewComponent(InsureAiContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Services = await _context.Services.CountAsync();
            ViewBag.Categories = await _context.Categories.CountAsync();
            ViewBag.Articles = await _context.Articles.CountAsync();
            ViewBag.Testimonial = await _context.Testimonials.CountAsync();
            return View();
        }
    }
}
