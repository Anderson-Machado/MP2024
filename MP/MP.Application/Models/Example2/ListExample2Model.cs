using Dawn;
using MP.Application.Models.Common;

namespace MP.Application.Models.Example2
{
    public class ListExample2Model : PaginationModel
    {
        public IEnumerable<Example2Model> Examples { get; set; }

        public ListExample2Model(IEnumerable<Example2Model> entities, int totalItems, int page, int pageSize)
           : base(totalItems, page, pageSize)
        {
            Guard.Argument(entities, nameof(entities)).NotNull();
            Examples = entities;
        }
    }
}