using Eshop.Application.Orders.CustomerOrder.Queries;
using Eshop.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Eshop.API.Controllers;

[Route("api/v1/orders")]
public class OrderController(ISender mediator) : Controller
{
    /// <summary>
    /// Retrieve order.
    /// </summary>
    /// <param name="orderId">Order ID.</param>
    [HttpGet("{orderId:guid}"), ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCustomerOrderDetails([FromRoute] Guid orderId)
    {
        var orderDetails = await mediator.Send(new GetOrderQuery(orderId));
        return Ok(orderDetails);
    }
}