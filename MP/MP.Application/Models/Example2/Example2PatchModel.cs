using System.Text.Json.Serialization;

namespace MP.Application.Models.Example2
{
    public class Example2PatchModel
    {
        [JsonIgnore]
        public Guid? Example2Id { get; set; }
        public string? Description { get; set; }
        public string? OtherName { get; set; }
    }
}