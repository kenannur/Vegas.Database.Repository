using MongoDB.Bson;
using MongoDB.Driver;

namespace Vegas.Database.MongoDB.Extensions
{
    public static class MongoExtensions
    {
        public static ObjectId? ToObjectId(this string str)
            => ObjectId.TryParse(str, out ObjectId objectId) ? objectId : null;

        public static IFindFluent<TEntity, TEntity> NextPage<TEntity>(
            this IFindFluent<TEntity, TEntity> queryable, int? pageNumber, int? pageCount)
        {
            if (!pageNumber.HasValue || !pageCount.HasValue)
            {
                return queryable;
            }
            return queryable
                .Skip((pageNumber.Value - 1) * pageCount.Value)
                .Limit(pageCount.Value);
        }
    }
}
