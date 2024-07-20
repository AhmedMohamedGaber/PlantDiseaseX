using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.PlantDiseaseX.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Admin.PlantDiseaseX.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetCurrentlyLoggedInUsersAsync()
        {
            var loggedInUsers = new List<ApplicationUser>();

            var httpContext = _httpContextAccessor.HttpContext;
            var userPrincipal = httpContext.User;

            if (userPrincipal?.Identity != null && userPrincipal.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(userPrincipal.Identity.Name);
                if (user != null)
                {
                    loggedInUsers.Add(user);
                }
            }

            return loggedInUsers;
        }
    }
}
