
using System.IdentityModel.Tokens.Jwt;
using ECommerce.Services.Order.Application.Consumer;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//MassTransit
builder.Services.AddMassTransit(m =>
{
    m.AddConsumer<CreateOrderMessageCommandConsumer>();
    m.AddConsumer<ProductNameChangedEventConsumer>();

    //port: 5672
    m.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("create-order-service", e => 
        {
            e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
        });

        cfg.ReceiveEndpoint("product-name-changed-event-order-service", e => 
        {
            e.ConfigureConsumer<ProductNameChangedEventConsumer>(context);
        });
    });
});


// --- Identity start --
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_order";
    options.RequireHttpsMetadata = false;
});
// --- Identity end --

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers(options => {
    options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
