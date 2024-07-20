using Microsoft.EntityFrameworkCore;
using PlantDiseaseX.Identity;

namespace Admin.PlantDiseaseX.Repository
{
    public class UserRepository
    {
        private readonly AppIdentityDbContext _context;

        public UserRepository(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetSignedInUsersCountAsync()
        {
            return await _context.Users.CountAsync(u => u.LastSignInDate.HasValue);
        }
    }

}
