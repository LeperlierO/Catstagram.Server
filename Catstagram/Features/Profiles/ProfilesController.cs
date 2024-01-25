using Catstagram.Server.Authorization;
using Catstagram.Server.Controllers;
using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Catstagram.Server.Features.Profiles
{

    public class ProfilesController : ApiController
    {
        private readonly IProfileService profileService;
        private readonly ICurrentUserService currentUserService;

        public ProfilesController(IProfileService profileService, ICurrentUserService currentUserService)
        {
            this.profileService = profileService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<ProfileServiceModel>> Mine()
            => await this.profileService.ByUser(this.currentUserService.GetId());

        [HttpPut]
        public async Task<ActionResult> Update (UpdateProfileRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.profileService.Update(
                userId,
                model.Email,
                model.UserName,
                model.Name,
                model.MainPhotoUrl,
                model.WebSite,
                model.Biography,
                model.Gender,
                model.IsPrivate);

            return result.Failure ? BadRequest(result.Error) : Ok();
        }
    }
}
