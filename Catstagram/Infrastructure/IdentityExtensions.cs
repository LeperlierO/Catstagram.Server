using Catstagram.Server.Data.Models;
using System.Security.Claims;

namespace Catstagram.Server.Infrastructure
{
    public static class IdentityExtensions
    {
        public static string GetId(this HttpContext context) // Le this dans les param permet de définir une nouvelle méthode pour le type claimprincipal
        {
            return ((User)context.Items["User"]).Id;
        }
    }
}
