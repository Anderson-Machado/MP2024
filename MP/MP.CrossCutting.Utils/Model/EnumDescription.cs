namespace MP.CrossCutting.Utils.Model
{
    public class EnumDescription
    {
        public int? Value { get; set; }
        public string? Text { get; set; }
        public string? Description { get; set; }

        public EnumDescription()
        {
        }

        public EnumDescription(int? value, string? text, string? description)
        {
            Value = value;
            Text = text;
            Description = description;
        }

        public EnumDescription(string? text, string? description)
        {
            Text = text;
            Description = description;
        }

        public EnumDescription(int? value, string? text)
        {
            Value = value;
            Text = text;
        }
    }
}