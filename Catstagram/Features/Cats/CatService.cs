using Catstagram.Data;
using Catstagram.Data.Migrations;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Cats.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

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

        public async Task<bool> Update(int id, string description, string userId)
        {
            var cat = await this.data
                                .Cats
                                .Where(c => c.Id == id && c.UserId == userId)
                                .FirstOrDefaultAsync();

            if (cat == null) return false;

            cat.Description = description;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CatListingServiceModel>> ByUser(string userId)
            => await this.data
                    .Cats
                    .Where(c => c.User.Id == userId)
                    .Select(c => new CatListingServiceModel
                    {
                        Id = c.Id,
                        ImageUrl = c.ImageUrl
                    }).ToListAsync();

        public async Task<CatDetailsServiceModel> Details(int id)
            => await this.data
                .Cats
                .Where(c => c.Id == id)
                .Select(c => new CatDetailsServiceModel
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Description = c.Description,
                    UserId = c.UserId,
                    UserName = c.User.UserName
                })
                .FirstOrDefaultAsync();
    }
}
