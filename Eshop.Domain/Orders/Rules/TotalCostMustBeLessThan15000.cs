using Eshop.Domain.SeedWork;

namespace Eshop.Domain.Orders.Rules;

public class TotalCostMustBeLessThan15000Rule(IEnumerable<OrderProduct> orderProducts) : IBusinessRule
{
    public bool IsBroken() => orderProducts.Sum(x => x.TotalCost) > 15000;

    public string Message => "Total cost of order must be less than 15000";
}