namespace Catstagram.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public IEnumerable<Cat> Cats { get; } = new HashSet<Cat>();
    }
}
