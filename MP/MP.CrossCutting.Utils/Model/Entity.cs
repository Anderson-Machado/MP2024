using Flunt.Notifications;
using MP.CrossCutting.Utils.Interfaces.Model;

namespace MP.CrossCutting.Utils.Model
{
    public abstract class Entity : Notifiable<Notification>, IIdentifiable
    {
        public decimal Id { get; set; }

    }
}