﻿using Eshop.Application.Configuration.Queries;
using Eshop.Application.Shared;

namespace Eshop.Application.Orders.CustomerOrder.Queries;

public class GetOrderQuery(Guid orderId) : IQuery<OrderDto>
{
    public Guid OrderId { get; } = orderId;
}