using Eshop.Application.Configuration.Queries;
using Eshop.Domain.Customers;

namespace Eshop.Application.Customers.Queries;

public class GetCustomerQuery(Guid customerId) : IQuery<Customer>
{
    public Guid CustomerId { get; } = customerId;
}