

namespace Vote.Web.Data.Entities
{
    using System;
    public class Country : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
