using Flunt.Validations;
using MP.CrossCutting.Utils.Model;
using MP.CrossCutting.Utils.Resources;

namespace MP.Core.Entities
{
    public class Example2 : EntityLogicalDelete
    {
        public string? Description { get; set; }

        public Example? Other { get; set; }
        public Guid? OtherId { get; set; }

        public Example2()
            : base()
        {
        }

        public void Delete()
        {
            SetDeletedDate();
        }

        public void Validate()
        {
            AddNotifications(new Contract<Example2>()
            .Requires()
            .IsNotNullOrWhiteSpace(Description, nameof(Description), string.Format(Messages.InvalidFieldNullOrEmpty, nameof(Description)))
            .IsNotNull(OtherId, nameof(OtherId), string.Format(Messages.InvalidFieldNullOrEmpty, nameof(OtherId)))
            .IsNotNull(CreatedAt, nameof(CreatedAt), string.Format(Messages.InvalidFieldNullOrEmpty, nameof(CreatedAt)))
            .IsNotNull(ModifiedAt, nameof(ModifiedAt), string.Format(Messages.InvalidFieldNullOrEmpty, nameof(ModifiedAt)))
            .IsNotNull(Id, nameof(Id), string.Format(Messages.InvalidFieldNullOrEmpty, nameof(Id))));
        }
    }
}