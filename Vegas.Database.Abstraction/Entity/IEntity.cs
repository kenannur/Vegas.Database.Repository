using System;

namespace Vegas.Database.Abstraction.Entity
{
    public interface IEntity<TId>
        where TId : struct
    {
        TId Id { get; set; }

        DateTime? CreatedDate { get; set; }
    }
}
