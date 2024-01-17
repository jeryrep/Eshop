using Eshop.Domain.Customers.Rules;
using Eshop.Domain.SeedWork;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Domain.Customers;

public class Customer : Entity, IAggregateRoot
{
    [BsonId, BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; private set; }

    [BsonElement("name")]
    public string Name { get; private set; }    
        
    public static Customer Create(Guid id, string name)
    {
        CheckRule(new CustomerNameMustNotBeEmptyAndConsistsOnlyLetters(name));
        return new Customer(id, name);
    }

    private Customer(Guid id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}