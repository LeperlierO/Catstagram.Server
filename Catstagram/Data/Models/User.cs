﻿namespace Catstagram.Server.Data.Models
{
    using Catstagram.Server.Data.Models.Base;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser, IEntity
    {
        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public IEnumerable<Cat> Cats { get; } = new HashSet<Cat>();
    }
}
