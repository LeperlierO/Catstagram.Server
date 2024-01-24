using Catstagram.Server.Features.Profiles.Models;

namespace Catstagram.Server.Features.Profiles
{
    public interface IProfileService
    {
        Task<ProfileServiceModel> ByUser(string userId);
    }
}
