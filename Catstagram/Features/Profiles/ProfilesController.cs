namespace Catstagram.Server.Features.Profiles
{

    using Catstagram.Server.Controllers;
    using Catstagram.Server.Features.Follows;
    using Catstagram.Server.Features.Profiles.Models;
    using Catstagram.Server.Infrastructure.Services;
    using Microsoft.AspNetCore.Mvc;

    using static Infrastructure.WebConstants;

    public class ProfilesController : ApiController
    {
        private readonly IProfileService profileService;
        private readonly IFollowService followService;
        private readonly ICurrentUserService currentUserService;

        public ProfilesController(IProfileService profileService, 
                                  ICurrentUserService currentUserService,
                                  IFollowService followService)
        {
            this.profileService = profileService;
            this.currentUserService = currentUserService;
            this.followService = followService;
        }

        [HttpGet]
        public async Task<ProfileServiceModel> Mine()
            => await this.profileService.ByUser(this.currentUserService.GetId(), allInformation: true);

        [HttpGet]
        [Route(Id)]
        public async Task<ProfileServiceModel> Details(string id)
        {
            var includeAllInformation = await this.followService
                                                  .IsFollower(id, this.currentUserService.GetId());

            if (!includeAllInformation)
            {
                includeAllInformation = await this.profileService.IsPublic(id);
            }

            return await this.profileService.ByUser(id, allInformation: includeAllInformation);
        }
            

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
