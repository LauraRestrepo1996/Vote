using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vote.Web.Data.Entities
{
    public class Vote : IEntity
    {
        public int Id { get; set; }
        public Event Event { get; set; }

        public Candidate Candidate { get; set; }

        public User User { get; set; }

    }
}
