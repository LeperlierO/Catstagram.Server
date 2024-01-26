using Catstagram.Data;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Features.Profiles
{
    public class ProfileService : IProfileService
    {
        private readonly CatstagramDbContext data;

        public ProfileService(CatstagramDbContext data)
            => this.data = data;

        public async Task<ProfileServiceModel> ByUser(string userId, bool allInformation = false)
            => await this.data
                         .Users
                         .Where(u => u.Id == userId)
                         .Select(u => allInformation
                         ? new PublicProfileServiceModel
                         {
                             Name = u.Profile.Name,
                             Biography = u.Profile.Biography,
                             Gender = u.Profile.Gender.ToString(),
                             MainPhotoUrl = u.Profile.MainPhotoUrl,
                             WebSite = u.Profile.WebSite,
                             IsPrivate = u.Profile.IsPrivate
                         }
                         : new ProfileServiceModel
                         {
                             Name = u.Profile.Name,
                             MainPhotoUrl = u.Profile.MainPhotoUrl,
                             IsPrivate = u.Profile.IsPrivate
                         })
                         .FirstOrDefaultAsync();

        public async Task<Result> Update(string userId, string email, string userName, string name, string mainPhotoUrl, string webSite, string biography, Gender gender, bool isPrivate)
        {
            var user = await this.data
                                 .Users
                                 .Include(u => u.Profile)
                                 .FirstOrDefaultAsync(u => u.Id == userId);

            if(user == null) return "User does not exist.";
            if (user.Profile == null) user.Profile = new Profile();

            var emailResult = await ChangeProfileEmail(user, userId, email);
            if (emailResult.Failure) return emailResult;

            var userNameResult = await ChangeProfileUserName(user, userId, userName);
            if(userNameResult.Failure) return userNameResult;

            ChangeProfile(user.Profile, name, mainPhotoUrl, webSite, biography, gender, isPrivate);

            await this.data.SaveChangesAsync();

            return true;
        }

        private async Task<Result> ChangeProfileEmail(User user, string userId, string email)
        {
            if (!String.IsNullOrWhiteSpace(email) && user.Email != email)
            {
                var emailExists = await this.data
                                            .Users
                                            .AnyAsync(u => u.Id != userId && u.Email == email);

                if (emailExists) return "The provided email is already taken.";

                user.Email = email;
            }

            return true;
        }

        private async Task<Result> ChangeProfileUserName(User user, string userId, string userName)
        {
            if (!String.IsNullOrWhiteSpace(userName) && user.UserName != userName)
            {
                var userNameExists = await this.data
                              .Users
                              .AnyAsync(u => u.Id != userId && u.UserName == userName);

                if (userNameExists) return "The provided user name is already taken.";

                user.UserName = userName;
            }

            return true;
        }

        private void ChangeProfile(Profile profile, string name, string mainPhotoUrl, string webSite, string biography, Gender gender, bool isPrivate)
        {
            if (profile.Name != name) profile.Name = name;
            if (profile.MainPhotoUrl != mainPhotoUrl) profile.MainPhotoUrl = mainPhotoUrl;
            if (profile.WebSite != webSite) profile.WebSite = webSite;
            if (profile.Biography != biography) profile.Biography = biography;
            if (profile.Gender != gender) profile.Gender = gender;
            if (profile.IsPrivate != isPrivate) profile.IsPrivate = isPrivate;
        }

        public async Task<bool> IsPublic(string userId)
            => await this.data
                         .Profiles
                         .Where(p => p.UserId == userId)
                         .Select(p => !p.IsPrivate)
                         .FirstOrDefaultAsync();
    }
}
