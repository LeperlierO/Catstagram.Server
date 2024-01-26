using Catstagram.Server.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Catstagram.Server.Features.Profiles.Models
{
    public class ProfileServiceModel
    {
        public string Name { get; set; }

        public string MainPhotoUrl { get; set; }

        public bool IsPrivate { get; set; }
    }
}
