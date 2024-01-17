using Eshop.Application.Configuration.Queries;
using Eshop.Domain.Customers;

namespace Eshop.Application.Customers.Queries;

public class GetCustomerQueryHandler(ICustomerRepository customerRepository)
    : IQueryHandler<GetCustomerQuery, Customer>
{
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));

    public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetByIdAsync(request.CustomerId);
    }
}