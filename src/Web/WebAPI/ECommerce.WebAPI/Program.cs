using System;
using System.Reflection;
using MassTransit;
using MassTransit.Definition;
using Messages.Accounts.Commands;
using Messages.Accounts.Queries;
using Messages.Catalogs.Commands;
using Messages.Catalogs.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "ECommerce.WebAPI", Version = "v1" }); });

builder.Services
    .AddMassTransit(cfg =>
    {
        cfg.SetKebabCaseEndpointNameFormatter();

        cfg.UsingRabbitMq((_, bus) =>
        {
            bus.Host(
                host: "192.168.100.9",
                host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

            // Account
            MapQueueEndpoint<RegisterAccount>();
            MapQueueEndpoint<UpdateAccount>();
            MapQueueEndpoint<DeleteAccount>();
            MapQueueEndpoint<GetAccountDetails>();
            MapQueueEndpoint<GetAccountsDetailsWithPagination>();
            
            // Catalog
            MapQueueEndpoint<CreateCatalog>();
            MapQueueEndpoint<UpdateCatalog>();
            MapQueueEndpoint<DeleteCatalog>();
            MapQueueEndpoint<ActivateCatalog>();
            MapQueueEndpoint<DeactivateCatalog>();
            MapQueueEndpoint<AddCatalogItem>();
            MapQueueEndpoint<RemoveCatalogItem>();
            MapQueueEndpoint<UpdateCatalogItem>();
            MapQueueEndpoint<GetCatalogItemsDetailsWithPagination>();
        });
    })
    .AddGenericRequestClient()
    .AddMassTransitHostedService();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.WebAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void MapQueueEndpoint<TMessage>()
    where TMessage : class
    => EndpointConvention.Map<TMessage>(new Uri($"exchange:{ToKebabCaseString(typeof(TMessage))}"));

static string ToKebabCaseString(MemberInfo member)
    => KebabCaseEndpointNameFormatter.Instance.SanitizeName(member.Name);