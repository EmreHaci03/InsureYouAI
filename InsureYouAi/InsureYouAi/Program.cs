using InsureYouAi.Context;
using InsureYouAi.Dtos.Mapping;
using InsureYouAi.Entities;
using InsureYouAi.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using FluentValidation;
using InsureYouAi.ValidationRules.CategoryValidator;
using FluentValidation.AspNetCore;
using InsureYouAi.Models.IdentityValidator;
using InsureYouAi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Error/403/";

    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        context.Response.Redirect("/Error/401");
        return Task.CompletedTask;
    };
});

builder.Services.AddDbContext<InsureAiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHttpClient();

builder.Services.AddIdentity<AppUser, AppRole>()
     .AddEntityFrameworkStores<InsureAiContext>()
     .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();


builder.Services.AddAutoMapper(cfg =>{}, typeof(GeneralMapping).Assembly);
builder.Services.AddApplicationServices();
builder.Services.AddRepositoryServices();

builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>(); // RuleFor Gibi Validator Yazma Araçlarý Ýçin
builder.Services.AddFluentValidationAutoValidation(); // Validatorlarý DI Containere Kaydeder.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithRedirects("/Error/{0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapHub<ChatHub>("/chathub");
app.Run();
