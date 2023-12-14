using System.Text.Json.Serialization;

namespace MP.Application.Models.Example
{
    public class ExamplePatchModel
    {
        [JsonIgnore]
        public Guid? ExampleId { get; set; }
        public string? Name { get; set; }
    }
}