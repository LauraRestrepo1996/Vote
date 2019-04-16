

namespace Vote.Web.Data
{
    using System.Linq;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly DataContext context;

        public EventRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Events.Include(p => p.User).OrderBy(p => p.Name);

        }
    }

}
