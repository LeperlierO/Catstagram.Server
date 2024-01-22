using Catstagram.Data;
using Catstagram.Data.Migrations;
using Catstagram.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Features.Cats
{
    public class CatService : ICatService
    {
        private readonly CatstagramDbContext data;

        public CatService(CatstagramDbContext data) => this.data = data;

        public async Task<int> Create(string imageUrl, string description, string userId)
        {
            var cat = new Cat
            {
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            data.Add(cat);

            await data.SaveChangesAsync();

            return cat.Id;
        }

        public async Task<IEnumerable<CatListingResponseModel>> ByUser(string userId)
            => await this.data
                    .Cats
                    .Where(c => c.User.Id == userId)
                    .Select(c => new CatListingResponseModel
                    {
                        Id = c.Id,
                        ImageUrl = c.ImageUrl
                    }).ToListAsync();
    }
}
