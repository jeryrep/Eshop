using Eshop.Domain.Orders.Events;
using Eshop.Domain.Orders.Rules;
using Eshop.Domain.Products;
using Eshop.Domain.SeedWork;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Domain.Orders;

public class Order : Entity, IAggregateRoot
{
    [BsonId, BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; private set; }

    [BsonElement("customerId"), BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid CustomerId { get; private set; }

    [BsonElement("products")]
    public List<OrderProduct> Products { get; private set; }

    private Order(Guid customerId, List<OrderProduct> orderProducts)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Products = orderProducts ?? throw new ArgumentNullException(nameof(orderProducts));

        AddDomainEvent(new OrderAddedEvent(Id, customerId));
    }

    public static Order Create(
        Guid customerId,
        List<OrderProductData> orderProductsData,
        List<ProductPriceData> allProductPriceDatas)
    {
        List<OrderProduct> orderProducts = [];
        orderProducts.AddRange(orderProductsData
            .Select(orderProductData => new
            {
                orderProductData,
                productPriceData = allProductPriceDatas.First(x => x.ProductId == orderProductData.ProductId)
            })
            .Select(t => OrderProduct.Create(t.orderProductData.ProductId, t.orderProductData.Quantity,
                t.productPriceData.UnitPrice)));

        CheckRule(new OrderMustHaveAtLeastOneProductRule(orderProducts));
        CheckRule(new TotalCostMustBeLessThan15000Rule(orderProducts));

        return new Order(customerId, orderProducts);
    }
}