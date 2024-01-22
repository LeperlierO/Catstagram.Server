using Catstagram.Server.Controllers;

namespace Catstagram.Server.Features.Cats
{
    using Catstagram.Server.Authorization;
    using Catstagram.Server.Data.Models;
    using Microsoft.AspNetCore.Mvc;

    public class CatsController : ApiController
    {
        private readonly ICatService catService;

        public CatsController(ICatService catService) => this.catService = catService;

        [CustomAuthorization]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
        {
            var userId = ((User)HttpContext.Items["User"]).Id;

            var catId = this.catService.Create(model.ImageUrl, model.Description, userId);

            return Created(nameof(this.Create), catId);
        }
    }
}
