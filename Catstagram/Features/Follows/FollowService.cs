using Catstagram.Data;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Features.Follows
{
    public class FollowService : IFollowService
    {
        private readonly CatstagramDbContext data;

        public FollowService(CatstagramDbContext data) => this.data = data;

        public async Task<Result> Follow(string userId, string followerId)
        {
            if(await this.data
                         .Follows
                         .AnyAsync(f => f.UserId == userId && f.FollowerId == followerId))
            {
                return "This user is already followed.";
            }


            bool publicProfile = await this.data
                                           .Profiles
                                           .Where(p => p.UserId == userId)
                                           .Select(p => !p.IsPrivate)
                                           .FirstOrDefaultAsync();

            this.data.Follows.Add(new Follow
            {
                UserId = userId,
                FollowerId = followerId,
                IsApproved = publicProfile
            });

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
