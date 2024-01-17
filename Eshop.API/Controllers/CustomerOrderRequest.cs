using Eshop.Application.Shared;

namespace Eshop.API.Controllers;

public class CustomerOrderRequest(List<ProductDto> products)
{
    public List<ProductDto> Products { get; set; } = products ?? throw new ArgumentNullException(nameof(products));
}