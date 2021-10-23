using System;
using Vegas.Database.Abstraction.Entity;

namespace Vegas.Database.RelationalDB.Entity
{
    public interface IRelationalEntity : IEntity<long>
    { }

    public class RelationalEntity : IRelationalEntity
    {
        public long Id { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
