using Microsoft.EntityFrameworkCore;
using TCC.Api.Extensions;
using TCC.Application.DependencyInjection;
using TCC.Business.Interfaces;
using TCC.Data.Context;
using TCC.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DefaultConnection");

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(cs);
});

builder.Services.AddAutoMapper(typeof(ApplicationDI));

builder.Services.ResolveDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
