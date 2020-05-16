using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SRS.Common;
using SRS.Models;

namespace SRS.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var accounts = new List<ApplicationUser>();

            foreach (var u in users)
            {
                var role = await GetRolesOfUser(u);
                var account = new ApplicationUser
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = role
                };

                accounts.Add(account);
            }
            return View(accounts);
        }

        public IActionResult Create()
        {
            var roles = _roleManager.Roles.OrderBy(r => r.Name).ToList();
            ViewData["Roles"] = new SelectList(roles, "Id", "Name");
            return View(new ApplicationUser());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser account)
        {
            var existUser = await _userManager.FindByEmailAsync(account.Email);

            if (existUser == null)
            {
                var role = await _roleManager.FindByIdAsync(account.SelectedRoleId);
                if (ModelState.IsValid)
                {
                    var user = new IdentityUser { UserName = account.Email, Email = account.Email };
                    IdentityResult result = await _userManager.CreateAsync(user, account.Password);
                    if (result.Succeeded)
                    {
                        var addRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
                        if (addRoleResult.Succeeded)
                            return RedirectToAction("Index");
                        else
                            Utils.Errors(addRoleResult, ModelState);
                    }
                    else
                        Utils.Errors(result, ModelState);
                }
            }
            else
                ModelState.AddModelError("CreateUser", "The email already exists. Please use a different email!");
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var roleList = _roleManager.Roles.OrderBy(r => r.Name).ToList();
            var editUser = await _userManager.FindByIdAsync(id);
            if (editUser != null)
            {
                var roles = await _userManager.GetRolesAsync(editUser);
                string roleId = string.Empty;
                if (roles.Count > 0)
                {
                    roleId = roleList.Find(r => r.Name == roles[0]).Id;
                }
                var account = new ApplicationUser
                {
                    Id = editUser.Id,
                    Email = editUser.Email,
                    SelectedRoleId = roleId
                };

                ViewData["Roles"] = new SelectList(roleList, "Id", "Name");
                return View(account);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser account)
        {
            var user = await _userManager.FindByIdAsync(account.Id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(account.Email))
                    user.Email = account.Email;
                else
                    ModelState.AddModelError("", "Email cannot be empty!");

                if (string.IsNullOrEmpty(account.SelectedRoleId))
                    ModelState.AddModelError("", "Role cannot be empty!");

                if (!string.IsNullOrEmpty(account.Email) && !string.IsNullOrEmpty(account.SelectedRoleId))
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var removeRole = await _userManager.RemoveFromRoleAsync(user, roles[0]);
                        if (removeRole.Succeeded)
                        {
                            var role = await _roleManager.FindByIdAsync(account.SelectedRoleId);
                            var updateRole = await _userManager.AddToRoleAsync(user, role.Name);
                            if (updateRole.Succeeded)
                            {
                                return RedirectToPage("Index");
                            }
                            else
                                Utils.Errors(updateRole, ModelState);
                        }
                        else
                            Utils.Errors(removeRole, ModelState);
                    }
                    else
                        Utils.Errors(result, ModelState);
                }
                else
                    ModelState.AddModelError("", "User not found!");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Utils.Errors(result, ModelState);
            }
            else
                ModelState.AddModelError("Delete", "User not found!");
            return View("Index");
        }

        private async Task<string> GetRolesOfUser(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = string.Empty;
            foreach (var r in roles)
            {
                if (!role.Contains(","))
                {
                    role = r;
                }
                else
                {
                    role = "," + r;
                }
            }

            return role;
        }
    }
}