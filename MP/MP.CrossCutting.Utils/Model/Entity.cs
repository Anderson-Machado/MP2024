using Flunt.Notifications;
using MP.CrossCutting.Utils.Extensions;
using MP.CrossCutting.Utils.Interfaces.Model;

namespace MP.CrossCutting.Utils.Model
{
    public abstract class Entity : Notifiable<Notification>, IAuditable, IIdentifiable
    {
        private int? _requestedHashCode;

        public Guid? Id { get; protected set; }
        public DateTimeOffset? CreatedAt { get; protected set; }
        public DateTimeOffset? ModifiedAt { get; protected set; }

        protected Entity(Guid id)
        {
            SetId(id);
            SetCreatedAt();
        }

        protected Entity()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Set id
        /// </summary>
        /// <param name="id">unique identify value</param>
        protected void SetId(Guid id) => Id = id;

        /// <summary>
        /// Set CreatedAt and ModifiedAt is current DateTimeOffset
        /// </summary>
        protected void SetCreatedAt()
        {
            CreatedAt = DateTimeOffset.Now;
            ModifiedAt = CreatedAt;
        }

        /// <summary>
        /// Set ModifiedAt is current DateTimeOffset
        /// </summary>
        /// <param name="modifiedDateTime">If null set now</param>
        protected void SetModifiedDate(DateTimeOffset? modifiedDateTime = default) => ModifiedAt = modifiedDateTime ?? DateTimeOffset.Now;

        /// <summary>
        /// Verify Id is null or empty
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTransient() => this.Id.IsNullOrEmpty();

        #region Override Methods

        public override bool Equals(object? obj)
        {
            if (obj is not EntityLogicalDelete item)
                return false;

            if (ReferenceEquals(this, item))
                return true;

            if (GetType() != item.GetType())
                return false;

            if (item.IsTransient() || IsTransient())
                return false;

            return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient())
#pragma warning disable S3249 // Classes directly extending "object" should not call "base" in "GetHashCode" or "Equals"
                return base.GetHashCode();
#pragma warning restore S3249 // Classes directly extending "object" should not call "base" in "GetHashCode" or "Equals"

            _requestedHashCode ??= Id.GetHashCode() ^ 31;

            return _requestedHashCode.Value;
        }

        #endregion
    }
}