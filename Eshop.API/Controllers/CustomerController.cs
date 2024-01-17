using Eshop.Application.Orders.CustomerOrder.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Eshop.Application.Customers.Commands;
using Eshop.Application.Customers.Queries;

namespace Eshop.API.Controllers;

[ApiController, Route("api/v1/customers")]
public class CustomerController(ISender mediator) : Controller
{
    /// <summary>
    /// Adds a new order for a specified customer.
    /// </summary>
    /// <param name="customerId">Customer ID.</param>
    /// <param name="request">List of products.</param>
    [HttpPost("{customerId:guid}/orders"), ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddCustomerOrder(
        [FromRoute] Guid customerId,
        [FromBody] CustomerOrderRequest request)
    {
        var response = await mediator.Send(new AddOrderCommand(customerId, request.Products));
        return Created(string.Empty, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(
        [FromBody] CreateCustomerRequest request)
    {
        var response = await mediator.Send(new CreateCustomerCommand(request.Name));
        return Created(string.Empty, response);
    }

    [HttpGet("{customerId:guid}")]
    public async Task<IActionResult> GetCustomer(
        [FromRoute] Guid customerId)
    {
        var response = await mediator.Send(new GetCustomerQuery(customerId));
        return Ok(response);
    }
}