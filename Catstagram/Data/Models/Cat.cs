namespace Catstagram.Server.Data.Models
{
    using Catstagram.Server.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;

    using static Validation.Cat;

    public class Cat : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(Validation.Cat.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
