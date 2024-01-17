﻿namespace Eshop.Infrastructure.Exceptions;

public class OrderNotExistsException(Guid id) : Exception
{ 
    public Guid Id { get; } = id;
}