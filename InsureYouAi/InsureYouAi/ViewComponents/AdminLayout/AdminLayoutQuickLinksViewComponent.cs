using AutoMapper;
using InsureYouAi.Dtos.CommentDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InsureYouAi.ViewComponents.AdminLayout
{
    public class AdminLayoutQuickLinksViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
