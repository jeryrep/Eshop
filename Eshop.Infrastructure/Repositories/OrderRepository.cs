using Eshop.Domain.Orders;
using Eshop.Infrastructure.Database;
using Eshop.Infrastructure.Exceptions;
using MongoDB.Driver;

namespace Eshop.Infrastructure.Repositories;

internal class OrderRepository(OrdersContext context, IEntityTracker entityTracker) : IOrderRepository
{
    private readonly OrdersContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly IEntityTracker _entityTracker = entityTracker ?? throw new ArgumentNullException(nameof(entityTracker));

    public void Add(Order order)
    {
        _entityTracker.TrackEntity(order);
    }

    public async Task<Order> GetByIdAsync(Guid id)
    {
        var order = await _context.Orders.Find(c => c.Id == id).FirstAsync() ?? throw new OrderNotExistsException(id);
        _entityTracker.TrackEntity(order);
        return order;
    }
}