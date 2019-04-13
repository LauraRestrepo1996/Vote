using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Vote.Web.Data

{
    using Entities; 

    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        public VoteRepository(DataContext context) : base(context)
        {

        }
    }
}
