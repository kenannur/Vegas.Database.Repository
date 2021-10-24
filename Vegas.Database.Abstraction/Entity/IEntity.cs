using System;

namespace Vegas.Database.Abstraction.Entity
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }

        DateTime? CreatedDate { get; set; }
    }
}
