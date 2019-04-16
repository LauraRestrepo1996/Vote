
namespace Vote.Web.Data
{
    using Entities;
    using System.Linq;

    public interface IEventRepository : IGenericRepository<Event>
    {
        IQueryable GetAllWithUsers(); 

    }

}
