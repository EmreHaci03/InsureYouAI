using AutoMapper;
using InsureYouAi.Dtos.CommentDtos;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IMapper _mapper;
        public CommentController(ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> CommentList()
        {
            var values = await commentService.CommentListWithDetail();
            var mapper = _mapper.Map<List<ResultCommentDto>>(values);
            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> CommentDetail(int id)
        {
            var values = await commentService.GetByIdAsync(id);
            var mapper = _mapper.Map<GetCommentByIdDto>(values);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveComment(int id)
        {
            var result = await commentService.CommentStatusTypeChangeApproved(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Yorum onaylandı.";
            }
            else
            {
                TempData["ErrorMessage"] = "Yorum bulunamadı.";
            }

            return RedirectToAction("CommentList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment=await commentService.GetCommentWithDetail(id);
            if (comment == null)
            {
                TempData["ErrorMessage"] = "Silinmek İstenen Yorum Bulunamamıştır";
                return View();
            }
            await commentService.DeleteAsync(comment);
            TempData["SuccessMessage"] = "Yorum başarıyla silindi.";
            return RedirectToAction("CommentList");
        }
    }
}
