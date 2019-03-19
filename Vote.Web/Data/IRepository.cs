

namespace Vote.Web.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    public interface IRepository
    {
        void AddEvent(Event @event);

        bool EventExists(int id);

        IEnumerable<Event> GetEvents();

        Event GetEvent(int id);

        void RemoveEvent(Event @event);

        Task<bool> SaveAllAsync();

        void UpdateEvent(Event @event);
    }
}