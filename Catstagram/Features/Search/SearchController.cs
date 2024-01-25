namespace Catstagram.Server.Features.Search
{
    using Catstagram.Server.Controllers;
    using Catstagram.Server.Features.Search.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : ApiController
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService) => this.searchService = searchService;

        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(Profiles))]
        public async Task<IEnumerable<ProfileSearchServiceModel>> Profiles(string query)
            => await this.searchService.Profiles(query);
    }
}
