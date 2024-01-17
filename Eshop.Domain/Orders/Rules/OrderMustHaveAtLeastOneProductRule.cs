using Eshop.Domain.SeedWork;

namespace Eshop.Domain.Orders.Rules;

public class OrderMustHaveAtLeastOneProductRule(IEnumerable<OrderProduct> orderProducts) : IBusinessRule
{
    public bool IsBroken() => !orderProducts.Any();

    public string Message => "Order must have at least one product";
}