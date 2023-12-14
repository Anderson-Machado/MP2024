using Flunt.Notifications;

namespace MP.Core.Entities.Dtos
{
    public class SearchDto : Notifiable<Notification>
    {
        public string? Term { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}