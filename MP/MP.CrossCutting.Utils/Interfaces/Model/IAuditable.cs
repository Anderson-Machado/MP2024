namespace MP.CrossCutting.Utils.Interfaces.Model
{
    public interface IAuditable
    {
        DateTimeOffset? CreatedAt { get; }
        DateTimeOffset? ModifiedAt { get; }
    }
}