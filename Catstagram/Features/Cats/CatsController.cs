namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Catstagram.Server.Features.Cats.Models;

    using static Infrastructure.WebConstants;
    using Catstagram.Server.Infrastructure.Services;

    public class CatsController : ApiController
    {
        private readonly ICatService catService;
        private readonly ICurrentUserService currentUserService;

        public CatsController(ICatService catService, ICurrentUserService currentUserService)
        {
            this.catService = catService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<CatDetailsServiceModel>> Details(int id)
            => await this.catService.Details(id);

        [HttpGet]
        public async Task<IEnumerable<CatListingServiceModel>> Mine()
            => await this.catService.ByUser(this.currentUserService.GetId());

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            int catId = await this.catService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), catId);
        }

        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult> Update(int id, UpdateCatRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.catService.Update(id, model.Description, userId);

            return result.Failure ? BadRequest() : Ok();
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.catService.Delete(id, userId);

            return result.Failure ? BadRequest() : Ok();
        }
    }
}
