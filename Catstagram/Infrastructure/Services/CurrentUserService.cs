using Catstagram.Server.Data.Models;
using Catstagram.Server.Infrastructure.Extensions;

namespace Catstagram.Server.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext context;



        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => this.context = httpContextAccessor.HttpContext;

        public string GetId()
            => this.context
                   ?.GetId();

        public string GetUserName()
            => ((User)this.context
                         ?.Items["User"])
                         ?.UserName;
    }
}
