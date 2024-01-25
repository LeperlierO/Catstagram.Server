using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Services;

namespace Catstagram.Server.Features.Profiles
{
    public interface IProfileService
    {
        Task<ProfileServiceModel> ByUser(string userId);

        Task<Result> Update(
            string userId, 
            string email, 
            string userName, 
            string name, 
            string mainPhotoUrl, 
            string webSite, 
            string biography, 
            Gender gender, 
            bool isPrivate);
    }
}
