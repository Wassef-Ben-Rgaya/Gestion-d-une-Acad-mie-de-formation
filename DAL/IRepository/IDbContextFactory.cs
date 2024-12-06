
using Core.Entities;

namespace DAL
{
    public interface IDbContextFactory
    {
        GAFDbContext DbContext { get; }
    }
}
