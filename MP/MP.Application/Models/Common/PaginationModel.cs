using Dawn;

namespace MP.Application.Models.Common
{
    public abstract class PaginationModel
    {
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        protected PaginationModel(int totalItems, int page, int pageSize)
        {
            Guard.Argument(page, nameof(page)).NotZero();
            Guard.Argument(pageSize, nameof(pageSize)).NotZero();

            TotalItems = totalItems;
            Page = page;
            PageSize = pageSize;
            TotalPages = (TotalItems == 0) ? 1 : (int)Math.Ceiling(totalItems / (decimal)PageSize);
        }
    }
}