using Microsoft.EntityFrameworkCore;
using PaymentOrder.API.Data;
using PaymentOrder.API.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PayOrderDb>(opt => opt.UseInMemoryDatabase("PayOrderList"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/payorder/{identifier}", async (string identifier, PayOrderDb db) =>
    await db.PayOrders.FirstOrDefaultAsync(x => x.Identifier == identifier)
        is PayOrder payOrder
            ? Results.Ok(payOrder)
            : Results.NotFound($"Not found."));

app.MapPost("/payorder", async (PayOrderDto payOrderDto, PayOrderDb db) =>
{
    PayOrder payOrder = new PayOrder()
    {
        Identifier = Guid.NewGuid().ToString(),
        Cpf = payOrderDto.Cpf,
        Value = Convert.ToDecimal(payOrderDto.Value),
    };

    db.PayOrders.Add(payOrder);
    await db.SaveChangesAsync();

    return Results.Created($"/payorder/{payOrder.Identifier}", payOrder);
});

app.Run();