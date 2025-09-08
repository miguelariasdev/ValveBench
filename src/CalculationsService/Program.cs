using CalculationsService.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8090, o => o.Protocols = HttpProtocols.Http2);
});

var app = builder.Build();

app.MapGrpcService<CalculationsServiceGrpc>();
app.MapGet("/", () => "CalculationsService up (gRPC on h2c :8090)");

app.Run();
