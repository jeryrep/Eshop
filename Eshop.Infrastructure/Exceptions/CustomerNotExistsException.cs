namespace Eshop.Infrastructure.Exceptions;

public class CustomerNotExistsException(Guid id) : Exception
{
    public Guid Id { get; } = id;
}