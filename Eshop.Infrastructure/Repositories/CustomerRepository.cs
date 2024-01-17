using Eshop.Domain.Customers;
using Eshop.Infrastructure.Database;
using Eshop.Infrastructure.Exceptions;
using MongoDB.Driver;

namespace Eshop.Infrastructure.Repositories;

internal class CustomerRepository(CustomersContext customersContext, IEntityTracker entityTracker) : ICustomerRepository
{
    private readonly CustomersContext _customersContext = customersContext ?? throw new ArgumentNullException(nameof(customersContext));
    private readonly IEntityTracker _entityTracker = entityTracker ?? throw new ArgumentNullException(nameof(entityTracker));

    public async Task<Customer> GetByIdAsync(Guid id)
    {
        var ids = await _customersContext.Customers.Find(c => true).ToListAsync();
        var customer = await _customersContext.Customers.Find(c => c.Id == id).FirstAsync() ?? throw new CustomerNotExistsException(id);
        _entityTracker.TrackEntity(customer);
        return customer;
    }

    public void Add(Customer customer)
    {
        _entityTracker.TrackEntity(customer);
    }
}