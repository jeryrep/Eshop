using Eshop.Application.Orders;
using Eshop.Domain.Customers;
using Eshop.Domain.Orders;
using Eshop.Domain.SeedWork;
using Eshop.Infrastructure.Database;
using Eshop.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Eshop.Infrastructure;

public static class Registry
{
    public static IServiceCollection RegistryInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbSettings = configuration.GetSection("MongoDB").Get<MongoDbSettings>() ?? throw new InvalidOperationException();

        return services
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IProductPriceDataApi, ProductPriceDataApi>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IEntityTracker, EntityTracker>()
            .AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>()
            .AddSingleton<IMongoClient>(_ => new MongoClient(mongoDbSettings.ConnectionString))
            .AddSingleton(new OrdersContext(mongoDbSettings))
            .AddSingleton(new CustomersContext(mongoDbSettings));
    }
}