namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Controllers;
    using Catstagram.Server.Authorization;
    using Catstagram.Server.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Catstagram.Server.Infrastructure;

    public class CatsController : ApiController
    {
        private readonly ICatService catService;

        public CatsController(ICatService catService) => this.catService = catService;

        [CustomAuthorization]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
        {
            var userId = HttpContext.GetId();

            int catId = await this.catService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), catId);
        }

        [CustomAuthorization]
        [HttpGet]
        public async Task<IEnumerable<CatListingResponseModel>> Mine()
            => await this.catService.ByUser(HttpContext.GetId());
    }
}
