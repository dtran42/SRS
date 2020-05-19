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
using SRS.Data;
using SRS.Models;
using SRS.ViewModels;

namespace SRS.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var accounts = new List<UserViewModel>();

            foreach (var u in users)
            {
                var role = await GetRolesOfUser(u);
                var info = _context.UserInfos.FirstOrDefault(i => i.Email == u.Email);
                var account = new UserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = role,
                    FullName = info != null ? info.FullName : string.Empty,
                    Phone = info != null ? info.Phone : string.Empty,
                    Location = info != null ? info.Location : string.Empty,
                    BusinessArea = info != null ? info.BusinessArea : string.Empty,
                    TestFiled = info != null ? info.TestFiled : string.Empty,
                };

                accounts.Add(account);
            }
            return View(accounts);
        }

        public IActionResult Create()
        {
            var roles = _roleManager.Roles.OrderBy(r => r.Name).ToList();
            ViewData["Roles"] = new SelectList(roles, "Id", "Name");
            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel account)
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
                        {
                            var info = new UserInfo() 
                            {
                                Email = account.Email,
                                FullName = account.FullName,
                                Phone = account.Phone,
                                Location = account.Location,
                                BusinessArea = account.BusinessArea,
                                TestFiled = account.TestFiled
                            };
                            _context.UserInfos.Add(info);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
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
                var info = _context.UserInfos.FirstOrDefault(i => i.Email == editUser.Email);
                var account = new UserViewModel
                {
                    Id = editUser.Id,
                    Email = editUser.Email,
                    SelectedRoleId = roleId,
                    FullName = info != null ? info.FullName : string.Empty,
                    Phone = info != null ? info.Phone : string.Empty,
                    Location = info != null ? info.Location : string.Empty,
                    BusinessArea = info != null ? info.BusinessArea : string.Empty,
                    TestFiled = info != null ? info.TestFiled : string.Empty,
                };

                ViewData["Roles"] = new SelectList(roleList, "Id", "Name");
                return View(account);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel account)
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
                                var info = _context.UserInfos.FirstOrDefault(i => i.Email == account.Email);
                                if (info == null)
                                {
                                    info = new UserInfo()
                                    {
                                        Email = account.Email,
                                        FullName = account.FullName,
                                        Phone = account.Phone,
                                        Location = account.Location,
                                        BusinessArea = account.BusinessArea,
                                        TestFiled = account.TestFiled
                                    };
                                    _context.UserInfos.Add(info);
                                }
                                else
                                {
                                    info.FullName = account.FullName;
                                    info.Phone = account.Phone;
                                    info.Location = account.Location;
                                    info.BusinessArea = account.BusinessArea;
                                    info.TestFiled = account.TestFiled;
                                    _context.UserInfos.Update(info);
                                }
                                _context.SaveChanges();
                                return RedirectToAction("Index");
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