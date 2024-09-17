using Microsoft.OpenApi.Models;
using TradeEngine.Infrastructure;
using TradeEngine.Processing;
using TradeEngine.Processing.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IPublisher<TradeValuation>, TradeValuationPublisher>();
builder.Services.AddSingleton<TradeEngine.Processing.TradeEngine>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientDataApi", Version = "v1" });
});

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientDataApi v1"));
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();