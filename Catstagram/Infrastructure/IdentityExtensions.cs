using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Catstagram.Server.Infrastructure
{
    public static class IdentityExtensions
    {
        public static string GetId(this ClaimsPrincipal user) // Le this dans les param permet de définir une nouvelle méthode pour le type claimprincipal
            => user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                .Value;
    }
}
