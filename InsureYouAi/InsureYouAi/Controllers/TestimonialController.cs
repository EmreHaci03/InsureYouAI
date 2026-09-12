using AutoMapper;
using InsureYouAi.Dtos.CategoryDtos;
using InsureYouAi.Dtos.PricingPlanDtos;
using InsureYouAi.Dtos.TestimonialDtos;
using InsureYouAi.Entities;
using Microsoft.AspNetCore.Authorization;
using InsureYouAi.Service.Concrete;
using InsureYouAi.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService TestimonialService;
        private readonly ICategoryService categoryService;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;
        public TestimonialController(ITestimonialService TestimonialService, IMapper mapper, ICategoryService categoryService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.TestimonialService = TestimonialService;
            _mapper = mapper;
            this.categoryService = categoryService;
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> TestimonialList()
        {
            var Testimonial = await TestimonialService.GetAllAsync();
            var mapper = _mapper.Map<List<ResultTestimonialDto>>(Testimonial);
            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTestimonial()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var PricingPlan = _mapper.Map<Testimonial>(dto);
            await TestimonialService.AddAsync(PricingPlan);
            TempData["SuccessMessage"] = "Referans Başarıyla Eklendi.";
            return RedirectToAction("TestimonialList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var Testimonial = await TestimonialService.GetByIdAsync(id);
            if (Testimonial is null)
                return NotFound();

            await TestimonialService.DeleteAsync(Testimonial);
            TempData["SuccessMessage"] = "Referans Başarıyla silindi.";
            return RedirectToAction("TestimonialList");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var Testimonial = await TestimonialService.GetByIdAsync(id);
            if (Testimonial is null)
                return NotFound();

            var mapper = _mapper.Map<UpdateTestimonialDto>(Testimonial);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var Testimonial = _mapper.Map<Testimonial>(dto);
            await TestimonialService.UpdateAsync(Testimonial);
            TempData["SuccessMessage"] = "Referans başarıyla Güncellendi.";
            return RedirectToAction("TestimonialList");
        }


        [HttpGet]
        public async Task<IActionResult> CreateTestimonialWithClaude()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonialWithClaude(string prompt)
        {
            var apiKey = configuration["ApiKey:Claude"];
            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

            var systemPrompt = @"Sen bir sigorta şirketi için profesyonel içerik üreten bir blog yazarısın...";


            var requestData = new
            {
                model = "claude-sonnet-4-6",
                max_tokens = 1024,
                system = systemPrompt,
                messages = new[]
                 {
                     new {role="user",content=prompt}
                 }
            };

            var response = await client.PostAsJsonAsync("https://api.anthropic.com/v1/messages", requestData);


            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"AI İçerik Üretiminde Hata Oluştu{error} {response.StatusCode}";
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var generateContent = doc.RootElement
                .GetProperty("content")[0]
                .GetProperty("text")
                .GetString();

            ViewBag.Content = generateContent ?? string.Empty;
            return View();
        }
    }
}
