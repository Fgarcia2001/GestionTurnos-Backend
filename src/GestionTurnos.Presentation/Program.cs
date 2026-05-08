using GestionTurnos.Aplication.Abstraction;
using GestionTurnos.Aplication.Abstraction.Infrastructure;
using GestionTurnos.Aplication.Services;
using GestionTurnos.Domain.Entities;
using GestionTurnos.Infrastructure.Persistance;
using GestionTurnos.Infrastructure.Persistance.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(StaffRepository<>));
builder.Services.AddScoped(typeof(StaffRepository<>), typeof(StaffRepository<>));
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddScoped<IStaffService, StaffService>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FMCTurnosDbContext>(options =>
    options.UseSqlServer(connectionString));
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
