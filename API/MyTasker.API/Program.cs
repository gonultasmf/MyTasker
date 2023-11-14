using Microsoft.EntityFrameworkCore;
using MyTasker.API.Context;
using MyTasker.API.Repositories.Abstract;
using MyTasker.API.Repositories.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISettingsRepository, SettingsRepository>();
builder.Services.AddTransient<ITaskModelRepository, TaskModelRepository>();

SQLitePCL.Batteries.Init();

builder.Services.AddDbContext<MyTaskerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AppDbConnStr")));

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
