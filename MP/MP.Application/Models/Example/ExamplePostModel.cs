using MP.Application.Validators;
using MP.CrossCutting.Utils.Resources;
using System.ComponentModel.DataAnnotations;

namespace MP.Application.Models.Example
{
    public class ExamplePostModel
    {
        [RequiredValueTypeValidation(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.InvalidID))]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

    }
}