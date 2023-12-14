using Flunt.Validations;
using MP.CrossCutting.Utils.Resources;

namespace MP.Application.Models.Common
{
    public class SearchModel : PaginationSearchModel
    {
        public string? Term { get; set; }
        public virtual void Validate()
        {
            AddNotifications(new Contract<SearchModel>()
              .Requires()
              .IsNotNull(Page, nameof(Page), string.Format(Messages.InvalidFieldNullOrEmpty, "Page"))
              .IsGreaterOrEqualsThan(Page.GetValueOrDefault(), 1, nameof(Page), string.Format(Messages.InvalidFieldMinValue, nameof(Page), 1))
              .IsNotNull(PageSize, nameof(PageSize), string.Format(Messages.InvalidFieldNullOrEmpty, nameof(PageSize)))
              .IsGreaterOrEqualsThan(PageSize.GetValueOrDefault(), 10, nameof(PageSize), string.Format(Messages.InvalidFieldMinValue, nameof(PageSize), 10)));
        }
    }
}