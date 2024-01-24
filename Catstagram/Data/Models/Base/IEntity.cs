namespace Catstagram.Server.Data.Models.Base
{
    using System.ComponentModel.DataAnnotations;

    public interface IEntity
    {
        DateTime CreatedOn { get; set; }

        [Required]
        string? CreatedBy { get; set; }

        DateTime? ModifiedOn { get; set; }

        string? ModifiedBy { get; set; }
    }
}
