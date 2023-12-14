using Flunt.Notifications;

namespace MP.Application.Models.Common
{
    public abstract class PaginationSearchModel : Notifiable<Notification>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}