using Microsoft.EntityFrameworkCore;
using TCC.Business.Interfaces;
using TCC.Data.Context;
using TCC.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUnityOfWork, UnitOfWork>();

var cs = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DefaultConnection");

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(cs);
});

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
