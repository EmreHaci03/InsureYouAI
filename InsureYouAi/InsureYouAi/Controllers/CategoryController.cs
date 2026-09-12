using AutoMapper;
using InsureYouAi.Dtos.CategoryDtos;
using InsureYouAi.Entities;
using InsureYouAi.Models;
using Microsoft.AspNetCore.Authorization;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await categoryService.CategoryListWithArticleCount();
            var mapped = _mapper.Map<List<CategoryArticleCountViewModel>>(values);
            return View(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var category = _mapper.Map<Category>(dto);
            await categoryService.AddAsync(category);
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await categoryService.GetByIdAsync(id);
            if (category is null)
                return NotFound();

            await categoryService.DeleteAsync(category);
            TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
            return RedirectToAction("CategoryList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var values = await categoryService.GetByIdAsync(id);
            var mapper = _mapper.Map<UpdateCategoryDto>(values);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var category = _mapper.Map<Category>(dto);
            await categoryService.UpdateAsync(category);
            return RedirectToAction("CategoryList");
        }
    }
}
