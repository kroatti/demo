using Delta.Invoicing.Core.Application.Abstraction;
using Delta.Invoicing.Core.Infrastructure;
using Delta.Invoicing.Core.Pipeline;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Delta.Invoicing.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            // Application services
            .AddSingleton<ICompanyDataStore, FakeCompanyDataStore>()
            .AddTransient<IItemRepository, FakeItemRepository>()
            .AddSingleton<IInvoiceRepository, JsonInvoiceRepository>()
            
            // Framework services
            .AddMediatR(typeof(ServiceCollectionExtension).Assembly)
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestLogging<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidator<,>))
            .AddAutoMapper(typeof(ServiceCollectionExtension).Assembly)
            .AddValidatorsFromAssembly(typeof(ServiceCollectionExtension).Assembly);

        return services;
    }
}