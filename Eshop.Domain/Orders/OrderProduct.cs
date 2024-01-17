using Eshop.Domain.SeedWork;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Domain.Orders;

public class OrderProduct : ValueObject
{
    [BsonId, BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; private set; }

    [BsonElement("quantity")]
    public int Quantity { get; private set; }

    [BsonElement("unitPrice")]
    public decimal UnitPrice { get; set; }

    public decimal TotalCost => Quantity * UnitPrice;

    private OrderProduct()
    {

    }

    private OrderProduct(Guid productId, int quantity, decimal unitPrice)
    {
        Id = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static OrderProduct Create(Guid productId, int quantity, decimal unitPrice)
    {
        return new OrderProduct(productId, quantity, unitPrice);
    }
}