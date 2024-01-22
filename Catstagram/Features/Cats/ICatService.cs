namespace Catstagram.Server.Features.Cats
{
    public interface ICatService
    {
        public Task<int> Create(string imageUrl, string description, string userId);
        public Task<IEnumerable<CatListingResponseModel>> ByUser(string userId);
    }
}
