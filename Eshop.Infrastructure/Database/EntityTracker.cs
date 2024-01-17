using Eshop.Domain.SeedWork;

namespace Eshop.Infrastructure.Database;

internal class EntityTracker : IEntityTracker
{
    private readonly List<Entity> _trackedEntities = [];

    public void TrackEntity(Entity entity)
    {
        _trackedEntities.Add(entity);
    }

    public IEnumerable<Entity> GetTrackedEntities() => _trackedEntities.AsReadOnly();

    public void ClearTrackedEntities() => _trackedEntities.Clear();
}