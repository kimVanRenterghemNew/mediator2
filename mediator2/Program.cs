using System.Reflection;
using mediator2;
using mediator2.Controllers;
using mediator2.Querrys;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

RegisterMediator(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




void RegisterMediator(IServiceCollection services)
{
    var requistType = typeof(Requist<,>);
    var requistImplementations = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == requistType));

    RegisterRequistValidators(services);

    foreach (var implementation in requistImplementations)
    {
        // Register Requist<TQuery, TResult> with MediatorFlow<TQuery, TResult> as its handler
        var serviceType = implementation.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == requistType);
        var genericArguments = serviceType.GetGenericArguments();

        // Create MediatorFlow type for the specific Requist<TQuery, TResult>
        var mediatorFlowType = typeof(MediatorFlow<,>).MakeGenericType(genericArguments);

        // Register the specific Requist<TQuery, TResult> with a factory to return MediatorFlow<TQuery, TResult>
        services.AddScoped(serviceType, provider =>
        {
            var validator = provider.GetRequiredService(typeof(RequistValidator<>).MakeGenericType(genericArguments[0]));
            var handler = ActivatorUtilities.CreateInstance(provider, implementation);
            return ActivatorUtilities.CreateInstance(provider, mediatorFlowType, validator, handler);
        });
    }
}

void RegisterRequistValidators(IServiceCollection services)
{
    var requistValidatorType = typeof(RequistValidator<>);
    var requistValidatorImplementations = Assembly.GetExecutingAssembly().GetTypes()
        .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == requistValidatorType));

    foreach (var implementation in requistValidatorImplementations)
    {
        var serviceType = implementation.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == requistValidatorType);
        services.AddScoped(serviceType, implementation);
    }
}
