using Eshop.Domain.Customers;
using Eshop.Domain.Orders;
using Eshop.Domain.SeedWork;
using MongoDB.Driver;

namespace Eshop.Infrastructure.Database;

internal class UnitOfWork(
    OrdersContext ordersContext,
    CustomersContext customersContext,
    IEntityTracker entityTracker,
    IDomainEventsDispatcher domainEventsDispatcher)
    : IUnitOfWork
{
    private readonly OrdersContext _ordersContext = ordersContext ?? throw new ArgumentNullException(nameof(ordersContext));
    private readonly CustomersContext _customersContext = customersContext ?? throw new ArgumentNullException(nameof(customersContext));
    private readonly IEntityTracker _entityTracker = entityTracker ?? throw new ArgumentNullException(nameof(entityTracker));
    private readonly IDomainEventsDispatcher _domainEventsDispatcher = domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        var trackedEntities = _entityTracker.GetTrackedEntities().ToList();

        foreach (var entity in trackedEntities)
        {
            switch (entity)
            {
                case Order order:
                {
                    var filter = Builders<Order>.Filter.Eq(c => c.Id, order.Id);
                    await _ordersContext.Orders.ReplaceOneAsync(filter, order, new ReplaceOptions { IsUpsert = true },
                        cancellationToken);
                    break;
                }
                case Customer customer:
                {
                    var filter = Builders<Customer>.Filter.Eq(c => c.Id, customer.Id);
                    await _customersContext.Customers.ReplaceOneAsync(filter, customer, new ReplaceOptions { IsUpsert = true },
                        cancellationToken);
                    break;
                }
            }
        }

        if (trackedEntities.Count == 0) return 0;
        await _domainEventsDispatcher.DispatchEventsAsync(trackedEntities);
        _entityTracker.ClearTrackedEntities();

        return 0;
    }
}