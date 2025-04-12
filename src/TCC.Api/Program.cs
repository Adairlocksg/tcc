using Microsoft.EntityFrameworkCore;
using TCC.Api.Extensions;
using TCC.Application.DependencyInjection;
using TCC.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

var cs = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DefaultConnection");
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*: {port}");

builder.Services.AddHealthChecks();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(cs);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddServiceWebHost(builder.Configuration);

builder.Services.AddAutoMapper(typeof(ApplicationDI));

builder.Services.ResolveDependencies();

var app = builder.Build();

app.UseCors("AllowAll");


//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseServiceApplicationAuth();

app.MapControllers();

app.Run();
