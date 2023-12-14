using MP.Core.Entities.Dtos;
using System.Linq.Expressions;

namespace MP.Core.Entities.Complements
{
    public static class EntityQueryExtends
    {

        public static Expression<Func<Example2, bool>> SearchQuery(this Example2 _, SearchDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return e => (dto.Term == null || e.Description!.Contains(dto.Term) || e.Other!.Name!.Contains(dto.Term));
        }

        public static Expression<Func<Example, bool>> ExampleNameQuery(this Example _, string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            return s => s.Name!.ToLower() == name.ToLower();
        }

        public static Expression<Func<Example2, bool>> Example2OtherNameQuery(this Example2 _, string? otherName)
        {
            if (string.IsNullOrEmpty(otherName))
            {
                throw new ArgumentNullException(nameof(otherName));
            }
            return s => s.Other.Name!.ToLower() == otherName!.ToLower();
        }
    }
}