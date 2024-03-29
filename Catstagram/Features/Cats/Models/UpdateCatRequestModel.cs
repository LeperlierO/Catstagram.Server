﻿using System.ComponentModel.DataAnnotations;

namespace Catstagram.Server.Features.Cats.Models
{
    using static Data.Validation.Cat;

    public class UpdateCatRequestModel
    {
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
