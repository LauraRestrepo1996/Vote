
namespace Vote.Web.Data
{
    using Entities;

    public class CandidateRepository : GenericRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(DataContext context) : base(context)
        {

        }
    }
}
