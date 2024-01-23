namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Controllers;
    using Catstagram.Server.Authorization;
    using Catstagram.Server.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Catstagram.Server.Features.Cats.Models;
    using Catstagram.Server.Infrastructure.Extensions;

    [CustomAuthorization]
    public class CatsController : ApiController
    {
        private readonly ICatService catService;

        public CatsController(ICatService catService) => this.catService = catService;

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CatDetailsServiceModel>> Details(int id)
            => await this.catService.Details(id);

        [HttpGet]
        public async Task<IEnumerable<CatListingServiceModel>> Mine()
            => await this.catService.ByUser(HttpContext.GetId());

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
        {
            var userId = HttpContext.GetId();

            int catId = await this.catService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), catId);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCatRequestModel model)
        {
            var userId = HttpContext.GetId();

            var updated = await this.catService.Update(model.Id, model.Description, userId);

            return updated ? Ok() : BadRequest();
        }
    }
}
