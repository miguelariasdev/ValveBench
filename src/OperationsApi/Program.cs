using Microsoft.AspNetCore.Server.Kestrel.Core;
using ValveBench.proto;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<Calc.CalcClient>(o =>
{
    o.Address = new Uri(Environment.GetEnvironmentVariable("CALC_URL") ?? "http://localhost:8090");
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, o => o.Protocols = HttpProtocols.Http1);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();