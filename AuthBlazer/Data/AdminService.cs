using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

            // Cast to List<ApplicationUser> if necessary
            return adminUsers.ToList();
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            return await _userManager.Users.CountAsync();
        }
    }
}
