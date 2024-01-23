using Catstagram.Server.Features.Cats.Models;

namespace Catstagram.Server.Features.Cats
{
    public interface ICatService
    {
        public Task<int> Create(string imageUrl, string description, string userId);
        public Task<IEnumerable<CatListingServiceModel>> ByUser(string userId);
        public Task<CatDetailsServiceModel> Details(int id);
    }
}
