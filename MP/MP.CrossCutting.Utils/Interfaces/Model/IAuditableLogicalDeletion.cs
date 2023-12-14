namespace MP.CrossCutting.Utils.Interfaces.Model
{
    public interface IAuditableLogicalDeletion
    {
        DateTimeOffset? CreatedAt { get; }
        DateTimeOffset? ModifiedAt { get; }
        DateTimeOffset? DeletedAt { get; }
    }
}