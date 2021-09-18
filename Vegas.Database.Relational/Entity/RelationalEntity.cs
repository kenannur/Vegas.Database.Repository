using System;
using Vegas.Database.Abstraction.Entity;

namespace Vegas.Database.Relational.Entity
{
    public class RelationalEntity : IEntity<long>
    {
        public long Id { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
