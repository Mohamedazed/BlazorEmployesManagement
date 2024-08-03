using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthBlazer.Data
{
    public class AdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<List<ApplicationUser>> GetAdminUsersAsync()
        {
            var adminRole = await _roleManager.FindByNameAsync("admin");
            if (adminRole == null)
            {
                // Handle if admin role doesn't exist
                return new List<ApplicationUser>();
            }

            var adminUsers = await _userManager.GetUsersInRoleAsync(adminRole.Name);

            return adminUsers.ToList();
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            return await _userManager.Users.CountAsync();
        }

        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // Handle if user not found
                return new List<string>();
            }

            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }
    }
}
