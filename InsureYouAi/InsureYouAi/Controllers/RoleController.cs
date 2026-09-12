using AutoMapper;
using InsureYouAi.Dtos.AppRoleDtos;
using InsureYouAi.Dtos.AppUserDtos;
using InsureYouAi.Entities;
using InsureYouAi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsureYouAi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper _mapper;
        public RoleController(RoleManager<AppRole> roleManager, IMapper mapper, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            _mapper = mapper;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> RoleList()
        {
            var role = await roleManager.Roles.ToListAsync();
            var mapper = _mapper.Map<List<ResultAppRoleDto>>(role);
            return View(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateAppRoleDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            AppRole appRole = new AppRole()
            {
                Name = dto.RoleName
            };
            var result=await roleManager.CreateAsync(appRole);

            if (result.Succeeded)
                return RedirectToAction("RoleList");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound("Silinmek İstenen Rol Bulunamadı!");

            await roleManager.DeleteAsync(role);
            return RedirectToAction("RoleList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound("Güncellenmek İstenen Rol Bulunamadı!");

            var mapper = _mapper.Map<UpdateAppRoleDto>(role);
            return View(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateAppRoleDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == dto.RoleId);

            if (role == null)
                return NotFound("Güncellenmek İstenen Rol Bulunamadı!");

            role.Name = dto.RoleName;

            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return RedirectToAction("RoleList");


            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(dto);


        }

        [HttpGet]
        public async Task<IActionResult> RoleUserList(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound("Rol Bulunamadı!");

            var user = await userManager.GetUsersInRoleAsync(role.Name);
            var mapper = _mapper.Map<List<ResultAppUserDto>>(user);

            ViewBag.RoleName = role.Name;
            ViewBag.RoleId = role.Id;

            return View(mapper);


        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var allRoles = await roleManager.Roles.ToListAsync();
            var userRoles = await userManager.GetRolesAsync(user);


            var model = new RoleAssignViewModel
            {
                UserId = user.Id,
                UserName = user.Name,
                roleChecks = allRoles.Select(x => new RoleCheckItemViewModel
                {
                    RoleId = x.Id,
                    RoleName = x.Name,
                    IsSelected = userRoles.Contains(x.Name)
                }).ToList()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(RoleAssignViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();

            var currentRoles = await userManager.GetRolesAsync(user);

            var selectedRoles = model.roleChecks
                .Where(x => x.IsSelected)
                .Select(x => x.RoleName)
                .ToList();


            var rolesToAdd = selectedRoles.Except(currentRoles).ToList();
            var rolesToRemove= currentRoles.Except(selectedRoles).ToList();


            if (rolesToAdd.Any())
                await userManager.AddToRolesAsync(user, rolesToAdd);

            if (rolesToRemove.Any())
                await userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return RedirectToAction("RoleList");
        }


    }
}
