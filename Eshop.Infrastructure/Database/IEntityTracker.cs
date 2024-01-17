using Eshop.Domain.SeedWork;

namespace Eshop.Infrastructure.Database;

internal interface IEntityTracker
{
    void ClearTrackedEntities();

    IEnumerable<Entity> GetTrackedEntities();

    void TrackEntity(Entity entity);
}