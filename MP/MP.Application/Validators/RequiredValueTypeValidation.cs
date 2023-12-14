using System.ComponentModel.DataAnnotations;

namespace MP.Application.Validators
{
    public class RequiredValueTypeValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not null)
            {
                return !value.Equals(Activator.CreateInstance(value.GetType()));
            }
            return base.IsValid(value);
        }
    }
}