﻿using E_commerce_BackFinal.Models;
using E_commerce_BackFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_BackFinal.Areas.Admin.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(string role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                    
                }
                return RedirectToAction("index");
            }

            return NotFound();
        }
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UpdateRoleVM updateUserRole = new UpdateRoleVM
            {
                User = user,
                Userid = user.Id,
                Roles = _roleManager.Roles.ToList(),
                UserRoles = await _userManager.GetRolesAsync(user)

            };
            return View(updateUserRole);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(string id, List<string> roles)
        {

            var user = await _userManager.FindByIdAsync(id);
            var dbRoles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            var addedRole = roles.Except(userRoles);
            var removedRole = userRoles.Except(roles);
            await _userManager.AddToRolesAsync(user, addedRole);
            await _userManager.RemoveFromRolesAsync(user, removedRole);

            return RedirectToAction("index");
        }
    }
}
