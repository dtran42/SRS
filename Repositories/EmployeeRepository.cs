using Microsoft.AspNetCore.Identity;
using SRS.Data;
using SRS.Models;
using SRS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<bool> CheckRoleAsync(Guid employeeId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(employeeId.ToString());
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains(roleName) ? true : false;
        }

        public List<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers.OrderBy(u => u.FirstName).ToList();
        }

        public async Task<IEnumerable<string>> GetInRoleAsync(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            var results = users.Select(u => u.Id.ToString()).ToList();
            return results;
        }

        public string GetNameById(Guid id)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id.ToString());
            return user.FirstName + ", " + user.LastName;
        }
    }
}
