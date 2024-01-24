using Catstagram.Data;
using Catstagram.Server.Features.Profiles.Models;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Features.Profiles
{
    public class ProfileService : IProfileService
    {
        private readonly CatstagramDbContext data;

        public ProfileService(CatstagramDbContext data)
            => this.data = data;

        public Task<ProfileServiceModel> ByUser(string userId)
            => this.data
                   .Users
                   .Where(u => u.Id == userId)
                   .Select(u => new ProfileServiceModel
                   {
                     Name = u.Profile.Name,
                     Biography = u.Profile.Biography,
                     Gender = u.Profile.Gender.ToString(),
                     MainPhotoUrl = u.Profile.MainPhotoUrl,
                     WebSite = u.Profile.WebSite,
                     IsPrivate = u.Profile.IsPrivate
                   })
                   .FirstOrDefaultAsync();
    }
}
