using Flunt.Validations;
using MP.CrossCutting.Utils.Model;
using MP.CrossCutting.Utils.Resources;

namespace MP.Core.Entities
{
    public class Example : Entity
    {
        public string? Name { get; set; }

        public Example()
            : base()
        {

        }

        public void Validate()
        {
            AddNotifications(new Contract<Example>()
              .Requires()
              .IsNotNullOrWhiteSpace(Name, nameof(Name), string.Format(Messages.InvalidFieldNullOrEmpty, nameof(Name)))
              .IsNotNull(CreatedAt, nameof(CreatedAt), string.Format(Messages.InvalidFieldNullOrEmpty, nameof(CreatedAt)))
              .IsNotNull(ModifiedAt, nameof(ModifiedAt), string.Format(Messages.InvalidFieldNullOrEmpty, nameof(ModifiedAt)))
              .IsNotNull(Id, nameof(Id), string.Format(Messages.InvalidFieldNullOrEmpty, nameof(Id))));
        }
    }
}